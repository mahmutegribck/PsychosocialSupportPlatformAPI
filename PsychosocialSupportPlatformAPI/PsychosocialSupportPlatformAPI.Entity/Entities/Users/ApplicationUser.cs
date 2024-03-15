using Microsoft.AspNetCore.Identity;
using PsychosocialSupportPlatformAPI.Entity.Entities.Messages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsychosocialSupportPlatformAPI.Entity.Entities.Users
{
    public class ApplicationUser : IdentityUser<string>
    {

        public required string Name { get; set; }
        public required string Surname { get; set; }
        public DateTime Birthday { get; set; }
        public string? ProfileImageUrl { get; set; }
        public string Provider { get; set; }

        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenEndDate { get; set; }



        //public ICollection<Message> Messages { get; set; }
        //[InverseProperty("Sender")]
        public ICollection<Message> SentMessages { get; set; }

        //[InverseProperty("Receiver")]
        public ICollection<Message> ReceivedMessages { get; set; }
    }
}
