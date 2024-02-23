using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Ware
{
    /// <summary>
    /// Preconfig of the storageunits which will later be used to create shelves.
    /// </summary>
    /// <param name="goodsType">This will be the name of the storage unit</param>
    /// <param name="configureSize">Shelf size list config</param>
    public class Storage(string goodsType, List<Storage.WareHouseSizeConfig> configureSize) : IStorage
    {
        private readonly string shelfcategory = goodsType;
        private readonly List<WareHouseSizeConfig> configfiles = configureSize;
        private double timeFromReceivingDepartmentToStorage, timeFromStoragetoTerminal;
        private readonly Dictionary<string, (string, string, double, double, bool)> yourWareList = [];


        /// <summary>
        /// Configures diffrent sizes that a complete Warehouse storageunit contains.
        /// </summary>
        public class WareHouseSizeConfig
        {
            public required string sizeName { get; set; }
            public int totalUnitsAvailable { get; set; }
            public double maxWidthCm { get; set; }
            public double maxHeightCm { get; set; }
        }

        /// <summary>
        /// Gets the goodstype of the shelf
        /// </summary>
        public string ShelfCategory
        {
            get { return shelfcategory; }
        }

        /// <summary>
        /// Prints the diffrent Size configs for each size created.
        /// </summary>
        public void SizeConfigPrint()
        {
            foreach (Storage.WareHouseSizeConfig Item in configfiles)
            {
                Console.WriteLine("StorageName: " + Item.sizeName + " TotalUnits: " + Item.totalUnitsAvailable + " Max Length CM: " + Item.maxHeightCm + " Max width CM: " + Item.maxWidthCm);
            }
        }

        /// <summary>
        /// Creates the Storageunit based on instructions from the config and constructor.
        /// </summary>
        public void Build()
        {
            int StorageCounter = 1;
            foreach(Storage.WareHouseSizeConfig j in configfiles)
            {
                for (int k = 0; k < j.totalUnitsAvailable; k++)
                {
                    yourWareList.Add(shelfcategory + "ShelfID: " + StorageCounter, ("PackageID: Empty", "Type of storage: " + j.sizeName, j.maxWidthCm, j.maxHeightCm, false));
                    StorageCounter++;
                }
            }
        }

        /// <summary>
        /// Places the package in the storagehouse if the size and goodstype fits the slot.
        /// </summary>
        /// <param name="package">A Package</param>
        /// <returns>An option to know if its in storage or got placed in the storage</returns>
        public string PlacePackage(Package package)
        {
            double packagesizew = package.Width;
            double packagesizeh = package.Height;
            foreach (KeyValuePair<string, (string, string, double, double, bool)> i in yourWareList)
            {
                if (shelfcategory == package.Goods)
                {
                    if (i.Value.Item1 == package.PackageId)
                    {
                        return "Package is already in storagehouse";
                    }
                    if (i.Value.Item5 == false)
                    {
                        if (packagesizew < i.Value.Item3 && packagesizeh < i.Value.Item4)
                        {
                            yourWareList[i.Key] = (package.PackageId, i.Value.Item2, i.Value.Item3, i.Value.Item4, true);
                            return "Package was placed in: " + i.Key;
                        }
                    }
                }
            }
            return "No suitable place found";
        }

        /// <summary>
        /// inputting the packageId will remove the package from the shelf its placed.
        /// </summary>
        /// <param name="packageId">A Package</param>
        /// <returns>it will return the packageId if it finds the packackage, else it will return null</returns>
        public string MovePackageById(string packageId)
        {
            foreach (KeyValuePair<string, (string, string, double, double, bool)> i in yourWareList)
            {
                if (i.Value.Item1 == packageId)
                {
                    yourWareList[i.Key] = ("PackageID: Empty", i.Value.Item2, i.Value.Item3, i.Value.Item4, false);
                    return packageId;
                }
            }
            return "null";
        }
        
        /// <summary>
        /// Moves the package from the shelf and returns the it in package format.
        /// </summary>
        /// <param name="package">A package</param>
        /// <returns>if it find the package it will return the package, else it will return a nulled package format</returns>
        public Package MovePackage(Package package)
        {
            foreach(KeyValuePair<string, (string, string, double, double, bool)> i in yourWareList)
            {
                if (i.Value.Item1 == package.PackageId)
                {
                    yourWareList[i.Key] = ("PackageID: Empty", i.Value.Item2, i.Value.Item3, i.Value.Item4, false);
                    return package;
                }
            }
            Package dummy = new("null", "null", 0, 0);

            return dummy;
        }

        /// <summary>
        /// Prints the entire storage house shelf unit.
        /// </summary>
        public void GetAllStorageInformationPrint()
        {
            foreach(KeyValuePair<string, (string, string, double, double, bool)> i in yourWareList)
            {
                Console.WriteLine(i);
            }
        }
        /// <summary>
        /// Returns the warehouse dictionary 
        /// </summary>
        /// <returns>warehouse dictionary </returns>
        public Dictionary<string, (string, string, double, double, bool)> GetAllStorageInformationAsDictionary()
        {
            return yourWareList;
        }

        /// <summary>
        /// Entering the storage unit's number it will search for the units nr.
        /// </summary>
        /// <param name="shelfNumber">ShelfID number</param>
        /// <returns>Returns the shelf number else, it will return Does not exist</returns>
        public string GetStorageNameById(int shelfNumber)
        {
            foreach(KeyValuePair<string, (string, string, double, double, bool)> i in yourWareList)
            {
                string[] keysplit = i.Key.Split(':');
                string key1 = keysplit[0];
                string yournumber = key1 + ": " + shelfNumber;
                if (i.Key == yournumber)
                {
                    return yournumber;
                }
            }
            return "Does not exist";
        }

        /// <summary>
        /// It will find the shelf where the packageId is located.
        /// </summary>
        /// <param name="packageId">A package</param>
        /// <returns>Returns the section of set package id is located, else returns nothing</returns>
        public string FindPackageSectionById(string packageId)
        {
            string item = "";
            foreach(KeyValuePair<string, (string, string, double, double, bool)> i in yourWareList)
            {
                if (i.Value.Item1 == packageId)
                {
                    item += i;
                }
            }
            return item;
        }

        /// <summary>
        /// Finds the package location by using the id
        /// </summary>
        /// <param name="packageId">A package</param>
        /// <returns>The shelf its placed at, else will return Does not exist</returns>
        public string FindPackageById(string packageId)
        {
            string item = "Does not exist";
            foreach (KeyValuePair<string, (string, string, double, double, bool)> i in yourWareList)
            {
                if (i.Value.Item1 == packageId)
                {
                    return i.Key;
                }
            }
            return item;
        }

        /// <summary>
        /// It checks if the spot is taken by another package
        /// </summary>
        /// <param name="storageName">Name of storage</param>
        /// <returns>returns true if taken, else false</returns>
        public bool IsSpotTaken(string storageName)
        {
            foreach(KeyValuePair<string, (string, string, double, double, bool)> i in yourWareList)
            {
                if (i.Key == storageName)
                {
                    return i.Value.Item5;
                }
            }
            return false;
        }

        /// <summary>
        /// Checks if package type is the same as the one in the storage unit
        /// </summary>
        /// <param name="package"></param>
        /// <returns>returns true if package type is the same as the one in the storage unit, else false</returns>
        public bool IsSameTypeOfGoods(Package package)
        {
            foreach (KeyValuePair<string, (string, string, double, double, bool)> i in yourWareList)
            {
                string[] keysplit = i.Key.Split('S');
                string key1 = keysplit[0];
                if (key1 == package.Goods)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Gets the time it takes from ReceivingDeportment to Storage
        /// </summary>
        public double TimeReceivingToStorageMinutes
        {
            get { return timeFromReceivingDepartmentToStorage / 60; }
            set { timeFromReceivingDepartmentToStorage = value; }
        }

        /// <summary>
        /// Finds the time it takes from storage to terminal based on config.
        /// </summary>
        /// <returns>x amount of time, else 0</returns>
        public double TimeStorageToTerminalMinutes
        {
            get { return timeFromStoragetoTerminal / 60; }
            set { timeFromStoragetoTerminal = value; }
        }

        /// <summary>
        /// It will find the time from Delivery To storageunit based from the config and converts it into seconds
        /// </summary>
        /// <returns>Get Time Delivery To Storage Seconds</returns>
        public double TimeDeliveryToStorageSeconds
        {
            get { return timeFromReceivingDepartmentToStorage; }
            set { timeFromReceivingDepartmentToStorage = value; }
        }

        /// <summary>
        /// Finds the time it takes from storage to terminal based on config and converts it into seconds.
        /// </summary>
        /// <returns>Time Storage To Terminal Seconds</returns>
        public double TimeStorageToTerminalSeconds
        {
            get { return timeFromStoragetoTerminal; }
            set { timeFromStoragetoTerminal = value; }
        }

    }
}