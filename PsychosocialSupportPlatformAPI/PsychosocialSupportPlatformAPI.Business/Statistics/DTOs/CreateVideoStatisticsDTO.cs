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
        public decimal ViewingRate { get; set; }

    }
}
