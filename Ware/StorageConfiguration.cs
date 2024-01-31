using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Ware
{
    public class StorageConfiguration(string nameofstorage, int totalspaceavailable, List<StorageConfiguration.WareHouseSizeConfig> configuresize) : IWareHouse
    {
        public string Shelfcategory = nameofstorage;
        public int Totalspace = totalspaceavailable;
        public List<WareHouseSizeConfig> Configfiles = configuresize;
        Dictionary<string, (string, string, string, double, double, bool)> yourWareList = [];

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
            //bool -> if the space is taken
            //reminder: remove extra info (change string to double (dict(constuctor)) -> cm usages) on use :D
            foreach(var j in Configfiles)
            {
                for(int k = 0; k < j.Totalunitsavailable; k++)
                {
                    yourWareList.Add("ShelfID: " +StorageCounter,("PackageID: Empty", "Type: "+ Shelfcategory,"Type of storage: "+j.Sizename, j.Maxwidthcm, j.Maxheightcm, false));
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
                if (i.Value.Item1 == package.packageid)
                {
                    return "Package is already in storagehouse";
                }
                if(i.Value.Item6 == false)
                {
                    if(packagesizew < i.Value.Item4 && packagesizeh < i.Value.Item5)
                    {
                        yourWareList[i.Key] = (package.packageid, i.Value.Item2, i.Value.Item3, i.Value.Item4, i.Value.Item5, true);
                        return "Package was placed in: "+i.Key;
                    }
                }
            }
            return "No suitable place found";
        }
        
        public void GetAllStorageInformationPrint()
        {
            foreach(var i in yourWareList)
            {
                Console.WriteLine(i);
            }
        }

        public string FindPackageSectionById(string packageid)
        {
            string item = "";
            foreach( var i in yourWareList)
            {
                if (i.Value.Item1 == packageid)
                {
                    item+=i;
                }
            }
            return item;
        }

        public string FindPackageById(string packageid)
        {
            //change to int or something :)
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

    }
}
