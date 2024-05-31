using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ware.PalletStorages
{
    internal interface IPalletAisle
    {
        void AddPalletStorage(PalletStorage palletStorage);

        void RemovePalletStorage(PalletStorage palletStorage);

        void GetAllPalletPrints();

    }
}
