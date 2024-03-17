using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsychosocialSupportPlatformAPI.Entity.Entities.Users
{
    public class Doctor : ApplicationUser
    {
        
        public required string Title { get; set; }

        public ICollection<Appointment> Appointments { get; set; }
    }
}
