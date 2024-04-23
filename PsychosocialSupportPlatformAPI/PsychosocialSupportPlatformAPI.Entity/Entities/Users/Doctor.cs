using PsychosocialSupportPlatformAPI.Entity.Entities.Appointments;

namespace PsychosocialSupportPlatformAPI.Entity.Entities.Users
{
    public class Doctor : ApplicationUser
    {
        public Doctor()
        {
            DoctorSchedules = new HashSet<DoctorSchedule>();
            AppointmentSchedules = new HashSet<AppointmentSchedule>();
        }
        public required string Title { get; set; }
        public ICollection<DoctorSchedule> DoctorSchedules { get; set; }
        public ICollection<AppointmentSchedule> AppointmentSchedules { get; set; }

    }
}
