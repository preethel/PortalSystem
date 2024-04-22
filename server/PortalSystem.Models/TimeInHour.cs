using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalSystem.Models
{
    public class TimeInHour
    {
        public int Hour { get; set; }
        
        public int Minute { get; set; }
        
        public int Sec { get; set; }
    }
}
