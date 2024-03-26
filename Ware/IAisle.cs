using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ware
{
    internal interface IAisle
    {
        public void AddStorage(Storage storage);
        public void removeStorage(Storage storage);
        public void GetPackagesInAislesPrint();
        public string FindPackage(Package package);

    }
}
