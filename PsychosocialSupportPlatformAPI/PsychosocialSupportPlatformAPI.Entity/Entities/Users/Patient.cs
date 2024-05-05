using PsychosocialSupportPlatformAPI.Entity.Entities.Appointments;
using PsychosocialSupportPlatformAPI.Entity.Entities.Videos;

namespace PsychosocialSupportPlatformAPI.Entity.Entities.Users
{
    public class Patient : ApplicationUser
    {
        public Patient()
        {
            AppointmentSchedules = new HashSet<AppointmentSchedule>();
            Statistics = new HashSet<VideoStatistics>();
            AppointmentStatistics = new HashSet<AppointmentStatistics>();
        }
        public ICollection<AppointmentSchedule> AppointmentSchedules { get; set; }
        public ICollection<VideoStatistics> Statistics { get; set; }
        public ICollection<AppointmentStatistics> AppointmentStatistics { get; set; }

    }
}
