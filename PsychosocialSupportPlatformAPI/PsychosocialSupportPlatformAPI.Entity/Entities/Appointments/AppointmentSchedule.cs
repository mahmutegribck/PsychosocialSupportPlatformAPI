using PsychosocialSupportPlatformAPI.Entity.Entities.Users;
using PsychosocialSupportPlatformAPI.Entity.Enums;
using System.ComponentModel.DataAnnotations;

namespace PsychosocialSupportPlatformAPI.Entity.Entities.Appointments
{
    public class AppointmentSchedule
    {
        public AppointmentSchedule()
        {
            AppointmentStatistics = new HashSet<AppointmentStatistics>();
        }

        [Key]
        public int Id { get; set; }
        public DateTime Day { get; set; }
        public TimeRange TimeRange { get; set; }
        public bool Status { get; set; } = false;
        public string? URL { get; set; }

        public string? PatientId { get; set; }
        public Patient? Patient { get; set; }
        public required string DoctorId { get; set; }
        public Doctor Doctor { get; set; } = null!;

        public ICollection<AppointmentStatistics> AppointmentStatistics { get; set; }

    }
}
