using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Ware
{
    internal class ShelvesUnits() : IWareHouse
    {

        private List<Shelf> shelves;

        public ShelvesUnits() => shelves = new List<Shelf>();

        public void ConfigureShelf(string shelfname, int space, string area)
            
        {
            shelves.Add(new Shelf(shelfname, space, area));
        }

        public List<Shelf> AllShelves()
        { 
            return shelves;
        }

        public void ConfigureShelf()
        {
            throw new NotImplementedException();
        }
    }
  

    internal class Shelf(string shelfname, int space, string area)
    {
        public string shelfname1 = shelfname;
        public int space1 = space;
        public string area1 = area;

    }


}
