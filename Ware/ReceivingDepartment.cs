﻿using System;
using System.Collections.Generic;

namespace Ware
{
    /// <summary>
    /// The reception of the packages and the times it takes are handled here.
    /// </summary>
    public class ReceivingDepartment(StorageConfiguration warehouse)
    {
        private List<CreatePackage> receivedPackages = [];
        private StorageConfiguration storageConfiguration = warehouse;

        /// <summary>
        /// The package is received and added to the list of received packages.
        /// </summary>
        /// <param name="package">The name of the package being received.</param>
        public void ReceivePackage(CreatePackage package)
        {
            receivedPackages.Add(package);
        }

        public void SendFirstPackageToStorage()
        {
            storageConfiguration.PlacePackage(receivedPackages[0]);
            {
                if (receivedPackages.Count > 0)
                {
                    CreatePackage firstPackage = receivedPackages[0];
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

        public void SendAllPackagesToStorage()
        {
            foreach (CreatePackage package in receivedPackages)
            {
                storageConfiguration.PlacePackage(package);
                storageConfiguration.GetAllStorageInformationPrint();
            }
        }
        public void printlistpackage() 
        {
            foreach (CreatePackage package in receivedPackages) 
            { 
                Console.WriteLine(package);
            }

        }

        public string TravelTimeToStorage() 
        {
            string time = "Estimated time to Storage: ";
            return time+= storageConfiguration.GetTimeDeliveryToStorage();


        }

        /// <summary>
        /// The packages are sent to the warehouse and return a list of the packages' results.
        /// </summary>
        /// <returns>List of strings that assign the result to the location of each package in the warehouse.</returns>
        public List<string> SendPackagesToWarehouse()
        {
            List<string> results = new List<string>();
            foreach (CreatePackage package in receivedPackages)
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


    }
}
