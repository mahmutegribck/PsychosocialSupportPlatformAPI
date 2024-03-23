using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsychosocialSupportPlatformAPI.Business.Videos.DTOs
{
    public class UploadVideoDTO
    {
        [Required(ErrorMessage = "Video Zorunlu. Lütfen Video Giriniz.")]
        public required IFormFile File { get; set; }

        [Required(ErrorMessage = "Video Başlığı Zorunlu. Lütfen Başlık Giriniz.")]
        public required string Title { get; set; }
        public string? Description { get; set; }
    }
}
