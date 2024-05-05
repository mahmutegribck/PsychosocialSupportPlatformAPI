namespace PsychosocialSupportPlatformAPI.Business.Statistics.Videos.DTOs
{
    public class GetVideoStatisticsDTO
    {
        public int Id { get; set; }
        public int ClicksNumber { get; set; }
        public decimal ViewingRate { get; set; }
        public int VideoId { get; set; }


    }
}
