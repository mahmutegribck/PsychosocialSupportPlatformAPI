using PsychosocialSupportPlatformAPI.Entity.Entities.Users;
using System.ComponentModel.DataAnnotations;

namespace PsychosocialSupportPlatformAPI.Entity.Entities.Videos
{
    public class VideoStatistics
    {
        [Key]
        public int Id { get; set; }
        public int ClicksNumber { get; set; }
        public double ViewingRate { get; set; }
        public Video Video { get; set; } = null!;
        public int VideoId { get; set; }
        public Patient Patient { get; set; } = null!;
        public required string PatientId { get; set; }

    }
}
