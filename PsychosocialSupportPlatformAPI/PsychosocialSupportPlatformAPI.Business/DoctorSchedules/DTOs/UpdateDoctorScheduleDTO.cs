using PsychosocialSupportPlatformAPI.Entity.Enums;
using System.ComponentModel;

namespace PsychosocialSupportPlatformAPI.Business.DoctorSchedules.DTOs
{
    public class UpdateDoctorScheduleDTO
    {
        public int Id { get; set; }
        public DateTime Day { get; set; }

        [DefaultValue(false)]
        public bool EightToNine { get; set; }

        [DefaultValue(false)]
        public bool NineToTen { get; set; }

        [DefaultValue(false)]
        public bool TenToEleven { get; set; }

        [DefaultValue(false)]
        public bool ElevenToTwelve { get; set; }

        [DefaultValue(false)]
        public bool TwelveToThirteen { get; set; }

        [DefaultValue(false)]
        public bool ThirteenToFourteen { get; set; }

        [DefaultValue(false)]
        public bool FourteenToFifteen { get; set; }

        [DefaultValue(false)]
        public bool FifteenToSixteen { get; set; }

        [DefaultValue(false)]
        public bool SixteenToSeventeen { get; set; }

    }
}
