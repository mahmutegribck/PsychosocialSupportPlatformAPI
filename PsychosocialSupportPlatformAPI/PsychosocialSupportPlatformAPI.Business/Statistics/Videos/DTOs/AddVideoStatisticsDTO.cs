namespace PsychosocialSupportPlatformAPI.Business.Statistics.Videos.DTOs
{
    public class AddVideoStatisticsDTO
    {
        public int VideoId { get; set; }
        public int ClicksNumber { get; set; }
        public decimal ViewingRate { get; set; }
    }
}
