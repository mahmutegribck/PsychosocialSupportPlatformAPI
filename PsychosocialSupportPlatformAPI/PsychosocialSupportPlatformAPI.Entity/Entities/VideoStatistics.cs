using PsychosocialSupportPlatformAPI.Entity.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsychosocialSupportPlatformAPI.Entity.Entities
{
    public class VideoStatistics
    {
        public int Id { get; set; }
        public int ClicksNumber { get; set; }
        public decimal ViewingRate { get; set; }

        public Video Video { get; set; }
        public Patient Patient { get; set; }
        public string PatientId { get; set; }

    }
}
