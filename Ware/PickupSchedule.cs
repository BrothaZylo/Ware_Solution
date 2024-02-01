using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ware
{
    public class PickupSchedule
    {
        private List<(CreatePackage package, DateTime pickupTime)>DailyPickupPackages;
        private List<(CreatePackage package, DateTime pickupTime)>WeeklyPickupPackages;


        public PickupSchedule() 
        {
            DailyPickupPackages = new List<(CreatePackage package, DateTime pickupTime)>();
            WeeklyPickupPackages = new List<(CreatePackage package, DateTime pickupTime)>();
        }
        public void addDailyPickupPackages(CreatePackage package, DateTime time)
        {
            DailyPickupPackages.Add((package, time));
        }
        public void addWeeklyPickupPackages(CreatePackage package, DateTime time)
        {
            WeeklyPickupPackages.Add((package, time));
        }

        public List<(CreatePackage, DateTime)> getDailyPickupPackages()
        {
            return DailyPickupPackages;
        }
        public List<(CreatePackage, DateTime)> getWeeklyPickupPackages() 
        {
            return WeeklyPickupPackages;
        }

    }
}

    }
}
