using PsychosocialSupportPlatformAPI.Entity.Entities.Users;
using System.ComponentModel.DataAnnotations;

namespace PsychosocialSupportPlatformAPI.Entity.Entities.Appointments
{
    public class DoctorSchedule
    {
        [Key]
        public int Id { get; set; }
        public DateTime Day { get; set; }
        public bool EightToNine { get; set; }
        public bool NineToTen { get; set; }
        public bool TenToEleven { get; set; }
        public bool ElevenToTwelve { get; set; }
        public bool TwelveToThirteen { get; set; }
        public bool ThirteenToFourteen { get; set; }
        public bool FourteenToFifteen { get; set; }
        public bool FifteenToSixteen { get; set; }
        public bool SixteenToSeventeen { get; set; }

        public required string DoctorId { get; set; }
        public required Doctor Doctor { get; set; }

    }
}
