using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using static Ware.Schedule;

namespace Ware
{
    /// <summary>
    /// The class will save when i package arrived to a location(the shelf, terminal.. etc)
    /// </summary>
    public class PackageLogging : IPackageLogging
    {
        private Dictionary<string, List<(string, DateTime)>> PackageLog = new();
        private List<(string, DateTime)> LocationAndTime = new();

        /// <summary>
        /// AddPackageLog() will check if the package already exist. If it dont then it will add the package as a key and its location and when it got there
        /// if it already exists will the new location and when it got there be added to the package
        /// </summary>
        /// <param name="packageId">This is the id of the package object</param>
        /// <param name="action">Tjis is the location it arrived at</param>
        /// <returns>returns that the package has been logged</returns>
        public string AddPackageLog(string packageId, string action)
        {
            if (!PackageLog.ContainsKey(packageId))
            {
                PackageLog.Add(packageId, new List<(string, DateTime)>());

                PackageLog[packageId].Add((action, DateTime.Now));

                return packageId + " Was logged";
            }

            PackageLog[packageId].Add((action, DateTime.Now));

            return packageId + " Was logged";
        }
        /// <summary>
        /// This will print out all the packages and where they have been and when they got there
        /// </summary>
        public void LogsPrint()
        {
            foreach (KeyValuePair<string, List<(string, DateTime)>> keys in PackageLog)
            {
                Console.WriteLine($"PackageID: {keys.Key}");
                foreach ((string, DateTime) items in keys.Value)
                {
                    Console.WriteLine($"{items.Item1} {items.Item2}");
                    
                }
                Console.WriteLine();
            }
        
        }
        /// <summary>
        /// Finds package history of a single package
        /// </summary>
        /// <param name="id">package id</param>
        /// <returns>a stringbuilder that contains the log of the package asked for</returns>
        public StringBuilder TrackPackage(string id)
        {
            StringBuilder stringBuilder = new System.Text.StringBuilder();
            foreach (KeyValuePair<string, List<(string, DateTime)>> keys in PackageLog)
            {
                if (keys.Key == id)
                {
                    foreach ((string, DateTime) items in keys.Value)
                    {
                        stringBuilder.Append($"PackageID : {keys.Key} {items.Item1} {items.Item2}\n");
                    } 
                }
            }
            return stringBuilder;
        }


        /*
        public class PackageHistory(Dictionary<Package, (DateTime DeliveryTime, DateTime PickupTime)> history)
        {
            private Dictionary<Package, (DateTime DeliveryTime, DateTime PickupTime)> History { get; set; } = history;
            private (Package pacakge, DateTime time) value { get; set; }

            // Adds deliverytime to a package
            public void DeliveryHistory(Package packages, DateTime deliveryTime)
            {
                // Creates the log for deliverytime and using DateTime.MinValue as a placeholder for PickupTime
                History.Add(packages, (deliveryTime, DateTime.MinValue));
            }
            // Adds pickuptime to a package
            public void PickTime(Package package, DateTime pickupTime)
            {
                if (History.ContainsKey(package))
                {
                    (DateTime, DateTime) packageHistory = History[package];
                    packageHistory.Item2 = pickupTime;
                    History[package] = packageHistory;
                }
            }

            // Method recieves a list of packages that will get there delivery time registered


            public void SeveralDelivery(List<Package> severalPackages, DateTime deliveryTime)
            {
                foreach (Package items in severalPackages)
                {
                    DeliveryHistory(items, deliveryTime);
                }
            }
            // Method recieves a list of packages that will get there pickup time registered
            public void SeveralPickup(List<Package> severalPackages, DateTime deliveryTime)
            {
                foreach (Package items in severalPackages)
                {
                    PickTime(items, deliveryTime);
                }
            }



            // Returning history of all packages as a dictionary
            public Dictionary<Package, (DateTime DeliveryTime, DateTime PickupTime)> AllHistoryAsADictionary()
            {
                return new Dictionary<Package, (DateTime DeliveryTime, DateTime PickupTime)>(History);
            }


            // Prints out all package history
            public void AllHistoryInfo()
            {
                foreach (KeyValuePair<Package, (DateTime DeliveryTime, DateTime PickupTime)> items in History)
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
                foreach (KeyValuePair<Package, (DateTime DeliveryTime, DateTime PickupTime)> item in History)
                {
                    Package checkPackage = item.Key;


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