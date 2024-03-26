using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ware
{
    internal interface IPalletStorage
    {
        void BuildStorage();
        void PlacePallet(Pallet pallet, string shelfId, int floor, int position);
        void PlacePalletAutomatic(Pallet pallet);
        void RemovePallet(string shelfId, int floor, int position);
        void PrintAllPalletStorageInformation();
        void SendPalletToTerminal(string shelfId, int floor, int position, Terminal terminal);
        void SendPalletToTerminalAutomatic(Pallet pallet, Terminal terminal);
        void AddShelf(string sizeName, int palletsPerFloor, int floors);
        void SetAccessDirection(bool north, bool east, bool south, bool west);

    }
}
