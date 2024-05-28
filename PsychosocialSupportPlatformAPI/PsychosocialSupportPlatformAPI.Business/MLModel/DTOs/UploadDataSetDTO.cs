using Microsoft.AspNetCore.Http;

namespace PsychosocialSupportPlatformAPI.Business.MLModel.DTOs
{
    public class UploadDataSetDTO
    {
        public required IFormFile DataSetFile { get; set; }
    }
}
