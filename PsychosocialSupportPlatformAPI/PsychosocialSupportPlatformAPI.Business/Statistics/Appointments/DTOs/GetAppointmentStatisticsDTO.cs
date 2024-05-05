namespace PsychosocialSupportPlatformAPI.Business.Statistics.Appointments.DTOs
{
    public class GetAppointmentStatisticsDTO
    {
        public int Id { get; set; }
        public required string AppointmentDay { get; set; }
        public required string AppointmentStartTime { get; set; }
        public required string AppointmentEndTime { get; set; }
        public required string AppointmentComment { get; set; }
        public string PatientId { get; set; }
        public string PatientName { get; set; }
        public string PatientSurname { get; set; }

    }
}
