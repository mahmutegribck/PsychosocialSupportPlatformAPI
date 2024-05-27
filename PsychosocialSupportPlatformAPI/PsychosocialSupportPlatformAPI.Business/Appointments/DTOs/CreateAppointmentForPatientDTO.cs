using PsychosocialSupportPlatformAPI.Entity.Enums;

namespace PsychosocialSupportPlatformAPI.Business.Appointments.DTOs
{
    public class CreateAppointmentForPatientDTO
    {
        public required string PatientUserName { get; set; }
        public required string Day { get; set; }
        public TimeRange TimeRange { get; set; }
    }
}
