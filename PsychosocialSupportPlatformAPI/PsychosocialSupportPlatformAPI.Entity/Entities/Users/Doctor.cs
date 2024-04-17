using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsychosocialSupportPlatformAPI.Entity.Entities.Users
{
    public class Doctor : ApplicationUser
    {
        public Doctor()
        {
            Appointments = new HashSet<Appointment>();
            DoctorSchedules = new HashSet<DoctorSchedule>();
        }
        public required string Title { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
        public ICollection<DoctorSchedule> DoctorSchedules { get; set; }
    }
}
