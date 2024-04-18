namespace PsychosocialSupportPlatformAPI.Business.Appointments.DTOs
{
    public class GetAppointmentDTO
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public string URL { get; set; }
    }
}
