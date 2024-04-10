using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ware
{
    internal interface IPackingArea
    {
        void AddToPallet(Package package, Pallet pallet);
        void ReceivePackage(Package package);
    }
}
