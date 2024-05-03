using Microsoft.EntityFrameworkCore;
using PsychosocialSupportPlatformAPI.Entity.Entities.Appointments;

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
                .AsNoTracking()
                .Where(a =>
                a.PatientId == appointmentSchedule.PatientId &&
                a.DoctorId == appointmentSchedule.DoctorId &&
                a.TimeRange == appointmentSchedule.TimeRange &&
                a.Day == appointmentSchedule.Day &&
                a.Status == true)
                .FirstOrDefaultAsync();
        }

        public async Task CancelPatientAppointment(AppointmentSchedule appointmentSchedule)
        {
            appointmentSchedule.Status = false;
            appointmentSchedule.PatientId = null;
            appointmentSchedule.URL = null;
            _context.AppointmentSchedules.Update(appointmentSchedule);
            await _context.SaveChangesAsync();
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
                        DoctorName = appointment.Doctor.Name,
                        DoctorSurname = appointment.Doctor.Surname,
                        DoctorTitle = appointment.Doctor.Title,
                        AppointmentURL = appointment.URL,
                        TimeRange = appointment.TimeRange
                    }).ToList()

                }).ToListAsync();
        }

        public async Task<AppointmentSchedule?> GetPatientAppointmentById(int patientAppointmentId, string patientId)
        {
            return await _context.AppointmentSchedules.AsNoTracking().Include(a => a.Doctor).Where(a => a.Id == patientAppointmentId && a.PatientId == patientId).FirstAsync();
        }
    }
}
