using PsychosocialSupportPlatformAPI.Entity.Enums;

namespace PsychosocialSupportPlatformAPI.Business.DoctorSchedules.DTOs
{
    public class UpdateDoctorScheduleDTO
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public List<TimeRange> TimeRanges { get; set; }

    }
}
