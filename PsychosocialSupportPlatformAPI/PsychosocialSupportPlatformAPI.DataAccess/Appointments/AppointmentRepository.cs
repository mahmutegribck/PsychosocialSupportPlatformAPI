using Microsoft.EntityFrameworkCore;
using PsychosocialSupportPlatformAPI.Entity.Entities.Appointments;
using PsychosocialSupportPlatformAPI.Entity.Entities.Users;

namespace PsychosocialSupportPlatformAPI.DataAccess.Appointments
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly PsychosocialSupportPlatformDBContext _context;

        public AppointmentRepository(PsychosocialSupportPlatformDBContext context)
        {
            _context = context;
        }
        public async Task<AppointmentSchedule?> GetPatientAppointment(AppointmentSchedule appointmentSchedule)
        {
            return await _context.AppointmentSchedules
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .Where(a =>
                a.PatientId == appointmentSchedule.PatientId &&
                a.DoctorId == appointmentSchedule.DoctorId &&
                a.TimeRange == appointmentSchedule.TimeRange &&
                a.Day == appointmentSchedule.Day &&
                a.Status == true)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task<AppointmentSchedule?> GetDoctorAppointment(AppointmentSchedule appointmentSchedule)
        {
            return await _context.AppointmentSchedules
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .Where(a =>
                a.DoctorId == appointmentSchedule.DoctorId &&
                a.TimeRange == appointmentSchedule.TimeRange &&
                a.Day == appointmentSchedule.Day)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task CancelPatientAppointment(AppointmentSchedule appointmentSchedule)
        {
            appointmentSchedule.Status = false;
            appointmentSchedule.PatientId = null;
            appointmentSchedule.Patient = null;
            appointmentSchedule.URL = null;
            _context.AppointmentSchedules.Update(appointmentSchedule);
            await _context.SaveChangesAsync();
        }

        public async Task CancelDoctorAppointment(AppointmentSchedule appointmentSchedule)
        {
            _context.AppointmentSchedules.Remove(appointmentSchedule);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Doctor>> GetPatientDoctorsByPatientId(string patientId)
        {
            return await _context.AppointmentSchedules.Where(a => a.PatientId == patientId).Select(a => a.Doctor).Distinct().ToListAsync();
        }

        public async Task<IEnumerable<object>> GetPatientAppointmentsByPatientId(string patientId)
        {
            return await _context.AppointmentSchedules
                .Include(a => a.Doctor)
                .AsNoTracking()
                .Where(a => a.PatientId == patientId)
                .GroupBy(a => a.Day.Date)
                .OrderBy(group => group.Key)
                .Select(group => new
                {
                    Day = group.Key.ToShortDateString(),
                    Appointments = group.Select(appointment => new
                    {
                        AppointmentId = appointment.Id,
                        Day = group.Key.ToShortDateString(),
                        TimeRange = appointment.TimeRange,
                        DoctorId = appointment.DoctorId,
                        DoctorName = appointment.Doctor.Name,
                        DoctorSurname = appointment.Doctor.Surname,
                        DoctorTitle = appointment.Doctor.DoctorTitle.Title,
                        AppointmentURL = appointment.URL
                    }).ToList()

                }).ToListAsync();
        }

        public async Task<AppointmentSchedule?> GetPatientAppointmentById(int patientAppointmentId, string patientId)
        {
            return await _context.AppointmentSchedules.AsNoTracking().Include(a => a.Doctor).Where(a => a.Id == patientAppointmentId && a.PatientId == patientId).FirstAsync();
        }

        public async Task<bool> CheckPatientAppointment(int appointmentScheduleId, string patientId, string doctorId)
        {
            return await _context.AppointmentSchedules.AnyAsync(a => a.Id == appointmentScheduleId && a.PatientId == patientId && a.DoctorId == doctorId);
        }
    }
}
