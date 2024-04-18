using PsychosocialSupportPlatformAPI.Entity.Enums;
using System.ComponentModel;

namespace PsychosocialSupportPlatformAPI.Business.DoctorSchedules.DTOs
{
    public class CreateDoctorScheduleDTO
    {
        public required DayOfWeek Day { get; set; }
        public required List<TimeRange> TimeRanges { get; set; }

    }
}
