using System.ComponentModel.DataAnnotations;

namespace PsychosocialSupportPlatformAPI.Business.Auth.AuthService.DTOs
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Email adresi zorunlu")]
        [EmailAddress]
        public required string Email { get; set; }


        [Required(ErrorMessage = "Şifre zorunlu")]
        [DataType(DataType.Password)]
        public required string Password { get; set; }
    }
}
