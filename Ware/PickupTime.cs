using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ware
{
    class PickupTime
    {
        public DateTime PickupSchedule;
        public List<CreatePackage> Packages;

        public PickupTime(DateTime pickupSchedule, List<CreatePackage> packages)
        {
            PickupSchedule = pickupSchedule;
            Packages = packages;
        }
    }
}
