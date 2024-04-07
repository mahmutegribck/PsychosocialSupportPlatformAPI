using System.ComponentModel.DataAnnotations;

namespace PsychosocialSupportPlatformAPI.Entity.Entities
{
    public class Video
    {
        [Key]
        public int Id { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public required string Url { get; set; }
        public required string Path { get; set; }

        public VideoStatistics? Statistics { get; set; }
    }
}
