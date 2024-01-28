using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Ware
{
    public class ShelvesUnits(string nameofstorage,int totalspaceavailable) : IWareHouse
    {
        public string shelfcategory = nameofstorage;
        public int totalspace = totalspaceavailable;
    }

}
