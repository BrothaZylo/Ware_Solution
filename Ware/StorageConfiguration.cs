﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Ware
{
    /// <summary>
    /// Preconfig of the storageunits wich will later be used to create shelves.
    /// </summary>
    /// <param name="nameofstorage"></param>
    /// <param name="totalspaceavailable"></param>
    /// <param name="configuresize"></param>
    /// <param name="configuretime"></param>
    public class StorageConfiguration(string nameofstorage, int totalspaceavailable, List<StorageConfiguration.WareHouseSizeConfig> configuresize, List<StorageConfiguration.WareHouseTimeConfig> configuretime) : IStorageConfiguration
    {
        public string ShelfCategory = nameofstorage;
        public int TotalSpace = totalspaceavailable;
        public List<WareHouseSizeConfig> ConfigFiles = configuresize;
        public List<WareHouseTimeConfig> ConfigTime = configuretime;
        Dictionary<string, (string, string, double, double, bool)> YourWareList = [];
        
        /// <summary>
        /// Configures the time-progress from start to end-point.
        /// </summary>
        public class WareHouseTimeConfig
        {
            public int TimeDeliveryToStorageMinutes;
            public int TimeStorageToTerminalMinutes;
        }

        /// <summary>
        /// Configures diffrent sizes that a complete Warehouse storageunit contains.
        /// </summary>
        public class WareHouseSizeConfig
        {
            public required string SizeName { get; set; }
            public int TotalUnitsAvailable { get; set; }
            public double MaxWidthCm { get; set; }
            public double MaxHeightCm { get; set; }
        }

        /// <summary>
        /// Prints the diffrent Size configs for each size created.
        /// </summary>
        public void WareHouseConfigPrint()
        {
            foreach (StorageConfiguration.WareHouseSizeConfig Item in ConfigFiles)
            {
                Console.WriteLine("StorageName: " + Item.SizeName + " TotalUnits: " + Item.TotalUnitsAvailable + " Max Length CM: " + Item.MaxHeightCm + " Max Width CM: " + Item.MaxWidthCm);
            }
        }

        /// <summary>
        /// Creates the Storageunit based on instructions from the config && constructor.
        /// </summary>
        public void CreateStorage()
        {
            int StorageCounter = 1;
            foreach(StorageConfiguration.WareHouseSizeConfig j in ConfigFiles)
            {
                for (int k = 0; k < j.TotalUnitsAvailable; k++)
                {
                    YourWareList.Add(ShelfCategory + "ShelfID: " + StorageCounter, ("PackageID: Empty", "Type of storage: " + j.SizeName, j.MaxWidthCm, j.MaxHeightCm, false));
                    StorageCounter++;
                }
            }
        }

        /// <summary>
        /// Places the package in the storagehouse if the size && goodstype fits the slot.
        /// </summary>
        /// <param name="package"></param>
        /// <returns></returns>
        public string PlacePackage(CreatePackage package)
        {
            double packagesizew = package.Width;
            double packagesizeh = package.Height;
            foreach (KeyValuePair<string, (string, string, double, double, bool)> i in YourWareList)
            {
                if (ShelfCategory == package.Goods)
                {
                    if (i.Value.Item1 == package.PackageId)
                    {
                        return "Package is already in storagehouse";
                    }
                    if (i.Value.Item5 == false)
                    {
                        if (packagesizew < i.Value.Item3 && packagesizeh < i.Value.Item4)
                        {
                            YourWareList[i.Key] = (package.PackageId, i.Value.Item2, i.Value.Item3, i.Value.Item4, true);
                            return "Package was placed in: " + i.Key;
                        }
                    }
                }
            }
            return "No suitable place found";
        }

        /// <summary>
        /// inputting the packageid will remove the package from the shelf its placed.
        /// </summary>
        /// <param name="packageid"></param>
        /// <returns>it will return the packageid if it finds the packackage, else it will return null</returns>
        public string MovePackageById(string packageid)
        {
            foreach (KeyValuePair<string, (string, string, double, double, bool)> i in YourWareList)
            {
                if (i.Value.Item1 == packageid)
                {
                    YourWareList[i.Key] = ("PackageID: Empty", i.Value.Item2, i.Value.Item3, i.Value.Item4, false);
                    return packageid;
                }
            }
            return "null";
        }
        
        /// <summary>
        /// Moves the package from the shelf and returns the it in package format.
        /// </summary>
        /// <param name="package"></param>
        /// <returns>if it find the package it will return the package, else it will return a nulled package format</returns>
        public CreatePackage MovePackage(CreatePackage package)
        {
            foreach(KeyValuePair<string, (string, string, double, double, bool)> i in YourWareList)
            {
                if (i.Value.Item1 == package.PackageId)
                {
                    YourWareList[i.Key] = ("PackageID: Empty", i.Value.Item2, i.Value.Item3, i.Value.Item4, false);
                    return package;
                }
            }
            CreatePackage dummy = new("null", "null", "null", 0, 0);

            return dummy;
        }

        /// <summary>
        /// Prints the entire storage house shelf unit.
        /// </summary>
        public void GetAllStorageInformationPrint()
        {
            foreach(KeyValuePair<string, (string, string, double, double, bool)> i in YourWareList)
            {
                Console.WriteLine(i);
            }
        }
        public Dictionary<string, (string, string, double, double, bool)> GetAllStorageInformationAsDictionary()
        {
            return YourWareList;
        }

        /// <summary>
        /// Entering the storage unit's number it will search for the units nr.
        /// </summary>
        /// <param name="shelfnumber"></param>
        /// <returns>Returns the shelf number else, it will return Does not exist</returns>
        public string GetStorageNameById(int shelfnumber)
        {
            foreach(KeyValuePair<string, (string, string, double, double, bool)> i in YourWareList)
            {
                string[] keysplit = i.Key.Split(':');
                string key1 = keysplit[0];
                string yournumber = key1 + ": " + shelfnumber;
                if (i.Key == yournumber)
                {
                    return yournumber;
                }
            }
            return "Does not exist";
        }

        /// <summary>
        /// It will find the shelf where the packageid is located.
        /// </summary>
        /// <param name="packageid"></param>
        /// <returns>Returns the section of set package id is located, else returns nothing</returns>
        public string FindPackageSectionById(string packageid)
        {
            string item = "";
            foreach(KeyValuePair<string, (string, string, double, double, bool)> i in YourWareList)
            {
                if (i.Value.Item1 == packageid)
                {
                    item += i;
                }
            }
            return item;
        }

        /// <summary>
        /// Finds the package location by using the id
        /// </summary>
        /// <param name="packageid"></param>
        /// <returns>The shelf its placed at, else will return Does not exist</returns>
        public string FindPackageById(string packageid)
        {
            //change to int mby or not xd :)
            string item = "Does not exist";
            foreach (KeyValuePair<string, (string, string, double, double, bool)> i in YourWareList)
            {
                if (i.Value.Item1 == packageid)
                {
                    return i.Key;
                }
            }
            return item;
        }

        /// <summary>
        /// It checks if the spot is taken by another package
        /// </summary>
        /// <param name="storagename"></param>
        /// <returns>returns true if taken, else false</returns>
        public bool IsSpotTaken(string storagename)
        {
            foreach(KeyValuePair<string, (string, string, double, double, bool)> i in YourWareList)
            {
                if (i.Key == storagename)
                {
                    return i.Value.Item5;
                }
            }
            return false;
        }

        /// <summary>
        /// Checks if package good == warehouse goods
        /// </summary>
        /// <param name="package"></param>
        /// <returns>returns true if package good == warehouse goods, else false</returns>
        public bool IsSameTypeOfGoods(CreatePackage package)
        {
            foreach (KeyValuePair<string, (string, string, double, double, bool)> i in YourWareList)
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
        /// It will find the time from Delivery To storageunit based from the config
        /// </summary>
        /// <returns>x amount of minutes, else 0</returns>
        public int GetTimeDeliveryToStorage()
        {
            foreach (StorageConfiguration.WareHouseTimeConfig i in ConfigTime)
            {
                return i.TimeDeliveryToStorageMinutes;
            }
            return 0;
        }

        /// <summary>
        /// Finds the time it takes from storage to terminal based on config.
        /// </summary>
        /// <returns>x amount of time, else 0</returns>
        public int GetTimeStorageToTerminal()
        {
            foreach (StorageConfiguration.WareHouseTimeConfig i in ConfigTime)
            {
                return i.TimeStorageToTerminalMinutes;
            }
            return 0;
        }

        /// <summary>
        /// It will find the time from Delivery To storageunit based from the config and converts it into seconds
        /// </summary>
        /// <returns></returns>
        public int GetTimeDeliveryToStorageSeconds()
        {
            foreach (StorageConfiguration.WareHouseTimeConfig i in ConfigTime)
            {
                return i.TimeDeliveryToStorageMinutes*60;
            }
            return 0;
        }

        /// <summary>
        /// Finds the time it takes from storage to terminal based on config and converts it into seconds.
        /// </summary>
        /// <returns></returns>
        public int GetTimeStorageToTerminalSeconds()
        {
            foreach (StorageConfiguration.WareHouseTimeConfig i in ConfigTime)
            {
                return i.TimeStorageToTerminalMinutes*60;
            }
            return 0;
        }

    }
}