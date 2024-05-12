using Microsoft.AspNetCore.Identity;

namespace PsychosocialSupportPlatformAPI.Entity.Entities.Users
{
    public class ApplicationUser : IdentityUser<string>
    {

        public required string Name { get; set; }
        public required string Surname { get; set; }
        public string? ProfileImageUrl { get; set; }
        public string? ProfileImagePath { get; set; }


        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenEndDate { get; set; }

        public ICollection<Message> SentMessages { get; set; }
        public ICollection<Message> ReceivedMessages { get; set; }
    }
}
