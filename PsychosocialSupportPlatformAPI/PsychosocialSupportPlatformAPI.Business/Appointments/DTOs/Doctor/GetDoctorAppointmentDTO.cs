using PsychosocialSupportPlatformAPI.Entity.Enums;

namespace PsychosocialSupportPlatformAPI.Business.Appointments.DTOs.Doctor
{
    public class GetDoctorAppointmentDTO
    {
        public int Id { get; set; }
        public DateTime Day { get; set; }
        public TimeRange TimeRange { get; set; }
        public required string URL { get; set; }
        public required string PatientId { get; set; }
        public required string PatientName { get; set; }
        public required string PatientSurname { get; set; }

    }
}
