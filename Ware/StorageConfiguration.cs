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
        Dictionary<string, (string, string, double, double, bool)> yourWareList = [];


        public void GetSizeConfig()
        {
            foreach (var Item in Configfiles)
            {
                Console.WriteLine("StorageName: " + Item.Sizename + " TotalUnits: " + Item.Totalunitsavailable + " Max Length CM: " + Item.Maxlengthcm + " Max Width CM: " + Item.Maxwidthcm);
            }
        }

        public class WareHouseSizeConfig
        {
            public required string Sizename { get; set; }
            public int Totalunitsavailable { get; set; }
            public double Maxwidthcm { get; set; }
            public double Maxlengthcm { get; set; }
        }

        public void CreateStorage()
        {
            int StorageCounter = 1;

            foreach(var j in Configfiles)
            {
                for(int k = 0; k < j.Totalunitsavailable; k++)
                {
                    yourWareList.Add("EmptySlot: " +StorageCounter,("ShelfID: " + StorageCounter, "Type: "+ Shelfcategory, j.Maxwidthcm, j.Maxlengthcm, false));
                    StorageCounter++;
                }
            }
        }
        
        public void GetStorage()
        {
            foreach(var i in yourWareList)
            {
                Console.WriteLine(i);
            }
        }

        public string FindPackageById(string packageid)
        {
            string item = "";
            foreach( var i in yourWareList)
            {
                if (i.Key == packageid)
                {
                    item+=i;
                }
            }
            return item;
        }


    }
}
