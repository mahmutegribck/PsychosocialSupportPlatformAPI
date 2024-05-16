using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using PsychosocialSupportPlatformAPI.Business.Appointments.DTOs;
using PsychosocialSupportPlatformAPI.Business.AppointmentSchedules.DTOs;
using PsychosocialSupportPlatformAPI.Business.Auth.JwtToken.DTOs;
using PsychosocialSupportPlatformAPI.Business.Users.DTOs;
using PsychosocialSupportPlatformAPI.DataAccess.Appointments;
using PsychosocialSupportPlatformAPI.DataAccess.AppointmentSchedules;
using PsychosocialSupportPlatformAPI.Entity.Entities.Appointments;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PsychosocialSupportPlatformAPI.Business.Appointments
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IAppointmentScheduleRepository _appointmentScheduleRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;


        public AppointmentService(
            IAppointmentRepository appointmentRepository,
            IAppointmentScheduleRepository appointmentScheduleRepository,
            IMapper mapper,
            IConfiguration configuration)
        {
            _appointmentRepository = appointmentRepository;
            _appointmentScheduleRepository = appointmentScheduleRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task CancelPatientAppointment(CancelPatientAppointmentDTO cancelPatientAppointmentDTO, string patientId)
        {
            AppointmentSchedule appointmentSchedule = _mapper.Map<AppointmentSchedule>(cancelPatientAppointmentDTO);
            appointmentSchedule.PatientId = patientId;
            AppointmentSchedule? cancelAppointmentSchedule = await _appointmentRepository.GetPatientAppointment(appointmentSchedule);

            if (cancelAppointmentSchedule == null) throw new Exception();

            await _appointmentRepository.CancelPatientAppointment(cancelAppointmentSchedule);
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
            AppointmentSchedule? appointmentSchedule = await _appointmentScheduleRepository.GetAppointmentScheduleByDayAndTimeRange(makeAppointmentDTO.DoctorId, DateTime.Parse(makeAppointmentDTO.Day), makeAppointmentDTO.TimeRange);

            if (appointmentSchedule == null)
                return false;

            appointmentSchedule.PatientId = patientId;
            appointmentSchedule.Status = true;

            appointmentSchedule.URL = await GenerateZoomMeetingUrl();

            await _appointmentScheduleRepository.UpdateAppointmentSchedule(appointmentSchedule);

            return true;
        }

        private async Task<string> GenerateZoomMeetingUrl()
        {
            var tokenhandler = new JwtSecurityTokenHandler();

            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var now = DateTime.UtcNow;
            var apiSecret = "pBMwoo9u5qoKf7ScJh9FF1gT1IdVSzv1";
            byte[] symmetricKey = Encoding.ASCII.GetBytes(apiSecret);


            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = "X7ialNJvTKq1NdmclN0Sg",
                Expires = now.AddSeconds(300),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(symmetricKey), SecurityAlgorithms.HmacSha256),
            };

            var token = tokenhandler.CreateToken(tokenDescriptor);
            var finaltoken = tokenhandler.WriteToken(token);

            string apiUrl = "https://api.zoom.us/v2/users/me/meetings";

            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", finaltoken);


            var requestBody = new
            {
                topic = "deneme",
                type = 2,
                start_time = DateTime.UtcNow.AddMinutes(10).ToString("yyyy-MM-ddTHH:mm:ssZ"),
                duration = 60,
                timezone = "UTC",
                settings = new
                {
                    host_video = true,
                    participant_video = true
                }

            };
            var requestContent = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(apiUrl, requestContent);
            var responseContent = await response.Content.ReadAsStringAsync();



            return "deneme";
        }
    }
}
