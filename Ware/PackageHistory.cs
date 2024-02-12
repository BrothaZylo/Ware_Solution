using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using static Ware.DeliverySchedule;

namespace Ware
{
    /// <summary>
    /// The class will save when i package arrived to a location(the shelf, terminal.. etc)
    /// </summary>
    public class PackageHistory
    {
        private Dictionary<string, List<(string, DateTime)>> PackageLog = new();
        private List<(string, DateTime)> LocationAndTime = new();

        /// <summary>
        /// AddPackageLog() will check if the package already exist. If it dont then it will add the package as a key and its location and when it got there
        /// if it already exists will the new location and when it got there be added to the package
        /// </summary>
        /// <param name="packageID">This is the id of the package object</param>
        /// <param name="action">Tjis is the location it arrived at</param>
        /// <returns></returns>
        public string AddPackageLog(string packageID, string action)
        {
            if (!PackageLog.ContainsKey(packageID))
            {
                PackageLog.Add(packageID, new List<(string, DateTime)>());

                PackageLog[packageID].Add((action, DateTime.Now));

                return packageID + " Was logged";
            }

            PackageLog[packageID].Add((action, DateTime.Now));

            return packageID + " Was logged";
        }
        /// <summary>
        /// This will print out all the packages and where they have been and when they got there
        /// </summary>
        public void GetPackageLog()
        {
            foreach (KeyValuePair<string, List<(string, DateTime)>> keys in PackageLog)
            {
                Console.WriteLine($"PackageID: {keys.Key}");
                foreach ((string, DateTime) items in keys.Value)
                {
                    Console.WriteLine($"{items.Item1} {items.Item2}");
                }
            }
        
        }
        public List<(string, DateTime)> GetLocationAndTime()
        {
            return LocationAndTime;
        }
        /*
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
                    Console.WriteLine($"ID: {items.Key.packageid}   " +
                        $"      Name: {items.Key.name}" +
                        $"      Type: {items.Key.goods}" +
                        $"      Speed: {items.Key.speed}" +
                        $"      Height: {items.Key.height}" +
                        $"      Time Arrived: {items.Value.DeliveryTime}" +
                        $"      Time Sent out: {items.Value.PickupTime}");
                }

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

            // Returns info about a specific charachter
            public void OnePackageHistory(string packageId)
            {
                foreach (KeyValuePair<CreatePackage, (DateTime DeliveryTime, DateTime PickupTime)> item in History)
                {
                    CreatePackage checkPackage = item.Key;


                    if (checkPackage.packageid == packageId)
                    {
                        Console.WriteLine($"ID: {checkPackage.packageid}" +
                            $"      Name: {checkPackage.name}" +
                            $"      Type: {checkPackage.goods}" +
                            $"      Speed: {checkPackage.speed}" +
                            $"      Height: {checkPackage.height}" +
                            $"      Time Arrived: {item.Value.DeliveryTime}" +
                            $"      Time Sent out{item.Value.PickupTime}");
                    }
                }
            }
        }*/
    }
}