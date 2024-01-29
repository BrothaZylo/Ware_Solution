using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ware
{
    public class PackageHistory
    {
        private Dictionary<CreatePackage, (DateTime DeliveryTime, DateTime PickupTime)> History;

        public PackageHistory(Dictionary<CreatePackage, (DateTime DeliveryTime, DateTime PickupTime)> history)
        {
            History = history;
        }
        // Adds deliverytime to a package
        public void DeliveryHistory(CreatePackage packages, DateTime deliveryTime)
        {
            // Creates the log for deliverytime and using DateTime.MinValue as a placeholder for PickupTime
            History.Add(packages,(deliveryTime, DateTime.MinValue));
        }
        // Adds pickuptime to a package
        public void PickTime(CreatePackage package, DateTime pickupTime)
        {
            if (History.ContainsKey(package))
            {   
                var (deliveryTime, _) = History[package];
                History[package] = (deliveryTime, pickupTime);
            }
        }
        public void SeveralDelivery(List<CreatePackage> severalPackages, DateTime deliveryTime)
        {
            foreach (var items in severalPackages)
            {
                DeliveryHistory(items,deliveryTime);
            }
        }
        public void SeveralPickup(List<CreatePackage> severalPackages, DateTime deliveryTime)
        {
            foreach (var items in severalPackages)
            {
                PickTime(items, deliveryTime);
            }

        }


        // Returning history of all packages
        /*
        public Dictionary<CreatePackage, (DateTime DeliveryTime, DateTime PickupTime)> AllHistory()
        {
            return new Dictionary<CreatePackage, (DateTime DeliveryTime, DateTime PickupTime)>(History);
        }
        */





        // Returning a specific package
        public List<(CreatePackage package, DateTime deliveryTime, DateTime pickupTime)> OnePackageHistory(string packageId)
        {
            List<(CreatePackage package, DateTime deliveryTime, DateTime pickupTime)> packageHistory = new List<(CreatePackage package, DateTime deliveryTime, DateTime pickupTime)>();
            foreach (var item in History)
            {
                CreatePackage checkPackage = item.Key;
                DateTime delivery = item.Value.DeliveryTime;
                DateTime pickup = item.Value.PickupTime;
                
                if(checkPackage.packageid == packageId)
                {
                    packageHistory.Add((checkPackage, delivery, pickup));
                }
            }
            return packageHistory;
           
        }
    }
}
