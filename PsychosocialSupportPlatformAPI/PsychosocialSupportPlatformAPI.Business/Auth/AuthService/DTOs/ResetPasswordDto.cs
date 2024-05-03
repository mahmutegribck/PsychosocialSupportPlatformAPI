using System.ComponentModel.DataAnnotations;

namespace PsychosocialSupportPlatformAPI.Business.Auth.AuthService.DTOs
{
    public class ResetPasswordDto
    {
        // [Required]
        //public string Token { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string NewPassword { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string ConfirmPassword { get; set; }
    }
}
