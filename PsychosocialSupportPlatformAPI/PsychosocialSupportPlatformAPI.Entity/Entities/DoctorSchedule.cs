using PsychosocialSupportPlatformAPI.Entity.Entities.Users;
using PsychosocialSupportPlatformAPI.Entity.Enums;

namespace PsychosocialSupportPlatformAPI.Entity.Entities
{
    public class DoctorSchedule
    {
        public int Id { get; set; }
        public DayOfWeek Day { get; set; }
        public bool EightToNine { get; set; }
        public bool NineToTen { get; set; }
        public bool TenToEleven { get; set; }
        public bool ElevenToTwelve { get; set; }
        public bool TwelveToThirteen { get; set; }
        public bool ThirteenToFourteen { get; set; }
        public bool FourteenToFifteen { get; set; }
        public bool FifteenToSixteen { get; set; }
        public bool SixteenToSeventeen { get; set; }
        public string DoctorId { get; set; }
        public Doctor Doctor { get; set; }

    }
}
