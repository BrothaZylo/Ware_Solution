using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Ware
{
    public class ShelvesUnits(string nameofstorage, int totalspaceavailable, List<WareHouseSizeConfig> configuresize) : IWareHouse
    {
        public string shelfcategory = nameofstorage;
        public int totalspace = totalspaceavailable;
        public List<WareHouseSizeConfig> configuresizes = configuresize;
    }
    public List<WareHouseSizeConfig> ShowWareHouseTest(List<WareHouseSizeConfig> configuresize)
    {
        foreach(WareHouseSizeConfig config in configuresize)
        {
            Console.WriteLine(config);
        }
        return configuresize;
    }

    public class WareHouseSizeConfig
    {
        public required string Sizename { get; set; }
        public int Totalunitsavailable { get; set; }
        public double Maxwidthcm { get; set; }
        public double Maxlengthcm { get; set; }
    }
}
