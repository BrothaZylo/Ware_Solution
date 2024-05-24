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
        private readonly ScheduleRepeatingModule scheduleRepeating;

        /// <summary>
        /// Creates a schedule wich allows daily, weekly, time and TransferType of a package to be scheduled.
        /// </summary>
        /// <param name="transferPackages">Object of repeating transfer packages</param>
        public Schedule(ScheduleRepeatingModule? transferPackages)
        {
            PrepareDictionary();
            if (transferPackages != null)
            {
                scheduleRepeating = transferPackages;
                AddPackagesRModule();
            }
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

        /// <summary>
        /// Add a single package to the schedule
        /// </summary>
        /// <param name="package">Package object you want to add</param>
        /// <param name="time">Timestamp feks: "11:35 - 24.05.2024"</param>
        /// <param name="day">DayOfWeek enum</param>
        /// <param name="transferType">TransferType enum, Delivery or Receive</param>
        public void AddPackage(Package package, string time, DayOfWeek day, TransferType transferType)
        {
            if (calender.ContainsKey((DaysOfWeek)day))
            {
                ScheduledPackage p = new ScheduledPackage(package, time, day);
                p.TransferTypes = transferType;
                calender[(DaysOfWeek)day].Add(p);
                RaisePackageAddEvent(package);
            }
        }

        /// <summary>
        /// Returns a dictionary of packages for the day asked for
        /// </summary>
        /// <param name="day">Day of week</param>
        /// <returns>returns the packages for the day asked for</returns>
        public List<ScheduledPackage> GetPackageDay(DayOfWeek day)
        {
            DaysOfWeek deliveryDay = (DaysOfWeek)(int)day;

            return calender[deliveryDay];
        }

        private void AddPackagesRModule()
        {
            foreach (ScheduledPackage package in scheduleRepeating.GetModule())
            {
                AddPackage(package.Packages, package.Time, package.Day, package.TransferTypes);
            }
        }

        /// <summary>
        /// Simple print to see whats inside of the schedule
        /// </summary>
        public void PrintSchedule()
        {
            foreach (KeyValuePair<DaysOfWeek, List<ScheduledPackage>> y in calender)
            {
                Console.WriteLine(y.Key);
                foreach (ScheduledPackage p in y.Value)
                {
                    Console.WriteLine(" - " + p.Packages.Name + " " + p.TransferTypes + " " + p.Time);
                }
            }
        }

        /// <summary>
        /// Returns the dictionary of the schedule
        /// </summary>
        /// <returns>Returns the dictionary of the schedule</returns>
        public Dictionary<DaysOfWeek, List<ScheduledPackage>> GetSchedule()
        {
            return calender;
        }

        /// <summary>
        /// When a package is added into the schedule
        /// </summary>
        public event EventHandler<PackageEventArgs>? PackageAddEvent;

        private void RaisePackageAddEvent(Package package)
        {
            PackageAddEvent?.Invoke(this, new PackageEventArgs(package));
        }
    }
}