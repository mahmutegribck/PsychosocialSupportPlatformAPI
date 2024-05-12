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


        public async Task AddAppointmentScheduleList(List<AppointmentSchedule> appointmentSchedules)
        {
            await _context.AppointmentSchedules.AddRangeAsync(appointmentSchedules);
            await _context.SaveChangesAsync();
        }


        public async Task DeleteAppointmentScheduleList(IEnumerable<AppointmentSchedule> appointmentSchedules)
        {
            _context.AppointmentSchedules.RemoveRange(appointmentSchedules);
            await _context.SaveChangesAsync();
        }


        public async Task<IEnumerable<object>> GetAllAppointmentSchedules(DateTime day)
        {
            var mergedSchedules = await _context.AppointmentSchedules.AsNoTracking().Where(s => s.Day == day.Date)
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
                    DoctorTitle = a.Doctor.Title

                }).ToListAsync();

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


        public async Task<IEnumerable<AppointmentSchedule>> GetAppointmentScheduleByDay(string doctorId, DateTime day)
        {
            return await _context.AppointmentSchedules.AsNoTracking().Where(a => a.DoctorId == doctorId && a.Day == day).ToListAsync();
        }




        public async Task<AppointmentSchedule?> GetAppointmentScheduleById(int appointmentScheduleId)
        {
            return await _context.AppointmentSchedules.AsNoTracking().Where(a => a.Id == appointmentScheduleId).FirstOrDefaultAsync();
        }

        public async Task UpdateAppointmentSchedule(AppointmentSchedule appointmentSchedule)
        {
            _context.AppointmentSchedules.Update(appointmentSchedule);
            await _context.SaveChangesAsync();
        }

        public async Task<AppointmentSchedule?> GetAppointmentScheduleByDayAndTimeRange(string doctorId, DateTime day, TimeRange timeRange)
        {
            return await _context.AppointmentSchedules.AsNoTracking().Where(a => a.DoctorId == doctorId && a.Day == day && a.TimeRange == timeRange && a.Status == false).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<AppointmentSchedule>> AllDoctorAppointments(string doctorId)
        {
            return await _context.AppointmentSchedules.Include(a => a.Patient).Where(a => a.DoctorId == doctorId && a.PatientId != null).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<AppointmentSchedule>> GetAllDoctorAppointmentsByPatientId(string patientId, string doctorId)
        {
            return await _context.AppointmentSchedules.Include(a => a.Patient).Where(a => a.DoctorId == doctorId && a.PatientId == patientId).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<AppointmentSchedule>> GetAllPastDoctorAppointmentsByPatientSlug(string patientSlug, string doctorId)
        {
            return await _context.AppointmentSchedules.Include(a => a.Patient).Where(a => a.DoctorId == doctorId && a.Patient != null && a.Patient.UserName == patientSlug && a.Day < DateTime.Now.Date && (int)a.TimeRange < DateTime.Now.Hour).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<AppointmentSchedule>> GetAllDoctorAppointmentsByDate(DateTime day, string doctorId)
        {
            return await _context.AppointmentSchedules.Include(a => a.Patient).Where(a => a.Day == day && a.DoctorId == doctorId && a.PatientId != null).AsNoTracking().ToListAsync();
        }
    }
}
