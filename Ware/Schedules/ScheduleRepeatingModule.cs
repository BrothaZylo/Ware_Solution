﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Ware.Schedules.Schedule;
using Ware.Packages;

namespace Ware.Schedules
{
    /// <summary>
    /// Repeating packages module for schedule
    /// </summary>
    public class ScheduleRepeatingModule
    {
        private List<ScheduledPackage> repeatingPackages = new List<ScheduledPackage>();

        /// <summary>
        /// Repeating packages module for schedule
        /// </summary>
        public ScheduleRepeatingModule()
        {
            repeatingPackages = [];
        }

        /// <summary>
        /// Adds a Weekly package to the module
        /// </summary>
        /// <param name="package">Package object for weekly transfer</param>
        /// <param name="time">time or date for transfer</param>
        /// <param name="day">day of transfer</param>
        /// <param name="transferType">Type of transfer</param>
        public void AddPackageWeekly(Package package, string time, DayOfWeek day, TransferType transferType)
        {
            ScheduledPackage p = new ScheduledPackage(package, time, day);
            p.TransferTypes = transferType;
            repeatingPackages.Add(p);
        }

        /// <summary>
        /// Adds a daily package to the module
        /// </summary>
        /// <param name="package">Package object for dauly transfer</param>
        /// <param name="time">Time or date for transfer</param>
        /// <param name="transferType">Type of transfer</param>
        public void AddPackageDaily(Package package, string time, TransferType transferType)
        {
            foreach (DayOfWeek day in Enum.GetValues(typeof(DaysOfWeek)))
            {
                ScheduledPackage p = new ScheduledPackage(package, time, day);
                p.TransferTypes = transferType;
                repeatingPackages.Add(p);
            }
        }

        /// <summary>
        /// Deletes a packages from the module
        /// </summary>
        /// <param name="package">package object for deletion</param>
        public void DeletePackage(Package package)
        {
            foreach (DayOfWeek day in Enum.GetValues(typeof(DaysOfWeek)))
            {
                foreach (ScheduledPackage item in repeatingPackages)
                {
                    if (item.Packages == package)
                    {
                        repeatingPackages.Remove(item);
                        DeletePackage(package);
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// Gets the list of all the packages scheduled
        /// </summary>
        /// <returns>A list of scheduled packages</returns>
        public List<ScheduledPackage> GetModule()
        {
            return repeatingPackages;
        }
    }

}
