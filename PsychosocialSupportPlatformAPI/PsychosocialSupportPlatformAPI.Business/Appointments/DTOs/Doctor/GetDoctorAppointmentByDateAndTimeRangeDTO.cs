using PsychosocialSupportPlatformAPI.Entity.Enums;

namespace PsychosocialSupportPlatformAPI.Business.Appointments.DTOs.Doctor
{
    public class GetDoctorAppointmentByDateAndTimeRangeDTO
    {
        public required string DoctorId { get; set; }
        public required string Day { get; set; }
        public TimeRange TimeRange { get; set; }
    }
}
