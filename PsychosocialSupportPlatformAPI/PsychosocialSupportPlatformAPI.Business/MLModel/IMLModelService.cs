using PsychosocialSupportPlatformAPI.Business.MLModel.DTOs;

namespace PsychosocialSupportPlatformAPI.Business.MLModel
{
    public interface IMLModelService
    {
        Task CreateMLModel(UploadDataSetDTO uploadDataSetDTO);

        Task<string?> GetMessagePrediction(string message);
    }
}
