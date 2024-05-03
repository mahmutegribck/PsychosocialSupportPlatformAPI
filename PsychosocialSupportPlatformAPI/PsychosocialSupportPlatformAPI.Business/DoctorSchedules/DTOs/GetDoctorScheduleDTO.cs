﻿namespace PsychosocialSupportPlatformAPI.Business.DoctorSchedules.DTOs
{
    public class GetDoctorScheduleDTO
    {
        public int Id { get; set; }
        public string Day { get; set; }
        public bool EightToNine { get; set; }
        public bool NineToTen { get; set; }
        public bool TenToEleven { get; set; }
        public bool ElevenToTwelve { get; set; }
        public bool TwelveToThirteen { get; set; }
        public bool ThirteenToFourteen { get; set; }
        public bool FourteenToFifteen { get; set; }
        public bool FifteenToSixteen { get; set; }
        public bool SixteenToSeventeen { get; set; }
        

    }
}
