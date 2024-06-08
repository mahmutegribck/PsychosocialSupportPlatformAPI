using Microsoft.EntityFrameworkCore;
using PsychosocialSupportPlatformAPI.Entity.Entities.Appointments;
using PsychosocialSupportPlatformAPI.Entity.Enums;

namespace PsychosocialSupportPlatformAPI.DataAccess.AppointmentSchedules
{
    public class AppointmentScheduleRepository : IAppointmentScheduleRepository
    {
        private readonly PsychosocialSupportPlatformDBContext _context;
        public AppointmentScheduleRepository(PsychosocialSupportPlatformDBContext context)
        {
            _context = context;
        }


        public async Task AddAppointmentScheduleList(List<AppointmentSchedule> appointmentSchedules, CancellationToken cancellationToken)
        {
            await _context.AppointmentSchedules.AddRangeAsync(appointmentSchedules);
            await _context.SaveChangesAsync(cancellationToken);
        }


        public async Task AddAppointmentSchedule(AppointmentSchedule appointmentSchedule, CancellationToken cancellationToken)
        {
            await _context.AppointmentSchedules.AddAsync(appointmentSchedule, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }


        public async Task DeleteAppointmentScheduleList(IEnumerable<AppointmentSchedule> appointmentSchedules, CancellationToken cancellationToken)
        {
            _context.AppointmentSchedules.RemoveRange(appointmentSchedules);
            await _context.SaveChangesAsync(cancellationToken);
        }


        public async Task<IEnumerable<object>> GetAllAppointmentSchedules(DateTime day, string patientId, CancellationToken cancellationToken)
        {
            string? patientLastAppointmentDoctorId = await _context.AppointmentSchedules
                .AsNoTracking()
                .Where(a => a.PatientId == patientId)
                .OrderByDescending(a => a.Day)
                .Select(a => a.DoctorId)
                .FirstOrDefaultAsync(cancellationToken);


            var query = _context.AppointmentSchedules
                .AsNoTracking()
                .Where(s => s.Day == day.Date)
                .OrderBy(a => a.Day)
                .ThenBy(a => a.TimeRange)
                .Select(a => new
                {
                    a.Day,
                    a.TimeRange,
                    a.Status,
                    DoctorID = a.DoctorId,
                    DoctorName = a.Doctor.Name,
                    DoctorSurname = a.Doctor.Surname,
                    DoctorTitle = a.Doctor.DoctorTitle.Title
                });

            if (patientLastAppointmentDoctorId != null)
            {
                query = query.Where(s => s.DoctorID == patientLastAppointmentDoctorId);
            }

            var mergedSchedules = await query.ToListAsync(cancellationToken);

            var groupedSchedules = mergedSchedules
                .GroupBy(a => new { a.Day, a.DoctorID })
                .Select(group => new
                {
                    group.Key.Day,
                    group.Key.DoctorID,
                    Appointments = group.Select(a => new
                    {
                        a.TimeRange,
                        a.Status,
                        a.DoctorName,
                        a.DoctorSurname,
                        a.DoctorTitle
                    })
                })
                .GroupBy(a => a.Day)
                .Select(group => new
                {
                    Day = group.Key.ToShortDateString(),
                    Doctors = group.Select(d => new
                    {
                        DoctorID = d.DoctorID,
                        DoctorName = d.Appointments.FirstOrDefault()?.DoctorName,
                        DoctorSurname = d.Appointments.FirstOrDefault()?.DoctorSurname,
                        DoctorTitle = d.Appointments.FirstOrDefault()?.DoctorTitle,
                        Appointments = d.Appointments.Select(a => new
                        {
                            a.TimeRange,
                            a.Status
                        })
                    })
                });
            return groupedSchedules;
        }


        public async Task<IEnumerable<AppointmentSchedule>> GetAppointmentScheduleByDay(string doctorId, DateTime day, CancellationToken cancellationToken)
        {
            return await _context.AppointmentSchedules
                .AsNoTracking()
                .Include(a => a.Doctor)
                .Include(a => a.Patient)
                .Where(a => a.DoctorId == doctorId && a.Day == day)
                .ToListAsync(cancellationToken);
        }


        public async Task<AppointmentSchedule?> GetAppointmentScheduleById(int appointmentScheduleId, CancellationToken cancellationToken)
        {
            return await _context.AppointmentSchedules
                .AsNoTracking()
                .Where(a => a.Id == appointmentScheduleId)
                .FirstOrDefaultAsync(cancellationToken);
        }


        public async Task UpdateAppointmentSchedule(AppointmentSchedule appointmentSchedule, CancellationToken cancellationToken)
        {
            _context.AppointmentSchedules.Update(appointmentSchedule);
            await _context.SaveChangesAsync(cancellationToken);
        }


        public async Task<AppointmentSchedule?> GetAppointmentScheduleByDayAndTimeRange(string doctorId, DateTime day, TimeRange timeRange, CancellationToken cancellationToken)
        {
            return await _context.AppointmentSchedules
                .AsNoTracking()
                .Where(a =>
                    a.DoctorId == doctorId &&
                    a.Day == day &&
                    a.TimeRange == timeRange)
                .FirstOrDefaultAsync(cancellationToken);
        }


        public async Task<IEnumerable<AppointmentSchedule>> AllDoctorAppointments(string doctorId, CancellationToken cancellationToken)
        {
            return await _context.AppointmentSchedules
                .AsNoTracking()
                .Include(a => a.Patient)
                .Where(a =>
                    a.DoctorId == doctorId &&
                    a.PatientId != null)
                .ToListAsync(cancellationToken);
        }


        public async Task<IEnumerable<AppointmentSchedule>> GetAllDoctorAppointmentsByPatientId(string patientId, string doctorId, CancellationToken cancellationToken)
        {
            return await _context.AppointmentSchedules
                .AsNoTracking()
                .Include(a => a.Patient)
                .Where(a =>
                    a.DoctorId == doctorId &&
                    a.PatientId == patientId)
                .ToListAsync(cancellationToken);
        }


        public async Task<IEnumerable<AppointmentSchedule>> GetAllPastDoctorAppointmentsByPatientSlug(string patientSlug, string doctorId, CancellationToken cancellationToken)
        {
            return await _context.AppointmentSchedules
                .AsNoTracking()
                .Include(a => a.Patient)
                .Where(a =>
                    a.DoctorId == doctorId &&
                    a.Patient != null &&
                    a.Patient.UserName == patientSlug &&
                    a.Day <= DateTime.Now.Date &&
                    (int)a.TimeRange < DateTime.Now.Hour)
                .ToListAsync(cancellationToken);
        }


        public async Task<IEnumerable<AppointmentSchedule>> GetAllDoctorAppointmentsByDate(DateTime day, string doctorId, CancellationToken cancellationToken)
        {
            return await _context.AppointmentSchedules
                .AsNoTracking()
                .Include(a => a.Patient)
                .Where(a =>
                    a.Day == day &&
                    a.DoctorId == doctorId &&
                    a.PatientId != null)
                .ToListAsync(cancellationToken);
        }


        public async Task<AppointmentSchedule?> GetDoctorAppointmentByDateAndTimeRange(AppointmentSchedule appointmentSchedule, CancellationToken cancellationToken)
        {
            return await _context.AppointmentSchedules
                .AsNoTracking()
                .Include(a => a.Patient)
                .Where(a =>
                    a.Day == appointmentSchedule.Day &&
                    a.TimeRange == appointmentSchedule.TimeRange &&
                    a.DoctorId == appointmentSchedule.DoctorId &&
                    a.PatientId != null)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
