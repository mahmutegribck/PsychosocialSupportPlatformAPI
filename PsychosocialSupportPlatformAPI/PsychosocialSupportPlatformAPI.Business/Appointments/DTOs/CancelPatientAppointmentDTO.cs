using PsychosocialSupportPlatformAPI.Entity.Enums;

namespace PsychosocialSupportPlatformAPI.Business.Appointments.DTOs
{
    public class CancelPatientAppointmentDTO
    {
        public required string Day { get; set; } 
        public TimeRange TimeRange { get; set; }
        public required string DoctorId { get; set; }
    }
}
