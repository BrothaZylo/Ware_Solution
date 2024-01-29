using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Ware
{
    public class StorageConfiguration(string nameofstorage, int totalspaceavailable, List<StorageConfiguration.WareHouseSizeConfig> configuresize) : IWareHouse
    {
        public string Shelfcategory = nameofstorage;
        public int Totalspace = totalspaceavailable;
        public List<WareHouseSizeConfig> Configfiles = configuresize;

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

        public string CreateStorage()
        {
            Dictionary<int, string> YourStorageUnit = [];

            for (int i = 0; i < Configfiles.Count; i++)
            {
                //get all the space.
            }
            return "";
        }


    }
}
