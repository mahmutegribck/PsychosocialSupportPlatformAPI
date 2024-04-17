using PsychosocialSupportPlatformAPI.Entity.Enums;

namespace PsychosocialSupportPlatformAPI.Business.DoctorSchedules.DTOs
{
    public class GetDoctorScheduleDTO
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public TimeRange TimeRange { get; set; }

    }
}
