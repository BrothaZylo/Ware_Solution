using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Ware.Schedule;

namespace Ware
{
    internal interface ISchedule
    {
        void AddPackage(string SingleOrRepeating, DayOfWeek day, Package package, DateTime deliveryTime, DateTime pickupTime);
        void ClearSchedule();
        Dictionary<DaysOfWeek, List<(string, Package, DateTime, DateTime)>> GetSchedule();
        bool CheckDay(DayOfWeek day);
        List<(string, Package, DateTime, DateTime)> GetPackageDay(DayOfWeek day);

    }
}
