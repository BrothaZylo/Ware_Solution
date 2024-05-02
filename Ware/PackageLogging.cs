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
    /// The class starts a package log. Saves as (TimeStamp, Action)
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
        /// <param name="action">This is the action preformed</param>
        /// <returns>returns that the package has been logged</returns>
        public string AddPackageLog(string packageId, string action)
        {
            if (packageId != null & packageId.Contains(' '))
            {
                throw new ArgumentException("The packageId should not be empty or contain any whitespaces");
            }
            else
            {
                if (!PackageLog.ContainsKey(packageId))
                {
                    PackageLog.Add(packageId, new List<(string, DateTime)>());

                    PackageLog[packageId].Add((action, DateTime.Now));
                    RaiseAddPackageLogEvent(packageId);
                    return packageId + " Was logged";
                }
                PackageLog[packageId].Add((action, DateTime.Now));
                RaiseAddPackageLogEvent(packageId);
                return packageId + " Was logged";         
            }

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
        public List<string> TrackPackage(string id)
        {
        List<string> packagesLog = [];
        bool packageExist = false;
        foreach (KeyValuePair<string, List<(string, DateTime)>> keys in PackageLog)
        {
            if (keys.Key == id)
            {
                packageExist = true;
                foreach ((string, DateTime) items in keys.Value)
                {
                    packagesLog.Add($"PackageID : {keys.Key} {items.Item1} {items.Item2}\n");
                }
            }
        }
        if (!packageExist)
        {
            throw new PackageInvalidException($"The package id: {id} could not be found in log");
        }    
        return packagesLog;
        }

        /// <summary>
        /// Get all of the packagelogs
        /// </summary>
        /// <returns>A dictionary with all package logs</returns>
        public Dictionary<string, List<(string, DateTime)>> GetAllPackageLog()
        {
            return PackageLog;
        }

        public event EventHandler<PackageEventArgs>? AddPackageLogEvent;

        private void RaiseAddPackageLogEvent(string packageId)
        {
            AddPackageLogEvent?.Invoke(this, new PackageEventArgs(packageId));
        }
    }
}