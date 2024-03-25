using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ware
{
    internal interface ITerminal
    {
        void AddPackage(Package packages);
        void AddPallet(Pallet pallet);
        List<Package> GetPackagesInTerminal();
        void PrintPalletInformation();
        void ClearPackages();

    }
}
