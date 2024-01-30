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
        Dictionary<string, (string, string, string, string, string, bool)> yourWareList = [];

        public class WareHouseSizeConfig
        {
            public required string Sizename { get; set; }
            public int Totalunitsavailable { get; set; }
            public double Maxwidthcm { get; set; }
            public double Maxlengthcm { get; set; }
        }

        public void WareHouseConfigPrint()
        {
            foreach (var Item in Configfiles)
            {
                Console.WriteLine("StorageName: " + Item.Sizename + " TotalUnits: " + Item.Totalunitsavailable + " Max Length CM: " + Item.Maxlengthcm + " Max Width CM: " + Item.Maxwidthcm);
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
                    yourWareList.Add("EmptySlot: " +StorageCounter,("ShelfID: " + StorageCounter, "Type: "+ Shelfcategory,"Type of storage: "+j.Sizename, "Max width cm: "+j.Maxwidthcm, "Max length cm: " + j.Maxlengthcm, false));
                    StorageCounter++;
                }
            }
        }
        
        public void GetAllStorageInformationPrint()
        {
            foreach(var i in yourWareList)
            {
                Console.WriteLine(i);
            }
        }

        public void FindPackageSectionByIdPrint(string packageid)
        {
            string item = "";
            foreach( var i in yourWareList)
            {
                if (i.Key == packageid)
                {
                    item+=i;
                }
            }
            Console.WriteLine(item);
        }

        public string FindPackageById(string packageid)
        {
            //change to int or something :)
            string item = "Does not exist";
            foreach (var i in yourWareList)
            {
                if (i.Key == packageid)
                {
                    return i.Value.Item1;
                }
            }
            return item;
        }

    }
}
