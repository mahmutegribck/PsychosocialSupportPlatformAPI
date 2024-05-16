using System.ComponentModel.DataAnnotations;

namespace PsychosocialSupportPlatformAPI.Business.Users.DTOs
{
    public class ChangePasswordDTO
    {
        public string OldPassword { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string NewPassword { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string ConfirmPassword { get; set; }
    }
}
