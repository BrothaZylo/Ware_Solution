using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ware
{
    public class Aisle
    {
        private string name;
        private List<Storage> storages;
        private int storageIdCounter = 1;

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

    }
}
