using System;
using System.Collections.Generic;

namespace Ware
{
    /// <summary>
    /// The reception of the packages and the times it takes are handled here.
    /// </summary>
    public class ReceivingDepartment(string name) : IReceivingDepartment
    {
        private string name = name;
        private readonly List<Package> receivedPackages = [];
        private readonly List<Package> allPackages = [];

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
            RaiseAddPackageEvent(package);
        }

        /// <summary>
        /// Sends a package to a storage.
        /// </summary>
        /// <param name="package">package you want to send</param>
        /// <param name="storage">storage you want to sent it to</param>
        /// <param name="shelfId">where you want to place it - shelf unique id</param>
        public void SendPackageToStorage(Package package, Storage storage, string shelfId)
        {
            foreach (Package item in receivedPackages)
            {
                if (item == package)
                {
                    storage.PlacePackage(package, shelfId);
                    receivedPackages.Remove(item);
                    RaiseSendPackageEvent(package, storage);
                    return;
                }
            }
        }

        /// <summary>
        /// Sends a package to a storage.
        /// </summary>
        /// <param name="package">package you want to send</param>
        /// <param name="storage">storage you want to sent it to</param>
        /// <param name="shelfId1">where you want to place it - shelf unique id</param>
        /// <param name="shelfId2">where you want to place it - shelf unique id</param>
        public void SendPackageToStorage(Package package, Storage storage, string shelfId1, string shelfId2)
        {
            foreach (Package item in receivedPackages)
            {
                if (item == package)
                {
                    storage.PlacePackage(package, shelfId1, shelfId2);
                    receivedPackages.Remove(item);
                    RaiseSendPackageEvent(package, storage);
                    return;
                }
            }
        }

        /// <summary>
        /// Sends a package to a storage.
        /// </summary>
        /// <param name="package">package you want to send</param>
        /// <param name="storage">storage you want to sent it to</param>
        /// <param name="shelfId1">where you want to place it - shelf unique id</param>
        /// <param name="shelfId2">where you want to place it - shelf unique id</param>
        /// <param name="shelfId3">where you want to place it - shelf unique id</param>
        public void SendPackageToStorage(Package package, Storage storage, string shelfId1, string shelfId2, string shelfId3)
        {
            foreach (Package item in receivedPackages)
            {
                if (item == package)
                {
                    storage.PlacePackage(package, shelfId1, shelfId2, shelfId3);
                    receivedPackages.Remove(item);
                    RaiseSendPackageEvent(package, storage);
                    return;
                }
            }
        }

        /// <summary>
        /// Sends the first package in the dictionary to storage then removes it from the dictionary.
        /// </summary>
        public void SendFirstPackageToStorage(Storage storage)
        {

            if (receivedPackages.Count > 0)
            {
                Package firstPackage = receivedPackages[0];
                if (storage.IsSameTypeOfGoods(firstPackage))
                {
                    storage.PlacePackageAutomatic(firstPackage);
                    RaiseSendFirstPackageEvent(firstPackage);
                    receivedPackages.RemoveAt(0);
                    //Console.WriteLine($"Package {firstPackage.Text} was sent to the warehouse and removed from the receiving dictionary.");
                }

                if (!storage.IsSameTypeOfGoods(firstPackage))
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
            for (int i = receivedPackages.Count - 1; i >= 0; i--)
            {
                Package package = receivedPackages[i];
                if (storageConfiguration.IsSameTypeOfGoods(receivedPackages[i]))
                {
                    RaiseSendAllPackageEvent(package, storageConfiguration);
                    storageConfiguration.PlacePackageAutomatic(receivedPackages[i]);
                    receivedPackages.RemoveAt(i);
                }
            }
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
        
        /// <summary>
        /// Name of ReceivingDepartment
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }


        /// <summary>
        /// Event for SendAllPackagesToStorage(Storage storage)
        /// </summary>
        public event EventHandler<PackageEventArgs> SendAllPackageEvent;

        /// <summary>
        /// Event for AddPackage(Package package)
        /// </summary>
        public event EventHandler<PackageEventArgs> PackageAddedEvent;

        /// <summary>
        /// Event for SendFirstPackageToStorage(Storage storage)
        /// </summary>
        public event EventHandler<PackageEventArgs> SendFirstPackageEvent;

        /// <summary>
        /// Event for SendFirstPackageToStorage(Package package, Storage storage)
        /// </summary>
        public event EventHandler<PackageEventArgs> SendPackageEvent;

        private void RaiseSendAllPackageEvent(Package package, Storage storage)
        {
            SendAllPackageEvent?.Invoke(this, new PackageEventArgs(package, storage));
        }

        private void RaiseAddPackageEvent(Package package)
        {
            PackageAddedEvent?.Invoke(this, new PackageEventArgs(package));
        }

        private void RaiseSendFirstPackageEvent(Package package)
        {
            SendFirstPackageEvent?.Invoke(this, new PackageEventArgs(package));
        }

        private void RaiseSendPackageEvent(Package package, Storage storage)
        {
            SendPackageEvent?.Invoke(this, new PackageEventArgs(package, storage));
        }
    }
}
