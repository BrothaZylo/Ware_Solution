using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Ware
{
    /// <summary>
    /// Preconfig of the storageunits which will later be used to create shelves.
    /// </summary>
    /// <param name="goodsType">This will be the name of the storage unit, and what type of goods can be placed in this unit</param>
    /// <param name="uniqueStorageId"></param>
    public class Storage(string goodsType, string uniqueStorageId) : IStorage
    {
        private readonly List<ShelvesAdd> addShelves = [];
        // Goods,(package,sizename,width,height,isEmpty bool)
        private readonly Dictionary<string, (Package?, string, double, double, bool)> yourStorageDict = [];
        private string uniqueId = uniqueStorageId;
        private string goodsType = goodsType;
        private bool northAccess = true;
        private bool eastAccess = true;
        private bool southAccess = true;
        private bool westAccess = true;


        /// <summary>
        /// Used for PlacePackage(args) and PlacePackageAutomatic(Package package)
        /// </summary>
        public event EventHandler<PackageEventArgs>? PackagePlacedEvent;

        /// <summary>
        /// Used for RemovePackage(args)
        /// </summary>
        public event EventHandler<PackageEventArgs>? RemovePackageEvent;

        /// <summary>
        /// Used for MovePackage(Package package)
        /// </summary>
        public event EventHandler<PackageEventArgs>? MovePackageEvent;

        /// <summary>
        /// Used for MovePackageToTerminal(Package package, Terminal terminal)
        /// </summary>
        public event EventHandler<PackageEventArgs>? MovePackageToTerminalEvent;

        private void RaisePackagePlacedEvent(Package package, string storageUniqueId)
        {
            PackagePlacedEvent?.Invoke(this, new PackageEventArgs(package, storageUniqueId));
        }

        private void RaiseRemovePackageEvent(Package package, string storageUniqueId)
        {
            RemovePackageEvent?.Invoke(this, new PackageEventArgs(package, storageUniqueId));
        }

        private void RaiseMovePackageEvent(Package package, string storageUniqueId)
        {
            MovePackageEvent?.Invoke(this, new PackageEventArgs(package, storageUniqueId));
        }
        private void RaiseMovePackageToTerminalEvent(Package package, Terminal terminal)
        {
            MovePackageToTerminalEvent?.Invoke(this, new PackageEventArgs(package, terminal));
        }
        /// <summary>
        /// Creates the Storageunit based on instructions from the config and constructor.
        /// </summary>
        public void Build()
        {
            double StorageCounter = 101;
            double unitCounter = 0;
            foreach (Storage.ShelvesAdd j in addShelves)
            {
                for (int k = 0; k < j.TotalUnitsAvailable; k++)
                {
                    yourStorageDict.Add(goodsType + "ShelfID: " +uniqueId+ Math.Round(StorageCounter, 3), (null , j.SizeName, j.MaxWidthCm, j.MaxHeightCm, false));
                    StorageCounter+=1;
                }
                StorageCounter = 101;
                unitCounter += 100;
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
                if (item.Key == GetStorageNameById(shelfId))
                {
                    RaisePackagePlacedEvent(package, UniqueId);
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
                if (item.Key == GetStorageNameById(shelfId1) || item.Key == GetStorageNameById(shelfId2))
                {
                    RaisePackagePlacedEvent(package, UniqueId);
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
                if (item.Key == GetStorageNameById(shelfId1) || item.Key == GetStorageNameById(shelfId2) || item.Key == GetStorageNameById(shelfId3))
                {
                    RaisePackagePlacedEvent(package, UniqueId);
                    yourStorageDict[item.Key] = (package, item.Value.Item2, item.Value.Item3, item.Value.Item4, true);
                }
            }
        }

        /// <summary>
        /// Automatically removes all packages related to input.
        /// </summary>
        /// <param name="package">Package you want removed</param>
        public void RemovePackage(Package package)
        {
            foreach (KeyValuePair<string, (Package?, string, double, double, bool)> i in yourStorageDict)
            {
                if (i.Value.Item1 == package)
                {
                    yourStorageDict[i.Key] = (null, i.Value.Item2, i.Value.Item3, i.Value.Item4, false);
                    RaiseRemovePackageEvent(package, UniqueId);
                }
            }
        }

        /// <summary>
        /// Removes packages based on shelf id
        /// </summary>
        /// <param name="shelfId">The id of the space you want to remove a package from</param>
        public void RemovePackage(string shelfId)
        {
            foreach (KeyValuePair<string, (Package?, string, double, double, bool)> item in yourStorageDict)
            {
                if (item.Key == GetStorageNameById(shelfId))
                {
                    if (item.Value.Item1 != null)
                    {
                        RaiseRemovePackageEvent(item.Value.Item1, UniqueId);
                    }
                    yourStorageDict[item.Key] = (null, item.Value.Item2, item.Value.Item3, item.Value.Item4, true);
                }
            }
        }

        /// <summary>
        /// Removes packages based on shelf id
        /// </summary>
        /// <param name="shelfId1">The id of the space you want to remove a package from</param>
        /// <param name="shelfId2">The id of the space you want to remove a package from</param>
        public void RemovePackage(string shelfId1, string shelfId2)
        {
            foreach (KeyValuePair<string, (Package?, string, double, double, bool)> item in yourStorageDict)
            {
                if (item.Key == GetStorageNameById(shelfId1) || item.Key == GetStorageNameById(shelfId2))
                {
                    if (item.Value.Item1 != null)
                    {
                        RaiseRemovePackageEvent(item.Value.Item1, UniqueId);
                    }
                    yourStorageDict[item.Key] = (null, item.Value.Item2, item.Value.Item3, item.Value.Item4, true);
                }
            }
        }

        /// <summary>
        /// Removes packages based on shelf id
        /// </summary>
        /// <param name="shelfId1">The id of the space you want to remove a package from</param>
        /// <param name="shelfId2">The id of the space you want to remove a package from</param>
        /// <param name="shelfId3">The id of the space you want to remove a package from</param>
        public void RemovePackage(string shelfId1, string shelfId2, string shelfId3)
        {
            foreach (KeyValuePair<string, (Package?, string, double, double, bool)> item in yourStorageDict)
            {
                if (item.Key == GetStorageNameById(shelfId1) || item.Key == GetStorageNameById(shelfId2) || item.Key == GetStorageNameById(shelfId3))
                {
                    if (item.Value.Item1 != null)
                    {
                        RaiseRemovePackageEvent(item.Value.Item1, UniqueId);
                    }
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
                        if (!(packagesizew < i.Value.Item3 && packagesizeh < i.Value.Item4))
                        {
                            throw new PackageInvalidException($"Package is too big. Package width: {packagesizew} - Shelf width {i.Value.Item3} Package height: {packagesizeh} | Shelf height {i.Value.Item4}");

                        }
                        yourStorageDict[i.Key] = (package, i.Value.Item2, i.Value.Item3, i.Value.Item4, true);
                        RaisePackagePlacedEvent(package, UniqueId);
                        break;
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
                RaiseMovePackageEvent(package, UniqueId);
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
                RaiseMovePackageToTerminalEvent(tmp, terminal);
                terminal.AddPackage(tmp);
            }
            if (tmp == null)
            {
                throw new PackageInvalidException($"The package {package.Name} was not found. Could not send it to terminal");
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
        public string? GetStorageNameById(string shelfNumber)
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
            throw new ArgumentException("The shelf number does not exist");
        }


        /// <summary>
        /// It will find the shelf where the packageId is located.
        /// </summary>
        /// <param name="packageId">A package</param>
        /// <returns>Returns the section of set package id is located, else returns nothing</returns>
        public string GetPackageSectionById(string packageId)
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
        public string GetPackageById(string packageId)
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
        /// Finds the package location by using the package id
        /// </summary>
        /// <param name="packageId">id of the package</param>
        /// <returns>Package object</returns>
        public Package? GetPackage(string packageId)
        {
            foreach (KeyValuePair<string, (Package?, string, double, double, bool)> item in yourStorageDict)
            {
                if (item.Value.Item1 is not null && item.Value.Item1.PackageId == packageId)
                {
                    return item.Value.Item1;
                }
            }
            return null; 
        }

        /// <summary>
        /// Finds the number of the unit the package is placed.
        /// </summary>
        /// <param name="package">Package object you want to search for</param>
        /// <returns>Unique id placement of package</returns>
        public string? GetPackagePlacement(Package package)
        {
            foreach (KeyValuePair<string, (Package?, string, double, double, bool)> item in yourStorageDict)
            {
                string[] keysplit = item.Key.Split(':');
                string key1 = keysplit[1];
                string yournumber = key1.Trim();
                if (item.Value.Item1 == package)
                {
                    return yournumber;
                }
            }
            return null;
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
        /// Sets the directions of accesspoint to the storage collectivly. True if can access, else false.
        /// </summary>
        /// <param name="north">Bool</param>
        /// <param name="east">Bool</param>
        /// <param name="south">Bool</param>
        /// <param name="west">Bool</param>
        public void SetAccessDirection(bool north, bool east, bool south, bool west)
        {
            northAccess = north;
            southAccess = south;
            westAccess = west;
            eastAccess = east;
        }

        /// <summary>
        /// Check if you can access the storage from the north side.
        /// </summary>
        public bool NorthAccess
        {
            get { return northAccess; }
            set { northAccess = value; }
        }

        /// <summary>
        /// Check if you can access the storage from the south side.
        /// </summary>
        public bool SouthAccess
        {
            get { return southAccess; }
            set { southAccess = value; }
        }

        /// <summary>
        /// Check if you can access the storage from the east side
        /// </summary>
        public bool EastAccess
        {
            get { return eastAccess; }
            set { eastAccess = value; }
        }

        /// <summary>
        /// Check if you can access the storage from the west side
        /// </summary>
        public bool WestAccess
        {
            get { return westAccess; }
            set { westAccess = value; }
        }

        /// <summary>
        /// Gets the goodstype of the shelf
        /// </summary>
        public string GoodsType
        {
            get { return goodsType; }
            set { goodsType = value; }
        }

        /// <summary>
        /// Storageunit identifyer
        /// </summary>
        public string UniqueId
        {
            get { return uniqueId; }
            set { uniqueId = value; }
        }

        /// <summary>
        /// Configures diffrent sizes that a complete Warehouse storageunit contains.
        /// </summary>
        private class ShelvesAdd
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
        public void UnitShelfsPrint()
        {
            foreach (Storage.ShelvesAdd Item in addShelves)
            {
                Console.WriteLine("StorageName: " + Item.SizeName + " TotalUnits: " + Item.TotalUnitsAvailable + " Max Length CM: " + Item.MaxHeightCm + " Max width CM: " + Item.MaxWidthCm);
            }
        }

    }
}