using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalSystem.Models
{
    public class Timing
    {
        
        public int[] DayOfWeek { get; set; }
        public TimeInHour StartTime { get; set; }
        public TimeInHour EndTime { get; set; }
    }
}
