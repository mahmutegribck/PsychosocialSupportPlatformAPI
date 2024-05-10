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

        public async Task<AppointmentStatistics?> GetAppointmentStatisticsById(int appointmentStatisticsId, string patientId, int appointmentScheduleId, string doctorId)
        {
            return await _context.AppointmentStatistics.AsNoTracking().Where(s => s.Id == appointmentStatisticsId && s.PatientId == patientId && s.DoctorId == doctorId && s.AppointmentScheduleId == appointmentScheduleId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<object>> GetAllPatientAppointmentStatisticsByDoctorId(string doctorId)
        {
            var statistics = await _context.AppointmentStatistics.Include(s => s.Patient).Include(s => s.AppointmentSchedule).Where(s => s.DoctorId == doctorId).AsNoTracking().ToListAsync();

            var groupedStatistics = statistics.GroupBy(s => new { s.Patient.Id, s.Patient.Name, s.Patient.Surname }).Select(group => new
            {
                PatientId = group.Key.Id,
                PatientName = group.Key.Name,
                PatientSurname = group.Key.Surname,
                Statistics = group.Select(s => new
                {
                    AppointmentStatisticId = s.Id,
                    AppointmentStartTime = s.AppointmentStartTime,
                    AppointmentEndTime = s.AppointmentEndTime,
                    AppointmentComment = s.AppointmentComment,
                    AppointmentDay = s.AppointmentSchedule.Day.ToShortDateString()
                })
            });

            return groupedStatistics;
        }

        public async Task<IEnumerable<AppointmentStatistics>> GetAllPatientAppointmentStatisticsByPatientId(string doctorId, string patientId)
        {
            return await _context.AppointmentStatistics.Include(s => s.Patient).Include(s => s.AppointmentSchedule).Where(s => s.DoctorId == doctorId && s.PatientId == patientId).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<AppointmentStatistics>> GetAllPatientAppointmentStatisticsByPatientId(string patientId)
        {
            return await _context.AppointmentStatistics.Include(s => s.Patient).Include(s => s.AppointmentSchedule).Where(s => s.PatientId == patientId).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<object>> GetAllPatientAppointmentStatistics()
        {
            var statistics = await _context.AppointmentStatistics
                .Include(s => s.Patient)
                .Include(s => s.Doctor)
                .Include(s => s.AppointmentSchedule)
                .AsNoTracking()
                .ToListAsync();

            var groupedByDoctor = statistics
                .GroupBy(s => s.DoctorId)
                .Select(doctorGroup =>
                {
                    var doctor = doctorGroup.First().Doctor;
                    var patientsGroupedByDoctor = doctorGroup
                        .GroupBy(s => s.PatientId)
                        .Select(patientGroup =>
                        {
                            var patient = patientGroup.First().Patient;
                            return new
                            {
                                PatientId = patient.Id,
                                PatientName = patient.Name,
                                PatientSurname = patient.Surname,
                                AppointmentStatistics = patientGroup.Select(appointmentStatistics => new
                                {
                                    AppointmentStatisticsId = appointmentStatistics.Id,
                                    AppointmentStartTime = appointmentStatistics.AppointmentStartTime,
                                    AppointmentEndTime = appointmentStatistics.AppointmentEndTime,
                                    AppointmentComment = appointmentStatistics.AppointmentComment,
                                    AppointmentDay = appointmentStatistics.AppointmentSchedule.Day.ToShortDateString()
                                }).ToArray()
                            };
                        }).ToArray();

                    return new
                    {
                        DoctorId = doctor.Id,
                        DoctorName = doctor.Name,
                        DoctorSurname = doctor.Surname,
                        Patients = patientsGroupedByDoctor
                    };
                }).ToArray();


            return groupedByDoctor;
        }

        public async Task<bool> CheckPatientAppointmentStatistics(int appointmentScheduleId)
        {
            return await _context.AppointmentStatistics.AnyAsync(s => s.AppointmentScheduleId == appointmentScheduleId);
        }
    }
}
