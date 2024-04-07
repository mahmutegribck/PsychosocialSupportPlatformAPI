using PsychosocialSupportPlatformAPI.Entity.Entities.Users;
using System.ComponentModel.DataAnnotations;

namespace PsychosocialSupportPlatformAPI.Entity.Entities
{
    public class VideoStatistics
    {
        [Key]
        public int Id { get; set; }
        public int ClicksNumber { get; set; }
        public double ViewingRate { get; set; }

        public Video Video { get; set; }
        public int VideoId { get; set; }
        public Patient Patient { get; set; }
        public string PatientId { get; set; }

    }
}
