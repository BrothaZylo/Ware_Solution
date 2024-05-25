using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ware.Packages;

namespace Ware.Terminals
{
    internal interface ITerminal
    {
        void AddPackage(Package packages);
        void AddPallet(Pallet pallet);
        List<Package> GetPackagesInTerminal();
        void PrintPackageList();
        void PrintPalletsInformation();
        void ClearPackages();
        void SendAllPackages();
        Package? SendPackage(Package package);
        void SendOutPallet(Pallet pallet);
        void SendOutPallets();






    }
}
