using PsychosocialSupportPlatformAPI.Entity.Enums;

namespace PsychosocialSupportPlatformAPI.Business.AppointmentSchedules.DTOs
{
    public class GetAppointmentScheduleDTO
    {
        public int Id { get; set; }
        public string Day { get; set; }
        public TimeRange TimeRange { get; set; }
        public string URL { get; set; }
        public string DoctorName { get; set; }
        public string DoctorSurname { get; set; }
        public string DoctorTitle { get; set; }

    }
}
