using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ware
{
    internal interface IPalletStorage
    {
        public void BuildStorage();
        void PlacePallet(Pallet pallet, string shelfId, int floor);
        void RemovePallet(string shelfId, int floor);
        void PrintAllPalletStorageInformation();
        void AddShelf(string sizeName, int totalUnitsAvailable, int floors);
        void SetAccessDirection(bool north, bool east, bool south, bool west);

    }
}
