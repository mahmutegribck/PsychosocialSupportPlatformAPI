using AutoMapper;
using Microsoft.Extensions.Configuration;
using PsychosocialSupportPlatformAPI.Business.Appointments.DTOs;
using PsychosocialSupportPlatformAPI.Business.AppointmentSchedules.DTOs;
using PsychosocialSupportPlatformAPI.Business.Mails;
using PsychosocialSupportPlatformAPI.Business.Users;
using PsychosocialSupportPlatformAPI.Business.Users.DTOs;
using PsychosocialSupportPlatformAPI.DataAccess.Appointments;
using PsychosocialSupportPlatformAPI.DataAccess.AppointmentSchedules;
using PsychosocialSupportPlatformAPI.DataAccess.Users;
using PsychosocialSupportPlatformAPI.Entity.Entities.Appointments;
using PsychosocialSupportPlatformAPI.Entity.Entities.Users;
using System.Text;
using System.Text.Json;

namespace PsychosocialSupportPlatformAPI.Business.Appointments
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IAppointmentScheduleRepository _appointmentScheduleRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IMailService _mailService;
        private readonly IUserRepository _userRepository;

        public AppointmentService(
            IAppointmentRepository appointmentRepository,
            IAppointmentScheduleRepository appointmentScheduleRepository,
            IMapper mapper,
            IConfiguration configuration,
            IMailService mailService,
            IUserRepository userRepository)
        {
            _appointmentRepository = appointmentRepository;
            _appointmentScheduleRepository = appointmentScheduleRepository;
            _mapper = mapper;
            _configuration = configuration;
            _mailService = mailService;
            _userRepository = userRepository;
        }


        public async Task CancelDoctorAppointment(CancelDoctorAppointmentDTO cancelDoctorAppointmentDTO, string doctorId)
        {
            AppointmentSchedule appointmentSchedule = _mapper.Map<AppointmentSchedule>(cancelDoctorAppointmentDTO);
            appointmentSchedule.DoctorId = doctorId;

            AppointmentSchedule? cancelAppointmentSchedule = await _appointmentRepository.GetDoctorAppointment(appointmentSchedule) ?? throw new Exception("İptal Edilmek İstenen Randevu Kaydı Bulunamadı");

            if (cancelAppointmentSchedule.PatientId != null)
            {
                await _mailService.SendEmailToPatientForCancelAppointment(cancelAppointmentSchedule);
            }
            await _appointmentRepository.CancelDoctorAppointment(cancelAppointmentSchedule);
        }


        public async Task CancelPatientAppointment(CancelPatientAppointmentDTO cancelPatientAppointmentDTO, string patientId)
        {
            AppointmentSchedule appointmentSchedule = _mapper.Map<AppointmentSchedule>(cancelPatientAppointmentDTO);
            appointmentSchedule.PatientId = patientId;
            AppointmentSchedule? cancelAppointmentSchedule = await _appointmentRepository.GetPatientAppointment(appointmentSchedule) ?? throw new Exception("İptal Edilmek İstenen Randevu Kaydı Bulunamadı");

            await _mailService.SendEmailToDoctorForCancelAppointment(cancelAppointmentSchedule);
            await _appointmentRepository.CancelPatientAppointment(cancelAppointmentSchedule);
        }

        public async Task CreateAppointmentForPatient(string doctorId, CreateAppointmentForPatientDTO createAppointmentForPatientDTO)
        {
            AppointmentSchedule? doctorAppoitment = await _appointmentScheduleRepository.GetAppointmentScheduleByDayAndTimeRange(doctorId, DateTime.Parse(createAppointmentForPatientDTO.Day), createAppointmentForPatientDTO.TimeRange);

            if (doctorAppoitment == null)
            {
                AppointmentSchedule appointmentSchedule = new()
                {
                    DoctorId = doctorId,
                    Day = DateTime.Parse(createAppointmentForPatientDTO.Day),
                    TimeRange = createAppointmentForPatientDTO.TimeRange
                };
                await _appointmentScheduleRepository.AddAppointmentSchedule(appointmentSchedule);

                doctorAppoitment = appointmentSchedule;
            }
            if (doctorAppoitment!.Status == true)
                throw new Exception("Başka Randevu Kaydınız Bulunmaktadır");

            Patient? patient = await _userRepository.GetPatientBySlug(createAppointmentForPatientDTO.PatientUserName) ?? throw new Exception("Hasta Kullanıcı Bulunamadı");

            doctorAppoitment.PatientId = patient.Id;
            doctorAppoitment.Status = true;

            doctorAppoitment.URL = "await GenerateZoomMeetingUrl()";

            await _appointmentScheduleRepository.UpdateAppointmentSchedule(doctorAppoitment);

        }

        public async Task<GetPatientAppointmentDTO?> GetPatientAppointmentById(int patientAppointmentId, string patientId)
        {
            return _mapper.Map<GetPatientAppointmentDTO>(await _appointmentRepository.GetPatientAppointmentById(patientAppointmentId, patientId));
        }


        public async Task<IEnumerable<object>> GetPatientAppointmentsByPatientId(string patientID)
        {
            return await _appointmentRepository.GetPatientAppointmentsByPatientId(patientID);
        }


        public async Task<IEnumerable<GetPatientDoctorDto>> GetPatientDoctorsByPatientId(string patientId)
        {
            return _mapper.Map<IEnumerable<GetPatientDoctorDto>>(await _appointmentRepository.GetPatientDoctorsByPatientId(patientId));
        }


        public async Task<bool> MakeAppointment(string patientId, MakeAppointmentDTO makeAppointmentDTO)
        {
            AppointmentSchedule? patientLastAppointment = await _appointmentRepository.GetPatientLastAppointment(patientId);

            if (patientLastAppointment != null)
            {
                if (Math.Abs((DateTime.Parse(makeAppointmentDTO.Day) - patientLastAppointment.Day).Days) < 14)
                    throw new Exception("Son 14 Gün İçerisinde Alınmış Randevunuz Bulunmaktadır.");

                if (patientLastAppointment.DoctorId != makeAppointmentDTO.DoctorId)
                    throw new Exception("Sadece Doktorunuzdan Randevu Alabilirsiniz.");
            }

            AppointmentSchedule? appointmentSchedule = await _appointmentScheduleRepository.GetAppointmentScheduleByDayAndTimeRange(makeAppointmentDTO.DoctorId, DateTime.Parse(makeAppointmentDTO.Day), makeAppointmentDTO.TimeRange);

            if (appointmentSchedule == null || appointmentSchedule.Status == true)
                return false;

            appointmentSchedule.PatientId = patientId;
            appointmentSchedule.Status = true;

            appointmentSchedule.URL = "await GenerateZoomMeetingUrl()";

            await _appointmentScheduleRepository.UpdateAppointmentSchedule(appointmentSchedule);

            return true;
        }


        private async Task<string> GenerateZoomMeetingUrl()
        {
            string authHeader = Convert.ToBase64String(Encoding.UTF8.GetBytes(_configuration["Zoom:ApiKey"] + ":" + _configuration["Zoom:SecretKey"]));

            string authorization = "Basic " + authHeader;

            using HttpClient client = new();

            client.DefaultRequestHeaders.Add("Authorization", authorization);

            HttpResponseMessage responseToken = await client.PostAsync(_configuration["Zoom:TokenURl"] + _configuration["Zoom:AccountId"], null);

            string tokenResponseBody = await responseToken.Content.ReadAsStringAsync();

            var jsonObject = JsonDocument.Parse(tokenResponseBody).RootElement;

            string? accessToken = jsonObject.GetProperty("access_token").GetString() ?? throw new Exception("Zoom Token Bulunamadı");

            HttpClient clientMeet = new();

            clientMeet.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

            var requestData = new
            {
                topic = "Artı Bir Destek",
                type = 2,
                start_time = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now.AddHours(3)).ToString("yyyy-MM-ddTHH:mm:ssZ"),
                duration = 60,
                timezone = "Europe/Istanbul",
                settings = new
                {
                    host_video = true,
                    participant_video = true,
                    cn_meeting = false,
                    in_meeting = false,
                    join_before_host = true,
                    mute_upon_entry = false,
                    watermark = false,
                    use_pmi = false,
                    approval_type = 2,
                    registration_type = 1,
                    audio = "both",
                    auto_recording = "none",
                    enforce_login = false,
                    meeting_authentication = true,
                    registration_close_time = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now.AddHours(3).AddMinutes(2)).ToString("yyyy-MM-ddTHH:mm:ssZ"),
                    allow_multiple_devices = false,
                },
            };

            var json = JsonSerializer.Serialize(requestData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage responseMeeting = await clientMeet.PostAsync(_configuration["Zoom:CreateMeetingUrl"], content);

            string responseBody = await responseMeeting.Content.ReadAsStringAsync();

            var meetingJson = JsonDocument.Parse(responseBody).RootElement;
            string joinUrl = meetingJson.GetProperty("join_url").GetString() ?? throw new Exception("Meet Toplantı Bağlantısı Oluşturulamadı");
            Console.WriteLine(meetingJson);
            return joinUrl;
        }
    }
}
