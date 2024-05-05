namespace PsychosocialSupportPlatformAPI.Business.Statistics.Appointments.DTOs
{
    public class DeleteAppointmentStatisticsDTO
    {
        public int Id { get; set; }
        public int AppointmentScheduleId { get; set; }
        public required string PatientId { get; set; }

    }
}
