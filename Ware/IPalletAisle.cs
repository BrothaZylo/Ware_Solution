using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ware
{
    internal interface IPalletAisle
    {
        public void AddPalletStorage(PalletStorage palletStorage);

        public void RemovePalletStorage(PalletStorage palletStorage);

        public void GetAllPalletPrints();

    }
}
