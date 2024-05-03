namespace PsychosocialSupportPlatformAPI.Business.DoctorSchedules.DTOs
{
    internal class GetDoctorScheduleByAdminDTO
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
        public required string DoctorName { get; set; }
        public required string DoctorSurname { get; set; }
        public required string DoctorTitle { get; set; }
        public string? DoctorProfileImageUrl { get; set; }
    }
}
