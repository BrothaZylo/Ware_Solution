using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Ware
{
    public class StorageConfiguration(string nameofstorage, int totalspaceavailable, List<StorageConfiguration.WareHouseSizeConfig> configuresize, List<StorageConfiguration.WareHouseTimeConfig> configuretime) : IWareHouse
    {
        public string Shelfcategory = nameofstorage;
        public int Totalspace = totalspaceavailable;
        public List<WareHouseSizeConfig> Configfiles = configuresize;
        public List<WareHouseTimeConfig> Configtime = configuretime;
        private Dictionary<string, (string, string, double, double, bool)> yourWareList = [];

        public class WareHouseTimeConfig
        {
            public int TimeDeliveryToStorageMinutes;
            public int TimeStorageToTerminalMinutes;
        }

        public class WareHouseSizeConfig
        {
            public required string Sizename { get; set; }
            public int Totalunitsavailable { get; set; }
            public double Maxwidthcm { get; set; }
            public double Maxheightcm { get; set; }
        }

        public void WareHouseConfigPrint()
        {
            foreach (var Item in Configfiles)
            {
                Console.WriteLine("StorageName: " + Item.Sizename + " TotalUnits: " + Item.Totalunitsavailable + " Max Length CM: " + Item.Maxheightcm + " Max Width CM: " + Item.Maxwidthcm);
            }
        }

        public void CreateStorage()
        {
            int StorageCounter = 1;
            foreach (var j in Configfiles)
            {
                for (int k = 0; k < j.Totalunitsavailable; k++)
                {
                    yourWareList.Add(Shelfcategory + "ShelfID: " + StorageCounter, ("PackageID: Empty", "Type of storage: " + j.Sizename, j.Maxwidthcm, j.Maxheightcm, false));
                    StorageCounter++;
                }
            }
        }

        public string PlacePackage(CreatePackage package)
        {
            double packagesizew = package.width;
            double packagesizeh = package.height;
            foreach (var i in yourWareList)
            {
                if (Shelfcategory == package.goods)
                {
                    if (i.Value.Item1 == package.packageid)
                    {
                        return "Package is already in storagehouse";
                    }
                    if (i.Value.Item5 == false)
                    {
                        if (packagesizew < i.Value.Item3 && packagesizeh < i.Value.Item4)
                        {
                            yourWareList[i.Key] = (package.packageid, i.Value.Item2, i.Value.Item3, i.Value.Item4, true);
                            return "Package was placed in: " + i.Key;
                        }
                    }
                }
            }
            return "No suitable place found";
        }

        public string MovePackageById(string packageid)
        {
            foreach (var i in yourWareList)
            {
                if (i.Value.Item1 == packageid)
                {
                    yourWareList[i.Key] = ("PackageID: Empty", i.Value.Item2, i.Value.Item3, i.Value.Item4, false);
                    return packageid;
                }
            }
            return "null";
        }

        public CreatePackage MovePackage(CreatePackage package)
        {
            foreach (var i in yourWareList)
            {
                if (i.Value.Item1 == package.packageid)
                {
                    yourWareList[i.Key] = ("PackageID: Empty", i.Value.Item2, i.Value.Item3, i.Value.Item4, false);
                    return package;
                }
            }
            CreatePackage dummy = new("null", "null", "null", 0, 0);

            return dummy;
        }

        public void GetAllStorageInformationPrint()
        {
            foreach (var i in yourWareList)
            {
                Console.WriteLine(i);
            }
        }

        public string GetStorageNameById(int shelfnumber)
        {
            foreach (var i in yourWareList)
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

        public string FindPackageSectionById(string packageid)
        {
            string item = "";
            foreach (var i in yourWareList)
            {
                if (i.Value.Item1 == packageid)
                {
                    item += i;
                }
            }
            return item;
        }

        public string FindPackageById(string packageid)
        {
            //change to int mby or not xd :)
            string item = "Does not exist";
            foreach (var i in yourWareList)
            {
                if (i.Value.Item1 == packageid)
                {
                    return i.Key;
                }
            }
            return item;
        }

        public bool IsSpotTaken(string storagename)
        {
            foreach (var i in yourWareList)
            {
                if (i.Key == storagename)
                {
                    return i.Value.Item5;
                }
            }
            return false;
        }

        public int GetTimeDeliveryToStorage()
        {
            foreach (var i in Configtime)
            {
                return i.TimeDeliveryToStorageMinutes;
            }
            return 0;
        }

        public int GetTimeStorageToTerminal()
        {
            foreach (var i in Configtime)
            {
                return i.TimeStorageToTerminalMinutes;
            }
            return 0;
        }

        public int GetTimeDeliveryToStorageSeconds()
        {
            foreach (var i in Configtime)
            {
                return i.TimeDeliveryToStorageMinutes * 60;
            }
            return 0;
        }

        public int GetTimeStorageToTerminalSeconds()
        {
            foreach (var i in Configtime)
            {
                return i.TimeStorageToTerminalMinutes * 60;
            }
            return 0;
        }

    }
}