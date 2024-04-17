using System.ComponentModel;

namespace PsychosocialSupportPlatformAPI.Entity.Enums
{
    public enum TimeRange
    {
        [Description("08:00 - 09:00")]
        EightToNine = 8,

        [Description("09:00 - 10:00")]
        NineToTen,

        [Description("10:00 - 11:00")]
        TenToEleven,

        [Description("11:00 - 12:00")]
        ElevenToTwelve,

        [Description("12:00 - 13:00")]
        TwelveToThirteen,

        [Description("13:00 - 14:00")]
        ThirteenToFourteen,

        [Description("14:00 - 15:00")]
        FourteenToFifteen,

        [Description("15:00 - 16:00")]
        FifteenToSixteen,

        [Description("16:00 - 17:00")]
        SixteenToSeventeen
    }
}
