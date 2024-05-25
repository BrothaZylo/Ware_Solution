using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ware.Packages;

namespace Ware
{
    internal interface IPackingArea
    {
        void ReceivePackage(Package package);
        void AddToPallet(Package package, Pallet pallet);
    }
}
