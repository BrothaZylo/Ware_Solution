using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ware.Packages;
using Ware.Pallets;

namespace Ware.PackingAreas
{
    internal interface IPackingArea
    {
        void ReceivePackage(Package package);
        void AddToPallet(Package package, Pallet pallet);
    }
}
