using PsychosocialSupportPlatformAPI.Business.MLModel.DTOs;

namespace PsychosocialSupportPlatformAPI.Business.MLModel
{
    public interface IMLModelService
    {
        Task CreateAIModel(UploadDataSetDTO uploadDataSetDTO);

        Task<string?> GetMessagePrediction(string message);
    }
}
