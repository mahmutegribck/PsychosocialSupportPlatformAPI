using AutoMapper;
using PsychosocialSupportPlatformAPI.Business.Appointments.DTOs;
using PsychosocialSupportPlatformAPI.Business.AppointmentSchedules.DTOs;
using PsychosocialSupportPlatformAPI.Business.Users.DTOs;
using PsychosocialSupportPlatformAPI.DataAccess.Appointments;
using PsychosocialSupportPlatformAPI.DataAccess.AppointmentSchedules;
using PsychosocialSupportPlatformAPI.Entity.Entities.Appointments;

namespace PsychosocialSupportPlatformAPI.Business.Appointments
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IAppointmentScheduleRepository _appointmentScheduleRepository;
        private readonly IMapper _mapper;
        public AppointmentService(IAppointmentRepository appointmentRepository, IAppointmentScheduleRepository appointmentScheduleRepository, IMapper mapper)
        {
            _appointmentRepository = appointmentRepository;
            _appointmentScheduleRepository = appointmentScheduleRepository;
            _mapper = mapper;
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

            //Toplantı bağlantısı oluşturulacak.

            await _appointmentScheduleRepository.UpdateAppointmentSchedule(appointmentSchedule);

            return true;
        }

        //private async Task<string> GenerateZoomMeetingUrl()
        //{

        //}
    }
}
