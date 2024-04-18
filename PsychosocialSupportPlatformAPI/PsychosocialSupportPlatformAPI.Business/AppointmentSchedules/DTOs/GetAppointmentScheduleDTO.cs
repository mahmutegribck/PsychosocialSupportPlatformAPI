using PsychosocialSupportPlatformAPI.Entity.Entities.Users;
using PsychosocialSupportPlatformAPI.Entity.Enums;

namespace PsychosocialSupportPlatformAPI.Business.AppointmentSchedules.DTOs
{
    public class GetAppointmentScheduleDTO
    {
        public int Id { get; set; }
        public DateTime Day { get; set; }
        public TimeRange TimeRange { get; set; }
        public bool Status { get; set; }
        public string DoctorId { get; set; }
        public Doctor Doctor { get; set; }
    }
}
