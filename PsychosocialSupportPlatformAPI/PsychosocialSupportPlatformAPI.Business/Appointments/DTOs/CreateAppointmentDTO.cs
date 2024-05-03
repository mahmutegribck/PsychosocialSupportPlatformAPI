using PsychosocialSupportPlatformAPI.Entity.Enums;

namespace PsychosocialSupportPlatformAPI.Business.Appointments.DTOs
{
    public class CreateAppointmentDTO
    {
        public DateTime Day { get; set; }
        public TimeRange TimeRange { get; set; }
        public required string DoctorId { get; set; }
        public required string PatientId { get; set; }
    }
}
