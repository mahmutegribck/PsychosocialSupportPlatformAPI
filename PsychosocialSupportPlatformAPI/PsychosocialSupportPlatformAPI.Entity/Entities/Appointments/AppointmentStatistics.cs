using PsychosocialSupportPlatformAPI.Entity.Entities.Users;
using System.ComponentModel.DataAnnotations;

namespace PsychosocialSupportPlatformAPI.Entity.Entities.Appointments
{
    public class AppointmentStatistics
    {
        [Key]
        public int Id { get; set; }
        public required string AppointmentStartTime { get; set; }
        public required string AppointmentEndTime { get; set; }
        public required string AppointmentComment { get; set; }

        public int AppointmentScheduleId { get; set; }
        public AppointmentSchedule AppointmentSchedule { get; set; }
        public required string PatientId { get; set; }
        public Patient Patient { get; set; }
        public required string DoctorId { get; set; }
        public Doctor Doctor { get; set; }
    }
}
