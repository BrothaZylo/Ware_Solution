using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ware.Scheduler
{
    public class ScheduledPackage
    {
        private Package package;
        private string time;
        private DayOfWeek day;

        public ScheduledPackage(Package packageToSchedule, string timeSchedule, DayOfWeek dayOfWeek)
        {
            package = packageToSchedule;
            time = timeSchedule;
            day = dayOfWeek;
        }

        public Package GetPackage
        {
            get { return package; }
            set { package = value; }
        }

        public string Time
        {
            get { return time; }
            set { time = value; }
        }

        public DayOfWeek Day
        {
            get { return day; }
            set { day = value; }
        }
    }

}
