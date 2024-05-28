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

        public async Task AddAppointmentStatistics(AddAppointmentStatisticsDTO addAppointmentStatisticsDTO, string doctorId, CancellationToken cancellationToken)
        {
            if (!await _appointmentRepository.CheckPatientAppointment(addAppointmentStatisticsDTO.AppointmentScheduleId, addAppointmentStatisticsDTO.PatientId, doctorId, cancellationToken)) throw new Exception("Hasta Randevu Kaydı Bulunamadı.");

            if (await _appointmentStatisticsRepository.CheckPatientAppointmentStatistics(addAppointmentStatisticsDTO.AppointmentScheduleId, cancellationToken)) throw new Exception("Randevuya Ait Rapor Kaydı Bulunmaktadır.");

            AppointmentStatistics appointmentStatistics = _mapper.Map<AppointmentStatistics>(addAppointmentStatisticsDTO);
            appointmentStatistics.DoctorId = doctorId;

            await _appointmentStatisticsRepository.AddAppointmentStatistics(appointmentStatistics, cancellationToken);
        }

        public async Task UpdateAppointmentStatistics(UpdateAppointmentStatisticsDTO updateAppointmentStatisticsDTO, string doctorId, CancellationToken cancellationToken)
        {
            if (await _appointmentStatisticsRepository.GetAppointmentStatisticsById(updateAppointmentStatisticsDTO.Id, updateAppointmentStatisticsDTO.PatientId, updateAppointmentStatisticsDTO.AppointmentScheduleId, doctorId, cancellationToken) == null) throw new Exception("Kayıtlı Randevu Raporu Bulunamadı.");

            AppointmentStatistics appointmentStatistics = _mapper.Map<AppointmentStatistics>(updateAppointmentStatisticsDTO);
            appointmentStatistics.DoctorId = doctorId;

            await _appointmentStatisticsRepository.UpdateAppointmentStatistics(appointmentStatistics, cancellationToken);
        }

        public async Task DeleteAppointmentStatistics(DeleteAppointmentStatisticsDTO deleteAppointmentStatisticsDTO, string doctorId, CancellationToken cancellationToken)
        {
            if (await _appointmentStatisticsRepository.GetAppointmentStatisticsById(deleteAppointmentStatisticsDTO.Id, deleteAppointmentStatisticsDTO.PatientId, deleteAppointmentStatisticsDTO.AppointmentScheduleId, doctorId, cancellationToken) == null) throw new Exception("Kayıtlı Randevu Raporu Bulunamadı.");

            AppointmentStatistics appointmentStatistics = _mapper.Map<AppointmentStatistics>(deleteAppointmentStatisticsDTO);
            appointmentStatistics.DoctorId = doctorId;

            await _appointmentStatisticsRepository.DeleteAppointmentStatistics(appointmentStatistics, cancellationToken);
        }

        public async Task<IEnumerable<object>> GetAllPatientAppointmentStatisticsByDoctorUserName(string doctorUserName, CancellationToken cancellationToken)
        {
            return await _appointmentStatisticsRepository.GetAllPatientAppointmentStatisticsByDoctorUserName(doctorUserName, cancellationToken);
        }

        public async Task<IEnumerable<object>> GetAllPatientAppointmentStatisticsByPatientUserName(string patientUserName, string doctorId, CancellationToken cancellationToken)
        {
            return await _appointmentStatisticsRepository.GetAllPatientAppointmentStatisticsByPatientUserName(patientUserName, doctorId, cancellationToken);
        }

        public async Task<IEnumerable<object>> GetAllPatientAppointmentStatisticsByPatientUserName(string patientUserName, CancellationToken cancellationToken)
        {
            return await _appointmentStatisticsRepository.GetAllPatientAppointmentStatisticsByPatientUserName(patientUserName, cancellationToken);
        }

        public async Task<IEnumerable<object>> GetAllPatientAppointmentStatistics(CancellationToken cancellationToken)
        {
            return await _appointmentStatisticsRepository.GetAllPatientAppointmentStatistics(cancellationToken);
        }
    }
}
