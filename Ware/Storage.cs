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
        private readonly string goodsType = goodsType;
        private readonly List<ShelvesConfig> addShelves = [];
        private double timeFromReceivingDepartmentToStorage = 0, timeFromStoragetoTerminal = 0;
        // Goods,(id,sizename,width,height,isEmpty)
        private readonly Dictionary<string, (Package?, string, double, double, bool)> yourStorageDict = [];

        /// <summary>
        /// Gets the goodstype of the shelf
        /// </summary>
        public string GoodsType
        {
            get { return goodsType; }
        }

        /// <summary>
        /// Creates the Storageunit based on instructions from the config and constructor.
        /// </summary>
        public void Build()
        {
            double StorageCounter = 1.01;
            double unitCounter = 0;
            foreach (Storage.ShelvesConfig j in addShelves)
            {
                for (int k = 0; k < j.TotalUnitsAvailable; k++)
                {
                    yourStorageDict.Add(goodsType + "ShelfID: " + Math.Round(StorageCounter, 3), (null , j.SizeName, j.MaxWidthCm, j.MaxHeightCm, false));
                    StorageCounter+=0.01;
                }
                StorageCounter = 1.01;
                unitCounter += 1;
                StorageCounter += unitCounter;
            }
        }

        /// <summary>
        /// Places a package in a storage manually
        /// </summary>
        /// <param name="package">The package you want to put in the shelf</param>
        /// <param name="shelfId"> The id of the space you want to place it in</param>
        public void PlacePackage(Package package, string shelfId)
        {
            foreach(KeyValuePair<string, (Package?, string, double, double, bool)> item in yourStorageDict)
            {
                if (item.Key == shelfId)
                {
                    yourStorageDict[item.Key] = (package, item.Value.Item2, item.Value.Item3, item.Value.Item4, true);
                }
            }
        }

        /// <summary>
        /// Places a package in a storage manually
        /// </summary>
        /// <param name="package">The package you want to put in the shelf</param>
        /// <param name="shelfId1"> The id of the space you want to place it in</param>
        /// <param name="shelfId2"> The id of the space you want to place it in</param>
        public void PlacePackage(Package package, string shelfId1, string shelfId2)
        {
            foreach (KeyValuePair<string, (Package?, string, double, double, bool)> item in yourStorageDict)
            {
                if (item.Key == shelfId1 || item.Key == shelfId2)
                {
                    yourStorageDict[item.Key] = (package, item.Value.Item2, item.Value.Item3, item.Value.Item4, true);
                }
            }
        }

        /// <summary>
        /// Places a package in a storage manually
        /// </summary>
        /// <param name="package">The package you want to put in the shelf</param>
        /// <param name="shelfId1"> The id of the space you want to place it in</param>
        /// <param name="shelfId2"> The id of the space you want to place it in</param>
        /// <param name="shelfId3"> The id of the space you want to place it in</param>
        public void PlacePackage(Package package, string shelfId1, string shelfId2, string shelfId3)
        {
            foreach (KeyValuePair<string, (Package?, string, double, double, bool)> item in yourStorageDict)
            {
                if (item.Key == shelfId1 || item.Key == shelfId2 || item.Key == shelfId3)
                {
                    yourStorageDict[item.Key] = (package, item.Value.Item2, item.Value.Item3, item.Value.Item4, true);
                }
            }
        }

        /// <summary>
        /// Removes a packages based on shelf id
        /// </summary>
        /// <param name="shelfId">The id of the space you want to remove a package from</param>
        public void RemovePackage(string shelfId)
        {
            foreach (KeyValuePair<string, (Package?, string, double, double, bool)> item in yourStorageDict)
            {
                if (item.Key == shelfId)
                {
                    yourStorageDict[item.Key] = (null, item.Value.Item2, item.Value.Item3, item.Value.Item4, true);
                }
            }
        }

        /// <summary>
        /// Removes a packages based on shelf id
        /// </summary>
        /// <param name="shelfId1">The id of the space you want to remove a package from</param>
        /// <param name="shelfId2">The id of the space you want to remove a package from</param>
        public void RemovePackage(string shelfId1, string shelfId2)
        {
            foreach (KeyValuePair<string, (Package?, string, double, double, bool)> item in yourStorageDict)
            {
                if (item.Key == shelfId1 || item.Key == shelfId2)
                {
                    yourStorageDict[item.Key] = (null, item.Value.Item2, item.Value.Item3, item.Value.Item4, true);
                }
            }
        }

        /// <summary>
        /// Removes a packages based on shelf id
        /// </summary>
        /// <param name="shelfId1">The id of the space you want to remove a package from</param>
        /// <param name="shelfId2">The id of the space you want to remove a package from</param>
        /// <param name="shelfId3">The id of the space you want to remove a package from</param>
        public void RemovePackage(string shelfId1, string shelfId2, string shelfId3)
        {
            foreach (KeyValuePair<string, (Package?, string, double, double, bool)> item in yourStorageDict)
            {
                if (item.Key == shelfId1 || item.Key == shelfId2 || item.Key == shelfId3)
                {
                    yourStorageDict[item.Key] = (null, item.Value.Item2, item.Value.Item3, item.Value.Item4, true);
                }
            }
        }

        /// <summary>
        /// Places the package in the storagehouse if the size and goodstype fits the slot.
        /// </summary>
        /// <param name="package">A Package</param>
        /// <returns>An option to know if its in storage or got placed in the storage</returns>
        public void PlacePackageAutomatic(Package package)
        {
            double packagesizew = package.Width;
            double packagesizeh = package.Height;
            foreach (KeyValuePair<string, (Package?, string, double, double, bool)> i in yourStorageDict)
            {
                if (goodsType == package.Goods)
                {
                    if (i.Value.Item1 == package)
                    {
                        throw new PackageInvalidException("No matching Id found: " + package);
                    }
                    if (i.Value.Item5 == false)
                    {
                        if (packagesizew < i.Value.Item3 && packagesizeh < i.Value.Item4)
                        {
                            yourStorageDict[i.Key] = (package, i.Value.Item2, i.Value.Item3, i.Value.Item4, true);
                            // blir plassert i i.key // add event , add excep
                            break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// inputting the packageId will remove the package from the shelf its placed.
        /// </summary>
        /// <param name="packageId">A Package</param>
        /// <returns>it will return the packageId if it finds the packackage, else it will return null</returns>
        public Package? MovePackageById(string packageId)
        {
            Package? tmp = null;
            foreach (KeyValuePair<string, (Package?, string, double, double, bool)> i in yourStorageDict)
            {
                if (i.Value.Item1 is not null && i.Value.Item1.PackageId == packageId)
                {
                    yourStorageDict[i.Key] = (null, i.Value.Item2, i.Value.Item3, i.Value.Item4, false);
                    //add event
                    tmp = i.Value.Item1;
                }
            }
            if(tmp != null)
            {
                return tmp;
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
            int foundPackageCounter = 0;
            foreach(KeyValuePair<string, (Package?, string, double, double, bool)> i in yourStorageDict)
            {
                if (i.Value.Item1 == package)
                {
                    yourStorageDict[i.Key] = (null, i.Value.Item2, i.Value.Item3, i.Value.Item4, false);
                    foundPackageCounter++;
                }
            }
            if (foundPackageCounter != 0)
            {
                return package;
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
            Package? tmp = null;
            foreach (KeyValuePair<string, (Package?, string, double, double, bool)> i in yourStorageDict)
            {
                if (i.Value.Item1 == package)
                {
                    yourStorageDict[i.Key] = (null, i.Value.Item2, i.Value.Item3, i.Value.Item4, false);
                    tmp = package;
                }
            }
            if (tmp != null)
            {
                terminal.AddPackage(tmp);
            }
            // add excep, event
        }


        /// <summary>
        /// Prints the entire storage house shelf unit.
        /// </summary>
        public void GetAllStorageInformationPrint()
        {
            foreach(KeyValuePair<string, (Package?, string, double, double, bool)> i in yourStorageDict)
            {
                if(i.Value.Item1 == null)
                {
                    Console.WriteLine("["+i.Key + " | (Package ID: " + "Empty ) | " + "(Size: "+i.Value.Item2 + ", Width: " + i.Value.Item3 + "cm, Height: " + i.Value.Item4+"cm)]");
                }
                else
                {
                    Console.WriteLine("["+i.Key + " | (Package ID: " + i.Value.Item1.PackageId + ") | " + "(Size: " + i.Value.Item2 + ", Width: " + i.Value.Item3 + "cm, Height: " + i.Value.Item4+"cm)]");
                }
            }
        }

        /// <summary>
        /// Returns the warehouse dictionary 
        /// </summary>
        /// <returns>warehouse dictionary </returns>
        public Dictionary<string, (Package?, string, double, double, bool)> GetAllStorageInformationAsDictionary()
        {
            return yourStorageDict;
        }

        /// <summary>
        /// Entering the storage unit's id will find the shelf content.
        /// </summary>
        /// <param name="shelfNumber">ShelfID number</param>
        /// <returns>Returns the shelf number else, it will return Does not exist</returns>
        public string? GetStorageNameById(int shelfNumber)
        {
            foreach(KeyValuePair<string, (Package?, string, double, double, bool)> i in yourStorageDict)
            {
                string[] keysplit = i.Key.Split(':');
                string key1 = keysplit[0];
                string yournumber = key1 + ": " + shelfNumber;
                if (i.Key == yournumber)
                {
                    return yournumber;
                }
            }
            //add excep
            return null;
        }

        /// <summary>
        /// It will find the shelf where the packageId is located.
        /// </summary>
        /// <param name="packageId">A package</param>
        /// <returns>Returns the section of set package id is located, else returns nothing</returns>
        public string FindPackageSectionById(string packageId)
        {
            string item = "";
            foreach(KeyValuePair<string, (Package?, string, double, double, bool)> i in yourStorageDict)
            {
                if (i.Value.Item1 is not null && i.Value.Item1.PackageId == packageId)
                {
                    item += i;
                    return item;
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
            foreach (KeyValuePair<string, (Package?, string, double, double, bool)> item in yourStorageDict)
            {
                if (item.Value.Item1 is not null && item.Value.Item1.PackageId == packageId)
                {
                    return item.Key;
                }
            }
            throw new PackageInvalidException(" Package with ID not found: " + packageId);
        }

        /// <summary>
        /// It checks if the spot is taken by another package
        /// </summary>
        /// <param name="shelfId">id of the space you want to check</param>
        /// <returns>returns true if taken, else false</returns>
        public bool IsSpotTaken(string shelfId)
        {
            foreach(KeyValuePair<string, (Package?, string, double, double, bool)> i in yourStorageDict)
            {
                if (i.Key == shelfId)
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
            foreach (KeyValuePair<string, (Package?, string, double, double, bool)> i in yourStorageDict)
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
        public void AddShelf(string sizeName, int totalUntsAvailable, double maxHeightCm, double maxWidthCm)
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