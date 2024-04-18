namespace PsychosocialSupportPlatformAPI.Entity.Entities.Users
{
    public class Patient : ApplicationUser
    {
        public Patient()
        {
            Appointments = new HashSet<Appointment>();
            Statistics = new HashSet<VideoStatistics>();
         
        }
        public ICollection<Appointment> Appointments { get; set; }
        public ICollection<VideoStatistics> Statistics { get; set; }
      




    }
}
