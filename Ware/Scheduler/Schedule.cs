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

namespace Ware.Scheduler
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

        private Dictionary<DaysOfWeek, List<ScheduledPackage>> calender;
        private ScheduleRepeatingModule scheduleRepeating;

        public Schedule(ScheduleRepeatingModule? repeatingDeliveries)
        {
            scheduleRepeating = repeatingDeliveries;
            PrepareDictionary();
            AddPackagesRModule();
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
            calender = new Dictionary<DaysOfWeek, List<ScheduledPackage>>();

            foreach (DaysOfWeek day in Enum.GetValues(typeof(DaysOfWeek)))
            {
                calender.Add(day, new List<ScheduledPackage>());
            }
        }

        public void AddPackage(Package package, string time, DayOfWeek day)
        {
            if (calender.ContainsKey((DaysOfWeek)day))
            {
                calender[(DaysOfWeek)day].Add(new ScheduledPackage(package, time, day));
                Console.WriteLine(package.Name + " Added");
                RaisePackageAddEvent(package);
            }
        }

        private void AddPackagesRModule()
        {
            foreach (ScheduledPackage package in scheduleRepeating.GetModule())
            {
                AddPackage(package.GetPackage, package.Time, package.Day);
            }
        }

        public Dictionary<DaysOfWeek, List<ScheduledPackage>> GetSchedule()
        {
            return calender;
        }

        public event EventHandler<PackageEventArgs>? PackageAddEvent;

        private void RaisePackageAddEvent(Package package)
        {
            PackageAddEvent?.Invoke(this, new PackageEventArgs(package));
        }
    }
}