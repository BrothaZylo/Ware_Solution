using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Ware
{   
    /// <summary>
    /// A class that creates a calender schedule for delivery
    /// </summary>
    public class Schedule : ISchedule
    {
        /// <summary>
        /// Creates an enum containing the days sunday to saturday that will be used as keys for the calender/dictionary
        /// </summary>
        public enum DaysOfWeek
        {
            Sunday,
            Monday,
            Tuesday,
            Wednesday,
            Thursday,
            Friday,
            Saturday
        }
        
        private Dictionary<DaysOfWeek, List<(string,Package, DateTime)>> calender;
        
        /// <summary>
        /// When creating a Schedule will it run PrepareDictinary() that will create 
        /// the calender with the 7 days as keys
        /// </summary>
        public Schedule()
        {
            PrepareDictionary();
        }

        /// <summary>
        /// Checks for which day the package is coming in and adds package and time to the calender
        /// </summary>
        /// <param name="day">What day the package is coming</param>
        /// <param name="package">The package being delivered</param>
        /// <param name="deliveryTime">The date and time it will arrive </param>
        /// <param name="pickupTime">The date and time it will be picked up </param>
        /// Steve. “Adding Items to a List in a Dictionary.” Stack Overflow, 2024,
        /// stackoverflow.com/questions/14991688/adding-items-to-a-dictionary-in-a-dictionary.
        /// Author Steve
        public void AddPackage(string singleOrRepeating, DayOfWeek day, Package package, DateTime pickupTime)
        {
            if (calender.ContainsKey((DaysOfWeek)day))
            {
                calender[(DaysOfWeek)day].Add((singleOrRepeating, package, pickupTime));  
                RaisePackageAddEvent(package);
            }
        }

        /// <summary>
        /// When run the method will remove all the packages that only arrive once and not repeated daily/weekly
        /// </summary>
        public void ClearSchedule()
        {
            List<(string, Package, DateTime)> tmp = new List<(string, Package, DateTime)>();
            foreach (KeyValuePair<DaysOfWeek, List<(string, Package, DateTime)>> days in calender)
            {
                
                foreach ((string, Package, DateTime) items in days.Value)
                {
                    if (items.Item1 == "Single")
                    {
                        tmp.Add(items);
                    }
                }
                foreach (var j in tmp)
                {
                    calender[days.Key].Remove(j);
                }

                tmp.Clear();      
            }

        }

        /// <summary>
        /// When run the method will print out the calender that contains the day it will be sent out and
        /// the packages connected to the day.
        /// </summary>
        /// <returns>Returns a Console write that tells us the days and what packages will come that day</returns>
        public Dictionary<DaysOfWeek, List<(string, Package,  DateTime)>> GetSchedule()
        {
            foreach (KeyValuePair<DaysOfWeek, List<(string, Package, DateTime)>> keys in calender)
            {
                Console.WriteLine($"Weekday: {keys.Key}");
                foreach ((string, Package, DateTime) items in keys.Value) 
                {
                    Console.WriteLine($"Single or Repeat: {items.Item1}   " +
                    $"   Package ID: {items.Item2.PackageId}" +
                    $"   Expected Pickup Time: {items.Item3}");

                }
            }
            return calender;
        }

        /// <summary>
        /// Returns a boolean depending on if the day exists and if it contains any packages
        /// 
        /// Joe. “How to Get the Integer Value of Day of Week.” Stack Overflow, 2024,
        /// stackoverflow.com/questions/9199080/how-to-get-the-integer-value-of-day-of-week.
        /// </summary>
        /// <param name="day">Day of week</param>
        /// <returns>Return a bool that tells if the day contains anything</returns>
        public bool CheckDay(DayOfWeek day)
        {
            DaysOfWeek deliveryDay = (DaysOfWeek)(int)day;
           
            return calender.ContainsKey((DaysOfWeek)day) && calender[(DaysOfWeek)day].Any();
        }

        /// <summary>
        /// Returns a dictionary of packages for the day asked for
        /// </summary>
        /// <param name="day">Day of week</param>
        /// <returns>returns the packages for the day asked for</returns>
        public List<(string, Package, DateTime)> GetPackageDay(DayOfWeek day)
        {
            DaysOfWeek deliveryDay = (DaysOfWeek)(int)day;

            return calender[deliveryDay];
        }

        /// <summary>
        /// When PrepareDictionary is called will it create a dictionary called calender and add the days monday-sunday
        /// as keys inside the dictionary with an empty dictionary as its value
        /// visc. “Creating Dictionaries with Pre-Defined Keys C#.” Stack Overflow, 2024,
        /// stackoverflow.com/questions/26160503/creating-dictionaries-with-pre-defined-keys-c-sharp.
        /// Author visc
        /// </summary>
        private void PrepareDictionary()
        {
            calender = new Dictionary<DaysOfWeek, List<(string, Package, DateTime)>>();

            foreach (DaysOfWeek day in Enum.GetValues(typeof(DaysOfWeek)))
            {
                calender.Add(day, new List<(string, Package, DateTime)>());
            }

        }

        /// <summary>
        /// Used for AddPackage(string singleOrRepeating, DayOfWeek day, Package package, DateTime pickupTime)
        /// </summary>
        public event EventHandler<PackageEventArgs>? PackageAddEvent;

        private void RaisePackageAddEvent(Package package)
        {
            PackageAddEvent?.Invoke(this, new PackageEventArgs(package));
        }
    }
}