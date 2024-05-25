using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ware.Terminals;

namespace Ware
{
    internal interface IPalletStorage
    {
        Dictionary<string, (List<Pallet>, string, bool)> GetAllPalletsInStorage();
        void BuildStorage();
        void PlacePallet(Pallet pallet, string shelfId);
        void PlacePalletAutomatic(Pallet pallet);
        void RemovePallet(Pallet pallet);
        void PrintAllPalletStorageInformation();
        void SendPalletToTerminal(Pallet pallet, Terminal terminal);
        void SendsAllPalletsToTerminal(Terminal terminal);
        Pallet? GetPallet(Pallet pallet);
        void AddShelf(string sizeName, int totalUnitsAvailable);
        void SetAccessDirection(bool north, bool east, bool south, bool west);

    }
}