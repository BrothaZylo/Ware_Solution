using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Ware.DeliverySchedule;

namespace Ware
{
    internal interface IDeliverySchedule
    {
        public void AddPackageToDay(string singleOrRepeating, DayOfWeek day, CreatePackage package, DateTime deliveryTime, DateTime pickupTime);
        public void ClearSchedule();
        public Dictionary<DaysOfWeek, List<(string, CreatePackage, DateTime, DateTime)>> GetCalender();

    }
}
