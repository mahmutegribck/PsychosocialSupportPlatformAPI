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
            if (!await _appointmentRepository.CheckPatientAppointment(addAppointmentStatisticsDTO.AppointmentScheduleId, addAppointmentStatisticsDTO.PatientId, doctorId)) throw new Exception("Hasta Randevu Kaydı Bulunamadı.");

            if (await _appointmentStatisticsRepository.CheckPatientAppointmentStatistics(addAppointmentStatisticsDTO.AppointmentScheduleId)) throw new Exception("Randevuya Ait Rapor Kaydı Bulunmaktadır.");

            AppointmentStatistics appointmentStatistics = _mapper.Map<AppointmentStatistics>(addAppointmentStatisticsDTO);
            appointmentStatistics.DoctorId = doctorId;

            await _appointmentStatisticsRepository.AddAppointmentStatistics(appointmentStatistics);
        }

        public async Task UpdateAppointmentStatistics(UpdateAppointmentStatisticsDTO updateAppointmentStatisticsDTO, string doctorId)
        {
            if (await _appointmentStatisticsRepository.GetAppointmentStatisticsById(updateAppointmentStatisticsDTO.Id, updateAppointmentStatisticsDTO.PatientId, updateAppointmentStatisticsDTO.AppointmentScheduleId, doctorId) == null) throw new Exception("Kayıtlı Randevu Raporu Bulunamadı.");

            AppointmentStatistics appointmentStatistics = _mapper.Map<AppointmentStatistics>(updateAppointmentStatisticsDTO);
            appointmentStatistics.DoctorId = doctorId;

            await _appointmentStatisticsRepository.UpdateAppointmentStatistics(appointmentStatistics);
        }

        public async Task DeleteAppointmentStatistics(DeleteAppointmentStatisticsDTO deleteAppointmentStatisticsDTO, string doctorId)
        {
            if (await _appointmentStatisticsRepository.GetAppointmentStatisticsById(deleteAppointmentStatisticsDTO.Id, deleteAppointmentStatisticsDTO.PatientId, deleteAppointmentStatisticsDTO.AppointmentScheduleId, doctorId) == null) throw new Exception("Kayıtlı Randevu Raporu Bulunamadı.");

            AppointmentStatistics appointmentStatistics = _mapper.Map<AppointmentStatistics>(deleteAppointmentStatisticsDTO);
            appointmentStatistics.DoctorId = doctorId;

            await _appointmentStatisticsRepository.DeleteAppointmentStatistics(appointmentStatistics);
        }

        public async Task<IEnumerable<object>> GetAllPatientAppointmentStatisticsByDoctorId(string doctorId)
        {
            return await _appointmentStatisticsRepository.GetAllPatientAppointmentStatisticsByDoctorId(doctorId);
        }

        public async Task<IEnumerable<GetAppointmentStatisticsDTO>> GetAllPatientAppointmentStatisticsByPatientId(string doctorId, string patientId)
        {
            return _mapper.Map<IEnumerable<GetAppointmentStatisticsDTO>>(await _appointmentStatisticsRepository.GetAllPatientAppointmentStatisticsByPatientId(doctorId, patientId));
        }

        public async Task<IEnumerable<GetAppointmentStatisticsDTO>> GetAllPatientAppointmentStatisticsByPatientId(string patientId)
        {
            return _mapper.Map<IEnumerable<GetAppointmentStatisticsDTO>>(await _appointmentStatisticsRepository.GetAllPatientAppointmentStatisticsByPatientId(patientId));
        }

        public async Task<IEnumerable<object>> GetAllPatientAppointmentStatistics()
        {
            return await _appointmentStatisticsRepository.GetAllPatientAppointmentStatistics();
        }
    }
}
