using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ware
{
    internal interface IPackingArea
    {
        void SendPackageToPackingArea(Package package);
        void AddToPallet(Package package, Pallet pallet);
        IReadOnlyList<Pallet> GetAllPallets();
        void RemovePackageFromPallet(Package package, Pallet pallet);
        void MovePackageToAnotherPallet(Package package, Pallet sourcePallet, Pallet targetPallet);
        event EventHandler<PackageEventArgs> PackageAddedToPallet;
    }
}
