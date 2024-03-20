using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsychosocialSupportPlatformAPI.Business.Videos.DTOs
{
    public class UploadVideoDTO
    {
        public required IFormFile File { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
    }
}
