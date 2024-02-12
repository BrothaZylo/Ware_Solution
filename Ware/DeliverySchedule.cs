using System;
using System.Collections;
using System.Collections.Generic;
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
    public class DeliverySchedule : PackageHistory ,IDeliverySchedule
    {
        /// <summary>
        /// Creates an enum that will be used as keys for the calender/dictionary
        /// </summary>
        public enum DaysOfWeek
        {
            Monday,
            Tuesday,
            Wednesday,
            Thursday,
            Friday,
            Saturday,
            Sunday
        }

        private Dictionary<DaysOfWeek, List<(string,CreatePackage, DateTime, DateTime)>> calender;
        
        /// <summary>
        /// When creating a DeliverySchedule will it run PrepareDictinary() that will create 
        /// the calender with the 7 days as keys
        /// </summary>
        public DeliverySchedule()
        {
            PrepareDictionary();
        }
        /// <summary>
        /// When PrepareDictionary is called will it create a dictionary called calender and add the days monday-sunday
        /// as keys inside the dictionary with an empty list as its value
        /// visc. “Creating Dictionaries with Pre-Defined Keys C#.” Stack Overflow, 2024,
        /// stackoverflow.com/questions/26160503/creating-dictionaries-with-pre-defined-keys-c-sharp.
        /// Author visc
        /// </summary>
        public void PrepareDictionary()
        {
            calender = new Dictionary<DaysOfWeek, List<(string, CreatePackage, DateTime, DateTime)>>();

            foreach (DaysOfWeek day in Enum.GetValues(typeof(DaysOfWeek)))
            {
                calender.Add(day, new List<(string, CreatePackage, DateTime, DateTime)>());
            }

        }
        /// <summary>
        /// Checks for which day the package is coming in and adds package and time to the calender
        /// </summary>
        /// <param name="day">What day the package is coming</param>
        /// <param name="package">The package being delivered</param>
        /// <param name="deliveryTime">The date and time it will arrive </param>
        /// <param name="pickupTime">The date and time it will be picke up </param>
        /// Steve. “Adding Items to a List in a Dictionary.” Stack Overflow, 2024,
        /// stackoverflow.com/questions/14991688/adding-items-to-a-list-in-a-dictionary.
        /// Author Steve
        public void AddPackageToDay(string singleOrRepeating, DayOfWeek day, CreatePackage package,DateTime deliveryTime, DateTime pickupTime)
        {
            if (calender.ContainsKey((DaysOfWeek)day))
            {
                calender[(DaysOfWeek)day-1].Add((singleOrRepeating, package, deliveryTime, pickupTime));
               
                
                
            }
        }
        /// <summary>
        /// When run the method will remove all the packages that only arrive once and not repeated daily/weekly
        /// </summary>
        public void ClearSchedule()
        {
            List<(string, CreatePackage, DateTime, DateTime)> tmp = new List<(string, CreatePackage, DateTime, DateTime)>();
            foreach (KeyValuePair<DaysOfWeek, List<(string, CreatePackage, DateTime, DateTime)>> days in calender)
            {
                
                foreach ((string, CreatePackage, DateTime, DateTime) items in days.Value)
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
        /// <returns></returns>
        public Dictionary<DaysOfWeek, List<(string, CreatePackage, DateTime, DateTime)>> GetCalender()
        {
            foreach (KeyValuePair<DaysOfWeek, List<(string, CreatePackage, DateTime, DateTime)>> keys in calender)
            {
                Console.WriteLine($"Weekday: {keys.Key}");
                foreach ((string, CreatePackage, DateTime, DateTime) items in keys.Value) 
                {
                    Console.WriteLine($"Single or Repeat: {items.Item1}   " +
                    $"   Package ID: {items.Item2.PackageId}" +
                    $"   Delivery time: {items.Item3}" +
                    $"   Pickup time: {items.Item4}");

                }
            }
            return calender;
        }
    }

}