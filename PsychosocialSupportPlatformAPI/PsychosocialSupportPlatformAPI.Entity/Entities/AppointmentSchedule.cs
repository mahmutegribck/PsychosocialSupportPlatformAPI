using PsychosocialSupportPlatformAPI.Entity.Entities.Users;
using PsychosocialSupportPlatformAPI.Entity.Enums;
using System.ComponentModel.DataAnnotations;

namespace PsychosocialSupportPlatformAPI.Entity.Entities
{
    public class AppointmentSchedule
    {
        public AppointmentSchedule()
        {
            Appointments = new HashSet<Appointment>();
        }

        [Key]
        public int Id { get; set; }
        public DateTime Day { get; set; }
        public TimeRange TimeRange { get; set; } 
        public bool Status { get; set; }
        public string DoctorId { get; set; }
        public Doctor Doctor { get; set; }  




        public ICollection<Appointment> Appointments { get; set; }

    }
}
