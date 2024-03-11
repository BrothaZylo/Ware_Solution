﻿using System;
using System.Collections.Generic;

namespace Ware
{
    /// <summary>
    /// The reception of the packages and the times it takes are handled here.
    /// </summary>
    public class ReceivingDepartment : IReceivingDepartment
    {
        private readonly List<Package> receivedPackages = [];
        private readonly List<Package> allPackages = [];

        public delegate void PackageAddedToRecevingDepartmentHandler(object o, PackageEventArgs args);
        public event PackageAddedToRecevingDepartmentHandler PackageAddedToReceivingDepartment;


        public delegate void AllPackagesSentToStorageHandler(object o, PackageEventArgs args);
        public event AllPackagesSentToStorageHandler AllPackagesSentToStorage;



        /// <summary>
        /// The package is received and added to the dictionary of received packages.
        /// </summary>
        /// <param name="package">The name of the package being received.</param>
        public void AddPackage(Package package)
        {
            if (receivedPackages.Contains(package))
            {
                throw new PackageInvalidException(" Attempted to add the same package two times: " + package.Name);
            }
            receivedPackages.Add(package);
            allPackages.Add(package);
            PackageAddedToReceivingDepartment?.Invoke(this, new PackageEventArgs(package));

        }

        /// <summary>
        /// Sends the first package in the dictionary to storage then removes it from the dictionary.
        /// </summary>
        public void SendFirstPackageToStorage(Storage storageConfiguration)
        {
            if (receivedPackages.Count == 0)
            {
                throw new PackageInvalidException(" No packages to send to the storage: "+ storageConfiguration.ToString);
            }

            if (receivedPackages.Count > 0)
            {
                Package firstPackage = receivedPackages[0];
                if (storageConfiguration.IsSameTypeOfGoods(firstPackage))
                {
                    storageConfiguration.PlacePackage(firstPackage);
                    receivedPackages.RemoveAt(0);
                    Console.WriteLine($"Package {firstPackage.PackageId} was sent to the warehouse and removed from the receiving dictionary.");
                }

                if (!storageConfiguration.IsSameTypeOfGoods(firstPackage))
                {
                    throw new PackageInvalidException(" First package doesn't match with storage type: " + firstPackage.Name);
                }
            }

        }


        /// <summary>
        /// Sends all packages to storage.
        /// </summary>
        public void SendAllPackagesToStorage(Storage storageConfiguration)
        {
            int pamount = receivedPackages.Count;
            if (receivedPackages.Count == 0)
            {
                throw new PackageInvalidException("No packages to send to the storage."+ storageConfiguration);
            }

            for (int i = receivedPackages.Count - 1; i >= 0; i--)
            {
                Package package = receivedPackages[i];
                if (storageConfiguration.IsSameTypeOfGoods(receivedPackages[i]))
                {
                    storageConfiguration.PlacePackage(receivedPackages[i]);
                    receivedPackages.RemoveAt(i);

                }
            }
            AllPackagesSentToStorage?.Invoke(this, new PackageEventArgs(storageConfiguration));

        }

        /// <summary>
        /// Prints the dictionary of all received packages.
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
        /// Returns a dictionary of all recivived packages
        /// </summary>
        /// <returns>Returns a dictionary of all recivived packages</returns>
        public List<Package> GetPackageList()
        {
            return receivedPackages;
        }
        /// <summary>
        /// Returns a dictionary of all AllPackages
        /// </summary>
        /// <returns>Returns a dictionary of all AllPackages</returns>
        public List<Package> GetAllPackages()
        {
            return allPackages;
        }

    }
}
