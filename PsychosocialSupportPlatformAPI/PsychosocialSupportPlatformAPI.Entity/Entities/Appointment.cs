using PsychosocialSupportPlatformAPI.Entity.Entities.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsychosocialSupportPlatformAPI.Entity.Entities
{
    public class Appointment
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public string URL { get; set; }
        public string DoctorId { get; set; }
        public Doctor Doctor { get; set; }
        public string PatientId { get; set; }
        public Patient Patient { get; set; }
    }
}
