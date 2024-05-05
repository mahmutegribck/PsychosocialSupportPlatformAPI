using Microsoft.EntityFrameworkCore;
using PsychosocialSupportPlatformAPI.Entity.Entities.Appointments;

namespace PsychosocialSupportPlatformAPI.DataAccess.Statistics.Appointments
{
    public class AppointmentStatisticsRepository : IAppointmentStatisticsRepository
    {
        private readonly PsychosocialSupportPlatformDBContext _context;

        public AppointmentStatisticsRepository(PsychosocialSupportPlatformDBContext context)
        {
            _context = context;
        }

        public async Task AddAppointmentStatistics(AppointmentStatistics appointmentStatistics)
        {
            await _context.AppointmentStatistics.AddAsync(appointmentStatistics);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAppointmentStatistics(AppointmentStatistics appointmentStatistics)
        {
            _context.AppointmentStatistics.Update(appointmentStatistics);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAppointmentStatistics(AppointmentStatistics appointmentStatistics)
        {
            _context.AppointmentStatistics.Remove(appointmentStatistics);
            await _context.SaveChangesAsync();
        }

        public async Task<AppointmentStatistics?> GetAppointmentStatisticsById(int appointmentStatisticsId, string patientId, string doctorId)
        {
            return await _context.AppointmentStatistics.AsNoTracking().Where(s => s.Id == appointmentStatisticsId && s.PatientId == patientId && s.DoctorId == doctorId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<AppointmentStatistics>> GetAllPatientAppointmentStatisticsByDoctorId(string doctorId)
        {
            return await _context.AppointmentStatistics.Include(s => s.Patient).Include(s => s.AppointmentSchedule).Where(s => s.DoctorId == doctorId).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<AppointmentStatistics>> GetAllPatientAppointmentStatisticsByPatientId(string doctorId, string patientId)
        {
            return await _context.AppointmentStatistics.Include(s => s.Patient).Include(s => s.AppointmentSchedule).Where(s => s.DoctorId == doctorId && s.PatientId == patientId).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<object>> GetAllPatientAppointmentStatistics()
        {
            var statistics = await _context.AppointmentStatistics
                .Include(s => s.Patient)
                .Include(s => s.Doctor)
                .AsNoTracking()
                .ToListAsync();

            var groupedByDoctor = statistics.GroupBy(s => s.DoctorId).Select(group =>
            {
                var doctor = group.First().Doctor; // Assuming all entries in the group have the same doctor
                var patients = group.Select(s => s.Patient).ToArray();
                return new
                {
                    DoctorId = doctor.Id,
                    DoctorName = doctor.Name,
                    DoctorSurname = doctor.Surname,
                    Patients = patients.Select(patient => new
                    {
                        PatientId = patient.Id,
                        PatientName = patient.Name,
                        PatientSurname = patient.Surname,
                        AppointmentStatistics = patient.AppointmentStatistics.Select(appointmentStatistics => new
                        {
                            AppointmentStatisticsId = appointmentStatistics.Id,
                            AppointmentStartTime = appointmentStatistics.AppointmentStartTime,
                            AppointmentEndTime = appointmentStatistics.AppointmentEndTime,
                            AppointmentComment = appointmentStatistics.AppointmentComment
                        })
                        // Add other patient properties you want to include
                    }).ToArray()
                };
            });

            return groupedByDoctor;
        }
    }
}
