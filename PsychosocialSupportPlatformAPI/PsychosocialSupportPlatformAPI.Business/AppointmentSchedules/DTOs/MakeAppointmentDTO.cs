using PsychosocialSupportPlatformAPI.Entity.Enums;

namespace PsychosocialSupportPlatformAPI.Business.AppointmentSchedules.DTOs
{
    public class MakeAppointmentDTO
    {
        public required string DoctorId { get; set; }
        public required string Day { get; set; }
        public TimeRange TimeRange { get; set; }
    }
}
