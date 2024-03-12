using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ware
{
    public class Reol
    {
        private string reolName;
        private List<Storage> shelves;

        public Reol(string reolName) 
        {
            reolName = reolName;
            shelves = new List<Storage>();
        }

        public void AddShelves(Storage shelf)
        {
            shelves.Add(shelf);
        }

        public void removeShelves(Storage shelf)
        {
            if (shelves.Contains(shelf))
            {
                shelves.Remove(shelf);
            }
        }
        public Storage GetShelf(int shelfNumber)
        {
            if (!(shelfNumber >= 0 && shelfNumber < shelves.Count()))
            {
                throw new ArgumentOutOfRangeException("Shelf number does not exist in " + reolName);
            }
            return shelves[shelfNumber];
        }
    }
}
