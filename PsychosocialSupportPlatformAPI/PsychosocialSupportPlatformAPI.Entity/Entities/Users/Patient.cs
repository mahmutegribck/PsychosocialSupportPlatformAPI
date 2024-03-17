using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsychosocialSupportPlatformAPI.Entity.Entities.Users
{
    public class Patient : ApplicationUser
    {
        public DateTime PregnancyStartDate { get; set; }
        public ICollection<Appointment> Appointments { get; set; }

    }
}
