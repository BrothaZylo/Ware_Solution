using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ware.Scheduler
{
    public class ScheduleRepeatingModule
    {
        private List<ScheduledPackage> repeatingPackages = new List<ScheduledPackage>();

        public ScheduleRepeatingModule()
        {
            repeatingPackages = [];
        }

        public void AddPackage(Package package, string time, DayOfWeek day)
        {
            repeatingPackages.Add(new ScheduledPackage(package, time, day));
        }

        public List<ScheduledPackage> GetModule()
        {
            return repeatingPackages;
        }
    }

}
