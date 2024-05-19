namespace PsychosocialSupportPlatformAPI.Entity.Entities.Users
{
    public class DoctorTitle
    {
        public DoctorTitle()
        {
            Doctors = new HashSet<Doctor>();
        }
        public int Id { get; set; }
        public required string Title { get; set; }

        public ICollection<Doctor> Doctors { get; set; }
    }
}
