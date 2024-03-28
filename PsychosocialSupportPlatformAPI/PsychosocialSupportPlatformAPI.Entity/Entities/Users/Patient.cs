using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsychosocialSupportPlatformAPI.Entity.Entities.Users
{
    public class Patient : ApplicationUser
    {
        public Patient()
        {
            Appointments = new HashSet<Appointment>();
            Statistics = new HashSet<VideoStatistics>();
        }
        public ICollection<Appointment> Appointments { get; set; }
        public ICollection<VideoStatistics> Statistics { get; set; }

    }
}
