using PsychosocialSupportPlatformAPI.Entity.Entities.Users;
using PsychosocialSupportPlatformAPI.Entity.Enums;
using System.ComponentModel.DataAnnotations;

namespace PsychosocialSupportPlatformAPI.Entity.Entities
{
    public class Appointment
    {

        [Key]
        public int Id { get; set; }
        public DateTime Day { get; set; }
        public TimeRange TimeRange { get; set; }
        public string URL { get; set; }
        public string DoctorId { get; set; }
        public Doctor Doctor { get; set; }
        public string PatientId { get; set; }
        public Patient Patient { get; set; }

        public int AppointmentScheduleId { get; set; }
        public AppointmentSchedule AppointmentSchedule { get; set; }


    }
}
