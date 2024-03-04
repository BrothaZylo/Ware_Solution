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
    /// <param name="goodsType">This will be the name of the storage unit, and what type of goods can be placed in this unit</param>
    public class Storage(string goodsType = "Undefined") : IStorage
    {
        private readonly string shelfCategory = goodsType;
        private readonly List<ShelvesConfig> addShelves = [];
        private double timeFromReceivingDepartmentToStorage = 0, timeFromStoragetoTerminal = 0;
        // Goods,(id,sizename,width,height,isEmpty)
        private readonly Dictionary<string, (string, string, double, double, bool)> yourStorageDict = [];

        /// <summary>
        /// Gets the goodstype of the shelf
        /// </summary>
        public string ShelfCategory
        {
            get { return shelfCategory; }
        }

        /// <summary>
        /// Creates the Storageunit based on instructions from the config and constructor.
        /// </summary>
        public void Build()
        {
            int StorageCounter = 1;
            foreach(Storage.ShelvesConfig j in addShelves)
            {
                for (int k = 0; k < j.TotalUnitsAvailable; k++)
                {
                    yourStorageDict.Add(shelfCategory + "ShelfID: " + StorageCounter, ("PackageID: Empty", "Type of storage: " + j.SizeName, j.MaxWidthCm, j.MaxHeightCm, false));
                    StorageCounter++;
                }
            }
        }

        /// <summary>
        /// Places the package in the storagehouse if the size and goodstype fits the slot.
        /// </summary>
        /// <param name="package">A Package</param>
        /// <returns>An option to know if its in storage or got placed in the storage</returns>
        public string? PlacePackage(Package package)
        {
            double packagesizew = package.Width;
            double packagesizeh = package.Height;
            foreach (KeyValuePair<string, (string, string, double, double, bool)> i in yourStorageDict)
            {
                if (shelfCategory == package.Goods)
                {
                    if (i.Value.Item1 == package.PackageId)
                    {
                        throw new PackageInvalidException("No matching Id found: " + package.PackageId);
                        break;
                    }
                    if (i.Value.Item5 == false)
                    {
                        if (packagesizew < i.Value.Item3 && packagesizeh < i.Value.Item4)
                        {
                            yourStorageDict[i.Key] = (package.PackageId, i.Value.Item2, i.Value.Item3, i.Value.Item4, true);
                            return "Package was placed in: " + i.Key;
                        }
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// inputting the packageId will remove the package from the shelf its placed.
        /// </summary>
        /// <param name="packageId">A Package</param>
        /// <returns>it will return the packageId if it finds the packackage, else it will return null</returns>
        public string? MovePackageById(string packageId)
        {
            foreach (KeyValuePair<string, (string, string, double, double, bool)> i in yourStorageDict)
            {
                if (i.Value.Item1 == packageId)
                {
                    yourStorageDict[i.Key] = ("PackageID: Empty", i.Value.Item2, i.Value.Item3, i.Value.Item4, false);
                    return packageId;
                }
            }
            throw new PackageInvalidException(" Package with ID not found: "+packageId);
        }
        
        /// <summary>
        /// Moves the package from the shelf and returns it in package format.
        /// </summary>
        /// <param name="package">A package</param>
        /// <returns>if it finds the package it will return the package, else it will return a null</returns>
        public Package? MovePackage(Package package)
        {
            foreach(KeyValuePair<string, (string, string, double, double, bool)> i in yourStorageDict)
            {
                if (i.Value.Item1 == package.PackageId)
                {
                    yourStorageDict[i.Key] = ("PackageID: Empty", i.Value.Item2, i.Value.Item3, i.Value.Item4, false);
                    return package;
                }
            }
            throw new PackageInvalidException(" Package with ID not found: " + package.Name);
        }
        /// <summary>
        /// Moves the package from the shelf and returns it in package format.
        /// </summary>
        /// <param name="package">A package</param>
        /// <param name="terminal">The Terminal where the package will be sent out</param>
        /// <returns>if it finds the package it will return the package, else it will return null</returns>
        public void MovePackageToTerminal(Package package, Terminal terminal)
        {
            foreach (KeyValuePair<string, (string, string, double, double, bool)> i in yourStorageDict)
            {
                if (i.Value.Item1 == package.PackageId)
                {
                    yourStorageDict[i.Key] = ("PackageID: Empty", i.Value.Item2, i.Value.Item3, i.Value.Item4, false);
                    terminal.AddPackage(package);
                }
            }
        }


        /// <summary>
        /// Prints the entire storage house shelf unit.
        /// </summary>
        public void GetAllStorageInformationPrint()
        {
            foreach(KeyValuePair<string, (string, string, double, double, bool)> i in yourStorageDict)
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
            return yourStorageDict;
        }

        /// <summary>
        /// Entering the storage unit's id will find the shelf content.
        /// </summary>
        /// <param name="shelfNumber">ShelfID number</param>
        /// <returns>Returns the shelf number else, it will return Does not exist</returns>
        public string GetStorageNameById(int shelfNumber)
        {
            foreach(KeyValuePair<string, (string, string, double, double, bool)> i in yourStorageDict)
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
            foreach(KeyValuePair<string, (string, string, double, double, bool)> i in yourStorageDict)
            {
                if (i.Value.Item1 == packageId)
                {
                    item += i;
                }
            }
            throw new PackageInvalidException(" Package with ID not found: " + packageId);
        }

        /// <summary>
        /// Finds the package location by using the id
        /// </summary>
        /// <param name="packageId">A package</param>
        /// <returns>The shelf its placed at, else will return Does not exist</returns>
        public string FindPackageById(string packageId)
        {
            string item = "Does not exist";
            foreach (KeyValuePair<string, (string, string, double, double, bool)> i in yourStorageDict)
            {
                if (i.Value.Item1 == packageId)
                {
                    return i.Key;
                }
            }
            throw new PackageInvalidException(" Package with ID not found: " + packageId);
        }

        /// <summary>
        /// It checks if the spot is taken by another package
        /// </summary>
        /// <param name="storageName">Name of storage</param>
        /// <returns>returns true if taken, else false</returns>
        public bool IsSpotTaken(string storageName)
        {
            foreach(KeyValuePair<string, (string, string, double, double, bool)> i in yourStorageDict)
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
        /// <param name="package">class package object</param>
        /// <returns>returns true if package type is the same as the one in the storage unit, else false</returns>
        public bool IsSameTypeOfGoods(Package package)
        {
            foreach (KeyValuePair<string, (string, string, double, double, bool)> i in yourStorageDict)
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

        /// <summary>
        /// Configures diffrent sizes that a complete Warehouse storageunit contains.
        /// </summary>
        private class ShelvesConfig
        {
            /// <summary>
            /// Name of the size for x amount of units
            /// </summary>
            public required string SizeName { get; set; }
            /// <summary>
            /// Set the amount of units for set size in the unit
            /// </summary>
            public int TotalUnitsAvailable { get; set; }
            /// <summary>
            /// Max width for x amount of units
            /// </summary>
            public double MaxWidthCm { get; set; }
            /// <summary>
            /// Max Height for x amount of units
            /// </summary>
            public double MaxHeightCm { get; set; }
        }
        /// <summary>
        /// Custom unit can be added to the storage
        /// </summary>
        /// <param name="sizeName">Size of the shelf</param>
        /// <param name="totalUntsAvailable">Total units/shelves </param>
        /// <param name="maxHeightCm">Height of the unit/Shelf</param>
        /// <param name="maxWidthCm">Width if the unit/shelf</param>
        public void AddUnit(string sizeName, int totalUntsAvailable, double maxHeightCm, double maxWidthCm)
        {
            addShelves.Add(new() { SizeName = sizeName, TotalUnitsAvailable = totalUntsAvailable, MaxHeightCm = maxHeightCm, MaxWidthCm = maxWidthCm });
        }

        /// <summary>
        /// Prints the diffrent Size configs for each size created.
        /// </summary>
        public void UnitSpecsPrint()
        {
            foreach (Storage.ShelvesConfig Item in addShelves)
            {
                Console.WriteLine("StorageName: " + Item.SizeName + " TotalUnits: " + Item.TotalUnitsAvailable + " Max Length CM: " + Item.MaxHeightCm + " Max width CM: " + Item.MaxWidthCm);
            }
        }
    }
}