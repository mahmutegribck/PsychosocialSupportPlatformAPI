using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsychosocialSupportPlatformAPI.Business.Videos.DTOs
{
    public class UploadMultipleVideoDTO
    {
        public List<IFormFile> Files { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
