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
        private int totalPackagesRecived = 0;
        private int packagesReceivedToday = 0;
        private Dictionary<string, int> dictAllStatistics = [];


        private void UpdateDictionary()
        {
            if (dictAllStatistics.Count == 0)
            {
                dictAllStatistics.Add("Total Packages Received: ", totalPackagesRecived);
                dictAllStatistics.Add("Packages Received Today: ", packagesReceivedToday);
            }
            dictAllStatistics["Total Packages Received: "] = (totalPackagesRecived);
            dictAllStatistics["Packages Received Today: "] = (packagesReceivedToday);
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
        /// <param name="package"></param>
        public void AddPackagesHandledToDay()
        {
            packagesReceivedToday++;
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
