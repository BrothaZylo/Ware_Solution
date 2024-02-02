using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ware
{
    public class PackageHistory
    {
        private Dictionary<CreatePackage, (DateTime DeliveryTime, DateTime PickupTime)> History { get; set; }
        private (CreatePackage pacakge, DateTime time) value { get; set; }

        public PackageHistory(Dictionary<CreatePackage, (DateTime DeliveryTime, DateTime PickupTime)> history)
        {
            History = history;
        }

        // Adds deliverytime to a package
        public void DeliveryHistory(CreatePackage packages, DateTime deliveryTime)
        {
            // Creates the log for deliverytime and using DateTime.MinValue as a placeholder for PickupTime
            History.Add(packages, (deliveryTime, DateTime.MinValue));
        }
        // Adds pickuptime to a package
        /*
        public void PickTime(CreatePackage package, DateTime pickupTime)
        {
            if (History.ContainsKey(package))
            {
                var (deliveryTime, _) = History[package];
                History[package] = (deliveryTime, pickupTime);
            }
        }*/

        public void PickTime(CreatePackage package, DateTime pickupTime)
        {
            if (History.ContainsKey(package))
            {
                var packageHistory = History[package];
                packageHistory.PickupTime = pickupTime;
                History[package] = packageHistory;
            }
        }



        // Method recieves a list of packages that will get there delivery time registered
        
        public void SeveralDelivery(List<CreatePackage> severalPackages, DateTime deliveryTime)
        {
            foreach (var items in severalPackages)
            {
                DeliveryHistory(items, deliveryTime);
            }
        }
        // Method recieves a list of packages that will get there pickup time registered
        public void SeveralPickup(List<CreatePackage> severalPackages, DateTime deliveryTime)
        {
            foreach (var items in severalPackages)
            {
                PickTime(items, deliveryTime);
            }

        }


        // Returning history of all packages as a dictionary
        public Dictionary<CreatePackage, (DateTime DeliveryTime, DateTime PickupTime)> AllHistoryAsADictionary()
        {
            return new Dictionary<CreatePackage, (DateTime DeliveryTime, DateTime PickupTime)>(History);
        }

        // Prints out all package history
        public void AllHistoryInfo()
        {
            var alllog = new Dictionary<CreatePackage, (DateTime DeliveryTime, DateTime PickupTime)>(History);
            foreach (var items in alllog)
            {
                Console.WriteLine($"ID: {items.Key.packageid}   " +
                    $"      Name: {items.Key.name}" +
                    $"      Type: {items.Key.goods}" +
                    $"      Speed: {items.Key.speed}" +
                    $"      Height: {items.Key.height}" +
                    $"      Time Arrived: {items.Value.DeliveryTime}" +
                    $"      Time Sent out: {items.Value.PickupTime}");
            }
        }

        // Returns info about a specific charachter
        public void OnePackageHistory(string packageId)
        {
            List<(CreatePackage package, DateTime deliveryTime, DateTime pickupTime)> packageHistory = new List<(CreatePackage package, DateTime deliveryTime, DateTime pickupTime)>();
            foreach (var item in History)
            {
                CreatePackage checkPackage = item.Key;


                if (checkPackage.packageid == packageId)
                {
                    Console.WriteLine($"{checkPackage.packageid}" +
                        $"      Name: {checkPackage.name}" +
                        $"      Type: {checkPackage.goods}" +
                        $"      Speed: {checkPackage.speed}" +
                        $"      Height: {checkPackage.height}" +
                        $"      Time Arrived: {item.Value.DeliveryTime}" +
                        $"      Time Sent out{item.Value.PickupTime}");
                }
            }
        }
    }
}