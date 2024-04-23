using AutoMapper;
using PsychosocialSupportPlatformAPI.Business.Appointments.DTOs;
using PsychosocialSupportPlatformAPI.DataAccess.Appointments;
using PsychosocialSupportPlatformAPI.DataAccess.AppointmentSchedules;
using PsychosocialSupportPlatformAPI.DataAccess.DoctorSchedules;
using PsychosocialSupportPlatformAPI.Entity.Entities.Appointments;

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

            var existingAppointmentSchedule = await _appointmentScheduleRepository.GetAppointmentScheduleByTimeRange(createAppointmentDTO.DoctorId, createAppointmentDTO.TimeRange, createAppointmentDTO.Day.Date);


            //if (existingAppointmentSchedule != null) throw new Exception("Randevu Alınmış.");

            existingAppointmentSchedule.Status = true;
            await _appointmentScheduleRepository.UpdateAppointmentSchedule(existingAppointmentSchedule);

            var patientAppointment = _mapper.Map<Appointment>(createAppointmentDTO);
            //patientAppointment.AppointmentScheduleId = existingAppointmentSchedule.Id;

            //patientAppointment.Day = existingAppointmentSchedule.Day.Date;
            //patientAppointment.TimeRange = existingAppointmentSchedule.TimeRange;
            await _appointmentRepository.CreatePatientAppointment(patientAppointment);
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

        public async Task<IEnumerable<GetAppointmentDTO>> GetAllPatientAppointmentsByDoctor(string patientID, string doctorID)
        {
            return _mapper.Map<IEnumerable<GetAppointmentDTO>>(await _appointmentRepository.GetAllPatientAppointmentsByDoctor(patientID, doctorID));
        }
    }
}
