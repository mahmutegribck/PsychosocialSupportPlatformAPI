using System.ComponentModel.DataAnnotations;

namespace PsychosocialSupportPlatformAPI.Business.Users.DTOs.PatientDTOs
{
    public class UpdatePatientDTO
    {
        [Required(ErrorMessage = "İsim zorunlu")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "Soyisim zorunlu")]
        public required string Surname { get; set; }

        [Required(ErrorMessage = "Telefon numarası zorunlu")]
        public required string PhoneNumber { get; set; }
    }
}
