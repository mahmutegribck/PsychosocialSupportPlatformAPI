using System.ComponentModel.DataAnnotations;

namespace PsychosocialSupportPlatformAPI.Entity.Entities
{
    public class Video
    {
        public Video()
        {
            Statistics = new HashSet<VideoStatistics>();
        }
        [Key]
        public int Id { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public required string Url { get; set; }
        public required string Path { get; set; }

        public ICollection<VideoStatistics> Statistics { get; set; }
    }
}
