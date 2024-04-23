using PsychosocialSupportPlatformAPI.Entity.Enums;

namespace PsychosocialSupportPlatformAPI.Business.DoctorSchedules.DTOs
{
    public class CreateDoctorScheduleDTO
    {
        public required string Day { get; set; }
        public required List<TimeRange> TimeRanges { get; set; }

    }
}
