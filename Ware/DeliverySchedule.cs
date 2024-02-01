using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ware
{
    public class DeliverySchedule
    {
        private List<(CreatePackage package, DateTime deliveryTime)>DailyPackages;
        private List<(CreatePackage package, DateTime deliveryTime)>WeeklyPackages;


        public DeliverySchedule() 
        {
            DailyPackages = new List<(CreatePackage package, DateTime deliveryTime)>();
            WeeklyPackages = new List<(CreatePackage package, DateTime deliveryTime)>();
        }
        public void addDailyPackages(CreatePackage package, DateTime time)
        {
            DailyPackages.Add((package, time));
        }
        public void addWeeklyPackages(CreatePackage package, DateTime time)
        {
            WeeklyPackages.Add((package, time));
        }

        public List<(CreatePackage, DateTime)> getDailyPackages()
        {
            return DailyPackages;
        }
        public List<(CreatePackage, DateTime)> getWeeklyPackages() 
        {
            return WeeklyPackages;
        }

    }
}
