using Microsoft.ML.Data;

namespace PsychosocialSupportPlatformAPI.Business.MLModel
{
    public class SentimentData
    {
        [LoadColumn(0)]
        public string SentimentText;

        [LoadColumn(1), ColumnName("Label")]
        public string Sentiment;
    }
}
