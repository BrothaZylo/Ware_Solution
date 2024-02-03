using System;
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
    public class DeliverySchedule
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

        private Dictionary<DaysOfWeek, List<(CreatePackage, DateTime)>> calender;
        
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
        /// https://stackoverflow.com/questions/26160503/creating-dictionaries-with-pre-defined-keys-c-sharp
        // Author visc
        /// </summary>
        public void PrepareDictionary()
        {
            calender = new Dictionary<DaysOfWeek, List<(CreatePackage, DateTime)>>();

            foreach (DaysOfWeek day in Enum.GetValues(typeof(DaysOfWeek)))
            {
                calender.Add(day, new List<(CreatePackage, DateTime)>());
            }
            foreach (KeyValuePair<DaysOfWeek, List<(CreatePackage, DateTime)>> item in calender)
            {
                Console.WriteLine(item.Key);
            }
        }
        //https://stackoverflow.com/questions/14991688/adding-items-to-a-list-in-a-dictionary
        //Author Steve
        public void AddPackageToDay(DayOfWeek day, CreatePackage package,DateTime time)
        {
            if (calender.ContainsKey((DaysOfWeek)day))
            {
                calender[(DaysOfWeek)day].Add((package,time));
            } 
        }

        public Dictionary<DaysOfWeek, List<(CreatePackage, DateTime)>> GetCalender()
        {
            foreach (KeyValuePair<DaysOfWeek, List<(CreatePackage, DateTime)>> keys in calender)
            {
                Console.WriteLine($"ID: {keys.Key}");
                foreach ((CreatePackage, DateTime) items in keys.Value) 
                {
                    Console.WriteLine($"ID: {items}   " +
                    $"      Name: {items.Item1.packageid}" +
                    $"      Type: {items.Item1.name}" +
                    $"      Speed: {items.Item1.goods}");

                }
            }
            return calender;
        }

    }

}