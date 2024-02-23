﻿using System;
using System.Collections.Generic;

namespace Ware
{
    /// <summary>
    /// The reception of the packages and the times it takes are handled here.
    /// </summary>
    public class ReceivingDepartment(Storage storage) : IReceivingDepartment
    {
        private List<Package> receivedPackages = [];
        private List<Package> allPackages = [];

        private Storage storageConfiguration = storage;

        /// <summary>
        /// The package is received and added to the list of received packages.
        /// </summary>
        /// <param name="package">The name of the package being received.</param>
        public void ReceivePackage(Package package)
        {
            receivedPackages.Add(package);
            allPackages.Add(package);

        }

        /// <summary>
        /// Sends the first package in the list to storage then removes it from the list.
        /// </summary>
        public void SendFirstPackageToStorage()
        {
            storageConfiguration.PlacePackage(receivedPackages[0]);
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
                        Console.WriteLine($"Package {firstPackage.PackageId} was not sent // please send to a warehouse with the same type of goods");

                    }
                }
            }
        }

        /// <summary>
        /// Sends all packages to storage.
        /// </summary>
        public void SendAllPackagesToStorage()
        {
            foreach (Package package in receivedPackages)
            {
                storageConfiguration.PlacePackage(package);
                storageConfiguration.GetAllStorageInformationPrint();
                
            }
            receivedPackages.Clear();
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
        public string TravelTimeToStorage() 
        {
            string time = "Estimated time to storage in minutes: ";
            return time+= storageConfiguration.TimeReceivingToStorageMinutes;


        }

        /// <summary>
        /// The packages are sent to the warehouse and return a list of the packages' results.
        /// </summary>
        /// <returns>List of strings that assign the result to the location of each package in the warehouse.</returns>
        
        /*
        public List<string> SendPackagesToWarehouse()
        {
            List<string> results = new List<string>();
            foreach (Package package in receivedPackages)
            {
                string result = storageConfiguration.PlacePackage(package);
                int timeDeliveryToStorage = storageConfiguration.GetTimeDeliveryToStorage();
                int timeStorageToTerminal = storageConfiguration.GetTimeStorageToTerminal();
                result += $" Time from receipt to storage: {timeDeliveryToStorage} minutes.";
                result += $" Time from warehouse to terminal: {timeStorageToTerminal} minutes.";
                results.Add(result);
            }

            receivedPackages.Clear();
            return results;
        }
        */
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
