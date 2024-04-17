using PsychosocialSupportPlatformAPI.Entity.Enums;

namespace PsychosocialSupportPlatformAPI.Business.DoctorSchedules.DTOs
{
    public class CreateDoctorScheduleDTO
    {
        public DateTime Date { get; set; }
        public List<TimeRange> TimeRanges { get; set; }

    }
}
