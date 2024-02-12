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
        void AddPackageToDay(string singleOrRepeating, DayOfWeek day, CreatePackage package, DateTime deliveryTime, DateTime pickupTime);
        void ClearSchedule();
        Dictionary<DaysOfWeek, List<(string, CreatePackage, DateTime, DateTime)>> GetCalender();
        bool HasPackagesThisDay(DayOfWeek day);
        List<(string, CreatePackage, DateTime, DateTime)> GetPackagesForToday(DayOfWeek day);

    }
}
