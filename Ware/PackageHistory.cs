using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ware
{
    public class PackageHistory(Dictionary<CreatePackage, (DateTime DeliveryTime, DateTime PickupTime)> history)
    {
        private Dictionary<CreatePackage, (DateTime DeliveryTime, DateTime PickupTime)> History { get; set; } = history;
        private (CreatePackage pacakge, DateTime time) value { get; set; }

        // Adds deliverytime to a package
        public void DeliveryHistory(CreatePackage packages, DateTime deliveryTime)
        {
            // Creates the log for deliverytime and using DateTime.MinValue as a placeholder for PickupTime
            History.Add(packages, (deliveryTime, DateTime.MinValue));
        }
        // Adds pickuptime to a package
        public void PickTime(CreatePackage package, DateTime pickupTime)
        {
            if (History.ContainsKey(package))
            {
                (DateTime, DateTime) packageHistory = History[package];
                packageHistory.Item2 = pickupTime;
                History[package] = packageHistory;
            }
        }

        // Method recieves a list of packages that will get there delivery time registered
        
        public void SeveralDelivery(List<CreatePackage> severalPackages, DateTime deliveryTime)
        {
            foreach (CreatePackage items in severalPackages)
            {
                DeliveryHistory(items, deliveryTime);
            }
        }
        // Method recieves a list of packages that will get there pickup time registered
        public void SeveralPickup(List<CreatePackage> severalPackages, DateTime deliveryTime)
        {
            foreach (CreatePackage items in severalPackages)
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
            foreach (KeyValuePair<CreatePackage, (DateTime DeliveryTime, DateTime PickupTime)> items in History)
            {
                Console.WriteLine($"ID: {items.Key.PackageId}   " +
                    $"      Name: {items.Key.Name}" +
                    $"      Type: {items.Key.Goods}" +
                    $"      Speed: {items.Key.SpeedOfDelivery}" +
                    $"      Height: {items.Key.Height}" +
                    $"      Time Arrived: {items.Value.DeliveryTime}" +
                    $"      Time Sent out: {items.Value.PickupTime}");
            }
        }

        // Returns info about a specific charachter
        public void OnePackageHistory(string packageId)
        {
            foreach (KeyValuePair<CreatePackage, (DateTime DeliveryTime, DateTime PickupTime)> item in History)
            {
                CreatePackage checkPackage = item.Key;


                if (checkPackage.PackageId == packageId)
                {
                    Console.WriteLine($"ID: {checkPackage.PackageId}" +
                        $"      Name: {checkPackage.Name}" +
                        $"      Type: {checkPackage.Goods}" +
                        $"      Speed: {checkPackage.SpeedOfDelivery}" +
                        $"      Height: {checkPackage.Height}" +
                        $"      Time Arrived: {item.Value.DeliveryTime}" +
                        $"      Time Sent out{item.Value.PickupTime}");
                }
            }
        }
    }
}