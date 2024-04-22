using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalSystem.Models
{
    public class _Timing
    {
        public List<DayOfWeek> DaysOfWeek { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public _Timing(List<DayOfWeek> daysOfWeek, TimeSpan startTime, TimeSpan endTime)
        {
            DaysOfWeek = daysOfWeek;
            StartTime = startTime;
            EndTime = endTime;
        }

        public override string ToString()
        {
            string daysOfWeekString = string.Join(", ", DaysOfWeek);
            return $"{daysOfWeekString} {StartTime.ToString(@"hh\:mm")} - {EndTime.ToString(@"hh\:mm")}";
        }
    }
}
