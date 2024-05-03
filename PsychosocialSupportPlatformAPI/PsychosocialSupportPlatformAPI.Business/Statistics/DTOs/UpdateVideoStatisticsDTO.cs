namespace PsychosocialSupportPlatformAPI.Business.Statistics.DTOs
{
    public class UpdateVideoStatisticsDTO
    {
        public int Id { get; set; }
        public int ClicksNumber { get; set; }
        public decimal ViewingRate { get; set; }
        public int VideoId { get; set; }


    }
}
