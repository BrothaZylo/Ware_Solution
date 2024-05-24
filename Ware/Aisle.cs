using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ware
{
    public class Aisle : IAisle
    {
        private string name;
        private List<Storage> storages;

        public Aisle(string aisleName) 
        {
            if (string.IsNullOrEmpty(aisleName))
            {
                throw new ArgumentException("Aisle name cannot be null or empty");
            }
            name = aisleName;
            storages = new List<Storage>();
        }
        
        /// <summary>
        /// Adds a storage to the aisle
        /// </summary>
        /// <param name="storage">A storage object</param>
        public void AddStorage(Storage storage)
        {
            storages.Add(storage);
            RaiseStorageAddEvent(storage);
        }

        /// <summary>
        /// Removes a storage from the aisle
        /// </summary>
        /// <param name="storage">A Storage object</param>
        public void removeStorage(Storage storage)
        {
            if (storages.Contains(storage))
            {
                storages.Remove(storage);
                RaiseStorageRemoveEvent(storage);
            }
        }

        /// <summary>
        /// Prints all the storages and all it is containing
        /// </summary>
        public void GetPackagesInAislesPrint()
        {
            Console.WriteLine($"========={name}=========");
            foreach (Storage item in storages)
            {
                item.GetAllStorageInformationPrint();
            }
        }

        /// <summary>
        /// Returns a string where we get the info of which storage the package is located at
        /// </summary>
        /// <param name="package">A package object</param>
        /// <returns>Returns a string that tells what storage the package is located at</returns>
        public string FindPackage(Package package)
        {
            foreach (Storage item in storages)
            {
                item.GetAllStorageInformationPrint();
                if (item.GetPackage(package.PackageId) == package)
                {
                    RaisePackageFoundEvent(item, package);
                    return item.GetPackageSectionById(package.PackageId);

                }
            }
            return null;
        }

        /// <summary>
        /// Getter and setter for aisle name
        /// </summary>
        public string AisleName 
        {
            get { return name; }
            set { name = value; }
        }

        public event EventHandler<StorageEventArgs>? StorageAddEvent;

        public event EventHandler<StorageEventArgs>? StorageRemoveEvent;

        public event EventHandler<StorageEventArgs>? PackageFoundEvent;
        private void RaiseStorageAddEvent(Storage storage)
        {
            StorageAddEvent?.Invoke(this, new StorageEventArgs(storage));
        }
        private void RaiseStorageRemoveEvent(Storage storage)
        {
            StorageAddEvent?.Invoke(this, new StorageEventArgs(storage));
        }
        private void RaisePackageFoundEvent(Storage storage, Package package)
        {
            PackageFoundEvent?.Invoke(this, new StorageEventArgs(storage, package));
        }
    }
}
