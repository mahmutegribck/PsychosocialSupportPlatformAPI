using PsychosocialSupportPlatformAPI.Entity.Entities.Appointments;

namespace PsychosocialSupportPlatformAPI.Entity.Entities.Users
{
    public class Doctor : ApplicationUser
    {
        public Doctor()
        {
            DoctorSchedules = new HashSet<DoctorSchedule>();
            AppointmentSchedules = new HashSet<AppointmentSchedule>();
            AppointmentStatistics = new HashSet<AppointmentStatistics>();

        }
        public bool Confirmed { get; set; } = false;

        public ICollection<DoctorSchedule> DoctorSchedules { get; set; }
        public ICollection<AppointmentSchedule> AppointmentSchedules { get; set; }
        public ICollection<AppointmentStatistics> AppointmentStatistics { get; set; }
        public int DoctorTitleId { get; set; }
        public DoctorTitle DoctorTitle { get; set; }

    }
}
