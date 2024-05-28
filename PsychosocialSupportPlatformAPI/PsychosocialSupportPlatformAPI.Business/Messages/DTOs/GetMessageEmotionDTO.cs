namespace PsychosocialSupportPlatformAPI.Business.Messages.DTOs
{
    public class GetMessageEmotionDTO
    {
        public double ScaredMessageRatio { get; set; }
        public double BoredMessageRatio { get; set; }
        public double HappyMessageRatio { get; set; }
        public double AngryMessageRatio { get; set; }
        public double SadMessageRatio { get; set; }
        public double EmergencyMessageRatio { get; set; }

    }
}
