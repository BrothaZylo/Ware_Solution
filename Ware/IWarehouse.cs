using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ware
{
    internal interface IWarehouse
    {
        void AddPackage(Package package);
        void AddStorage(Storage storage);
    }
}
