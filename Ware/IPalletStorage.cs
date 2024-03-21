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
        void PlacePallet(Pallet pallet, string shelfId);
        void PrintAllPalletStorageInformation();
        void AddShelf(string sizeName, int totalUnitsAvailable);
        void RemovePallet(string shelfId);
    }
}
