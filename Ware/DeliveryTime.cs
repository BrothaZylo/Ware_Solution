using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ware
{
    class DeliveryTime
    {
        public DateTime DeliverySchedule;
        public List<CreatePackage> Packages;

        public DeliveryTime(DateTime deliverySchedule, List<CreatePackage> packages)
        {
            DeliverySchedule = deliverySchedule;
            Packages = packages;
        }

    }
}


