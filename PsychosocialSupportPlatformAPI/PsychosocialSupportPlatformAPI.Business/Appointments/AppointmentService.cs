using AutoMapper;
using PsychosocialSupportPlatformAPI.Business.Appointments.DTOs;
using PsychosocialSupportPlatformAPI.DataAccess.Appointments;
using PsychosocialSupportPlatformAPI.DataAccess.AppointmentSchedules;
using PsychosocialSupportPlatformAPI.DataAccess.DoctorSchedules;
using PsychosocialSupportPlatformAPI.Entity.Entities;

namespace PsychosocialSupportPlatformAPI.Business.Appointments
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IDoctorScheduleRepository _doctorScheduleRepository;
        private readonly IAppointmentScheduleRepository _appointmentScheduleRepository;
        private readonly IMapper _mapper;
        public AppointmentService(IAppointmentRepository appointmentRepository, IDoctorScheduleRepository doctorScheduleRepository, IMapper mapper, IAppointmentScheduleRepository appointmentScheduleRepository)
        {
            _appointmentRepository = appointmentRepository;
            _doctorScheduleRepository = doctorScheduleRepository;
            _mapper = mapper;
            _appointmentScheduleRepository = appointmentScheduleRepository;
        }

        public async Task CreatePatientAppointment(CreateAppointmentDTO createAppointmentDTO)
        {
            //var cexistingDoctorSchedule = await _doctorScheduleRepository.GetDoctorScheduleByTimeRange(createAppointmentDTO.DoctorId, createAppointmentDTO.TimeRange, createAppointmentDTO.Day.DayOfWeek);,

            var existingDoctorSchedule = await _appointmentScheduleRepository.GetAppointmentScheduleByTimeRange(createAppointmentDTO.DoctorId, createAppointmentDTO.TimeRange, createAppointmentDTO.Day);
            
            if (existingDoctorSchedule != null) throw new Exception("Randevu Alınmış.");

            await _appointmentRepository.CreatePatientAppointment(_mapper.Map<Appointment>(createAppointmentDTO));
        }

        public async Task DeletePatientAppointment(int appointmentID, string patientID)
        {
            var existingPatientAppointment = await _appointmentRepository.GetPatientAppointmentById(appointmentID, patientID);
            if (existingPatientAppointment == null) throw new Exception();

            await _appointmentRepository.DeletePatientAppointment(existingPatientAppointment);
        }

        public async Task<GetAppointmentDTO> GetDoctorAppointmentById(int appointmentID, string doctorID)
        {
            return _mapper.Map<GetAppointmentDTO>(await _appointmentRepository.GetDoctorAppointmentById(appointmentID, doctorID));
        }

        public async Task<GetAppointmentDTO> GetPatientAppointmentById(int appointmentID, string patientID)
        {
            return _mapper.Map<GetAppointmentDTO>(await _appointmentRepository.GetPatientAppointmentById(appointmentID, patientID));
        }

        public async Task<IEnumerable<GetAppointmentDTO>> GetAllDoctorAppointments(string doctorID)
        {
            return _mapper.Map<IEnumerable<GetAppointmentDTO>>(await _appointmentRepository.GetAllDoctorAppointments(doctorID));
        }

        public async Task<IEnumerable<GetAppointmentDTO>> GetAllPatientAppointments(string patientID)
        {
            return _mapper.Map<IEnumerable<GetAppointmentDTO>>(await _appointmentRepository.GetAllPatientAppointments(patientID));
        }

    }
}
