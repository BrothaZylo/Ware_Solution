using System;
using System.Collections.Generic;

namespace Ware
{
    /// <summary>
    /// The reception of the packages and the times it takes are handled here.
    /// </summary>
    public class ReceivingDepartment() : IReceivingDepartment
    {
        private readonly List<Package> receivedPackages = [];
        private readonly List<Package> allPackages = [];

        /// <summary>
        /// The package is received and added to the list of received packages.
        /// </summary>
        /// <param name="package">The name of the package being received.</param>
        public void AddPackage(Package package)
        {
            if (package == null)
            {
                throw new ArgumentNullException("Can't add null on package.");
            }


            receivedPackages.Add(package);
            allPackages.Add(package);

        }



        /// <summary>
        /// Sends the first package in the list to storage then removes it from the list.
        /// </summary>
        public void SendFirstPackageToStorage(Storage storageConfiguration)
        {
            if (receivedPackages.Count > 0)
            {
                Package firstPackage = receivedPackages[0];
                if (storageConfiguration.IsSameTypeOfGoods(firstPackage))
                {
                    storageConfiguration.PlacePackage(firstPackage);
                    receivedPackages.RemoveAt(0);
                    Console.WriteLine($"Package {firstPackage.PackageId} was sent to the warehouse and removed from the receiving list.");
                }
                if (!storageConfiguration.IsSameTypeOfGoods(firstPackage))
                {
                    Console.WriteLine($"Package {firstPackage.PackageId} was not sent // please sendRecToStorage to a warehouse with the same type of goods");
                }
            } 
        }

        /// <summary>
        /// Sends all packages to storage.
        /// </summary>
        public void SendAllPackagesToStorage(Storage storageConfiguration)
        {
            foreach (Package package in receivedPackages)
            {
                if (storageConfiguration.IsSameTypeOfGoods(package))
                {
                    storageConfiguration.PlacePackage(package);
                }
            }
            for(int i = 0; i < receivedPackages.Count; i++)
            {
                if (storageConfiguration.IsSameTypeOfGoods(receivedPackages[i]))
                {
                    receivedPackages.RemoveAt((int)i);
                }
            }
        }

        /// <summary>
        /// Prints the list of all received packages.
        /// </summary>
        public void GetAllPackagePrint() 
        {
            foreach (Package package in receivedPackages) 
            { 
                Console.WriteLine(package);
            }

        }

        /// <summary>
        /// Finds the time it takes from when recieved to it enters the storage.
        /// </summary>
        /// <returns>Represents the estimated time it took to get to storage.</returns>
        public string TravelTimeToStorage(Storage storageConfiguration) 
        {
            string time = "Estimated time to storage in seconds: ";
            return time+= storageConfiguration.TimeDeliveryToStorageSeconds;


        }

        /// <summary>
        /// Returns a list of all recivived packages
        /// </summary>
        /// <returns>Returns a list of all recivived packages</returns>
        public List<Package> GetPackageList()
        {
            return receivedPackages;
        }
        /// <summary>
        /// Returns a list of all AllPackages
        /// </summary>
        /// <returns>Returns a list of all AllPackages</returns>
        public List<Package> GetAllPackages()
        {
            return allPackages;
        }

    }
}
