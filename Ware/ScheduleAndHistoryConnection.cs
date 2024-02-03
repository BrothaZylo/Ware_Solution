using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Ware
{
    public class ScheduleAndHistoryConnection
    {
        private PackageHistory history;

        public ScheduleAndHistoryConnection(PackageHistory pacageHistory)
        {
            history = pacageHistory;
        }
        public void delivery(CreatePackage package, DateTime deliveryTime)
        {
            history.DeliveryHistory(package, deliveryTime);
        }
        public void pickup(CreatePackage package, DateTime pickupTime)
        {
            history.PickTime(package, pickupTime);
        }
    }
}
