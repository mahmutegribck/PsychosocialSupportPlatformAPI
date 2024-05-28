using Microsoft.ML.Data;

namespace PsychosocialSupportPlatformAPI.Business.MLModel
{
    public class SentimentPrediction
    {
        [ColumnName("PredictedLabel")]
        public string Prediction { get; set; }
    }
}
