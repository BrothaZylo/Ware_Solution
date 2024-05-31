using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ware.KittingAreas;
using Ware.Packages;
using Ware.Pallets;

namespace Ware.Terminals
{
    internal interface ITerminal
    {
        void AddPackage(Package packages);
        void AddPallet(Pallet pallet);
        void AddKittingBox(KittingBox kittingBox);
        List<Package> GetPackagesInTerminal();
        void PrintPackageList();
        void PrintPalletsInformation();
        void PrintKittingBoxesInformation();
        void ClearPackages();
        void SendAllPackages();
        Package? SendPackage(Package package);
        void SendOutKittingBox(KittingBox kittingBox);
        void SendOutPallet(Pallet pallet);
        void SendOutPallets();






    }
}
