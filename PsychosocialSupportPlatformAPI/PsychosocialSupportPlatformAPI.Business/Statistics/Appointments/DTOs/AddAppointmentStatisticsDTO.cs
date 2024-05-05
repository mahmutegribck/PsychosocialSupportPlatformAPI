namespace PsychosocialSupportPlatformAPI.Business.Statistics.Appointments.DTOs
{
    public class AddAppointmentStatisticsDTO
    {
        public required string AppointmentStartTime { get; set; }
        public required string AppointmentEndTime { get; set; }
        public required string AppointmentComment { get; set; }

        public int AppointmentScheduleId { get; set; }
        public required string PatientId { get; set; }

    }
}
