using PsychosocialSupportPlatformAPI.Entity.Enums;

namespace PsychosocialSupportPlatformAPI.Business.Appointments.DTOs
{
    public class CancelDoctorAppointmentDTO
    {
        public required string Day { get; set; }
        public TimeRange TimeRange { get; set; }
    }
}
