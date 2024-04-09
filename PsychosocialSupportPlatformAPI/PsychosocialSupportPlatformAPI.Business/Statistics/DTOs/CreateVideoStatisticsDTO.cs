using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsychosocialSupportPlatformAPI.Business.Statistics.DTOs
{
    public class CreateVideoStatisticsDTO
    {
        public int ClicksNumber { get; set; }
        public double ViewingRate { get; set; }
        public int VideoId { get; set; }


    }
}
