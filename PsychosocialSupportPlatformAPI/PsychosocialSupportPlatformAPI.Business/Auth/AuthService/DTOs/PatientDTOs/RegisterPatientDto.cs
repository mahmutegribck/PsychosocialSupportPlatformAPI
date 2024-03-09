using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsychosocialSupportPlatformAPI.Business.Auth.AuthService.DTOs.PatientDTOs
{
    public class RegisterPatientDto
    {
        [Required(ErrorMessage = "İsim zorunlu")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Soyisim zorunlu")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Email adresi zorunlu")]
        [EmailAddress(ErrorMessage = "Geçerli bir email adresi giriniz")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Şifre zorunlu")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Şifre onayı zorunlu")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Girmiş olduğunuz parola birbiri ile eşleşmiyor.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Gebelik başlangıç tarihi zorunlu")]
        [DataType(DataType.Date)]
        public DateTime PregnancyStartDate { get; set; }

        [Required(ErrorMessage = "Telefon numarası zorunlu")]
        //[Phone(ErrorMessage = "Geçerli bir telefon numarası giriniz")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Doğum tarihi zorunlu")]
        public DateTime Birthday { get; set; }
    }
}
