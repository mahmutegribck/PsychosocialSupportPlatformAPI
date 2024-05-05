using AutoMapper;
using PsychosocialSupportPlatformAPI.Business.Statistics.Appointments.DTOs;
using PsychosocialSupportPlatformAPI.DataAccess.Appointments;
using PsychosocialSupportPlatformAPI.DataAccess.Statistics.Appointments;
using PsychosocialSupportPlatformAPI.Entity.Entities.Appointments;

namespace PsychosocialSupportPlatformAPI.Business.Statistics.Appointments
{
    public class AppointmentStatisticsService : IAppointmentStatisticsService
    {
        private readonly IAppointmentStatisticsRepository _appointmentStatisticsRepository;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IMapper _mapper;

        public AppointmentStatisticsService(
            IAppointmentStatisticsRepository appointmentStatisticsRepository,
            IAppointmentRepository appointmentRepository,
            IMapper mapper)
        {
            _appointmentStatisticsRepository = appointmentStatisticsRepository;
            _appointmentRepository = appointmentRepository;
            _mapper = mapper;
        }

        public async Task AddAppointmentStatistics(AddAppointmentStatisticsDTO addAppointmentStatisticsDTO, string doctorId)
        {
            if (!await _appointmentRepository.CheckPatientAppointment(addAppointmentStatisticsDTO.AppointmentScheduleId, addAppointmentStatisticsDTO.PatientId, doctorId)) throw new Exception();

            AppointmentStatistics appointmentStatistics = _mapper.Map<AppointmentStatistics>(addAppointmentStatisticsDTO);

            await _appointmentStatisticsRepository.AddAppointmentStatistics(appointmentStatistics);
        }

        public async Task UpdateAppointmentStatistics(UpdateAppointmentStatisticsDTO updateAppointmentStatisticsDTO, string doctorId)
        {
            if (await _appointmentStatisticsRepository.GetAppointmentStatisticsById(updateAppointmentStatisticsDTO.Id, updateAppointmentStatisticsDTO.PatientId, doctorId) == null) throw new Exception();

            AppointmentStatistics appointmentStatistics = _mapper.Map<AppointmentStatistics>(updateAppointmentStatisticsDTO);
            appointmentStatistics.DoctorId = doctorId;

            await _appointmentStatisticsRepository.UpdateAppointmentStatistics(appointmentStatistics);
        }

        public async Task DeleteAppointmentStatistics(DeleteAppointmentStatisticsDTO deleteAppointmentStatisticsDTO, string doctorId)
        {
            if (await _appointmentStatisticsRepository.GetAppointmentStatisticsById(deleteAppointmentStatisticsDTO.Id, deleteAppointmentStatisticsDTO.PatientId, doctorId) == null) throw new Exception();

            AppointmentStatistics appointmentStatistics = _mapper.Map<AppointmentStatistics>(deleteAppointmentStatisticsDTO);

            appointmentStatistics.DoctorId = doctorId;

            await _appointmentStatisticsRepository.DeleteAppointmentStatistics(appointmentStatistics);
        }

        public async Task<IEnumerable<GetAppointmentStatisticsDTO>> GetAllPatientAppointmentStatisticsByDoctorId(string doctorId)
        {
            return _mapper.Map<IEnumerable<GetAppointmentStatisticsDTO>>(await _appointmentStatisticsRepository.GetAllPatientAppointmentStatisticsByDoctorId(doctorId));
        }

        public async Task<IEnumerable<GetAppointmentStatisticsDTO>> GetAllPatientAppointmentStatisticsByPatientId(string doctorId, string patientId)
        {
            return _mapper.Map<IEnumerable<GetAppointmentStatisticsDTO>>(await _appointmentStatisticsRepository.GetAllPatientAppointmentStatisticsByPatientId(doctorId, patientId));
        }

        public async Task<IEnumerable<object>> GetAllPatientAppointmentStatistics()
        {
            return await _appointmentStatisticsRepository.GetAllPatientAppointmentStatistics();
        }
    }
}
