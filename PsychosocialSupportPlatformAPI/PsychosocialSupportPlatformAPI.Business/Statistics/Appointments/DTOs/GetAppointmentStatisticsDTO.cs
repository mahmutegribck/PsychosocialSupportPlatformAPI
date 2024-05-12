namespace PsychosocialSupportPlatformAPI.Business.Statistics.Appointments.DTOs
{
    public class GetAppointmentStatisticsDTO
    {
        public int Id { get; set; }
        public required string AppointmentDay { get; set; }
        public required string AppointmentStartTime { get; set; }
        public required string AppointmentEndTime { get; set; }
        public required string AppointmentComment { get; set; }
        public required string DoctorName { get; set; }
        public required string DoctorSurname { get; set; }
        public required string DoctorTitle { get; set; }
        public required string DoctorProfileImageUrl { get; set; }


    }
}
