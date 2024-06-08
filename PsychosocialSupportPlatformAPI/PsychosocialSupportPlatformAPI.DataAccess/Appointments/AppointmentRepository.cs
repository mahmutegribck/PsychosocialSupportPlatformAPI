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

        public async Task<AppointmentSchedule?> GetPatientAppointment(AppointmentSchedule appointmentSchedule, CancellationToken cancellationToken)
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
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<AppointmentSchedule?> GetDoctorAppointment(AppointmentSchedule appointmentSchedule, CancellationToken cancellationToken)
        {
            return await _context.AppointmentSchedules
                .AsNoTracking()
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .Where(a =>
                a.DoctorId == appointmentSchedule.DoctorId &&
                a.TimeRange == appointmentSchedule.TimeRange &&
                a.Day == appointmentSchedule.Day)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task CancelPatientAppointment(AppointmentSchedule appointmentSchedule, CancellationToken cancellationToken)
        {
            appointmentSchedule.Status = false;
            appointmentSchedule.PatientId = null;
            appointmentSchedule.Patient = null;
            appointmentSchedule.URL = null;
            _context.AppointmentSchedules.Update(appointmentSchedule);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task CancelDoctorAppointment(AppointmentSchedule appointmentSchedule, CancellationToken cancellationToken)
        {
            _context.AppointmentSchedules.Remove(appointmentSchedule);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<Doctor>> GetPatientDoctorsByPatientId(string patientId, CancellationToken cancellationToken)
        {
            return await _context.AppointmentSchedules
                .AsNoTracking()
                .Where(a => a.PatientId == patientId)
                .Select(a => a.Doctor)
                .Distinct()
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<object>> GetPatientAppointmentsByPatientId(string patientId, CancellationToken cancellationToken)
        {
            return await _context.AppointmentSchedules
                .AsNoTracking()
                .Include(a => a.Doctor)
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
                }).ToListAsync(cancellationToken);
        }

        public async Task<AppointmentSchedule?> GetPatientAppointmentById(int patientAppointmentId, string patientId, CancellationToken cancellationToken)
        {
            return await _context.AppointmentSchedules
                .AsNoTracking()
                .Include(a => a.Doctor)
                .Where(a => a.Id == patientAppointmentId && a.PatientId == patientId)
                .FirstAsync(cancellationToken);
        }

        public async Task<bool> CheckPatientAppointment(int appointmentScheduleId, string patientId, string doctorId, CancellationToken cancellationToken)
        {
            return await _context.AppointmentSchedules
                .AnyAsync(a =>
                    a.Id == appointmentScheduleId &&
                    a.PatientId == patientId &&
                    a.DoctorId == doctorId, cancellationToken);
        }

        public async Task<AppointmentSchedule?> GetPatientLastAppointment(string patientId, CancellationToken cancellationToken)
        {
            return await _context.AppointmentSchedules
                .AsNoTracking()
                .Where(a => a.PatientId == patientId)
                .OrderByDescending(a => a.Day)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
