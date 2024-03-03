using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ware
{
    /// <summary>
    /// Statastics related to the storage house.
    /// </summary>
    public class Statistics : IStatistics
    {
        private int totalPackagesRecived, packagesReceivedToday;
        private static DateTime curTime = DateTime.Now;
        private static DateTime timeReset = new DateTime(curTime.Year, curTime.Month, curTime.Day, 0, 0, 0);
        private TimeSpan resetSpan = curTime - timeReset;
        private Dictionary<string, int> dictAllStatistics = [];
        private Dictionary<string, int> dictAllStatisticsTmp = [];



        private void UpdateDictionary()
        {
            if (dictAllStatistics.Count == 0)
            {
                dictAllStatistics.Add("Overall Packages Received: ", totalPackagesRecived);
                dictAllStatistics.Add("Packages Received Today: ", packagesReceivedToday);
            }
            dictAllStatistics["Overall Packages Received: "] = (totalPackagesRecived);
            dictAllStatistics["Packages Received Today: "] = (packagesReceivedToday);
        }

        private static void UpdateCurrentTimer()
        {
            curTime = DateTime.Now;
        }

        private static void UpdateResetTmer()
        {
            timeReset = new DateTime(curTime.Year, curTime.Year, curTime.Month, curTime.Day, 0, 0, 0);
        }

        /// <summary>
        /// Adds 1 count to the amount of packages handled in total.
        /// </summary>
        public void AddTotalPackagesHandled()
        {
        totalPackagesRecived++;
        }

        /// <summary>
        /// Adds 1 count to the amount of packages handled today.
        /// </summary>
        public void AddPackagesHandledToDay()
        {
            packagesReceivedToday++;
            DailySaveHelper();
        }

        private void DailySaveHelper()
        {
            if(resetSpan.TotalHours >= 24)
            {
                if(dictAllStatisticsTmp.Count == 0)
                {
                    dictAllStatisticsTmp.Add("Packages Received Today: ", packagesReceivedToday);
                    dictAllStatisticsTmp.Add("Overall Packages Received: ", totalPackagesRecived);
                }
                dictAllStatistics["Packages Received Today: "] = (packagesReceivedToday);
                dictAllStatistics["Overall Packages Received: "] = (totalPackagesRecived);
            }
        }

        /// <summary>
        /// Console prints the dictionary containing all the statistics.
        /// </summary>
        public void PrintDict()
        {
            UpdateDictionary();
            foreach (KeyValuePair<string, int> key in dictAllStatistics)
            {
                Console.WriteLine(key);
            }
        }

        /// <summary>
        /// Gets all the statistical numbers.
        /// </summary>
        /// <returns>A dictionary containing all the statistics</returns>
        public Dictionary<string, int> GetAllStatisticsDictionary()
        {
            UpdateDictionary();
            return dictAllStatistics;
        }

        /// <summary>
        /// Gets the total amount of packages handled.
        /// </summary>
        public int TotalPackages
        {
            get { return totalPackagesRecived; }
        }

        /// <summary>
        /// Gets the amount of packages handled today.
        /// </summary>
        public int PackagesReceivedToday
        {
            get { return packagesReceivedToday; }
        }


    }
}
