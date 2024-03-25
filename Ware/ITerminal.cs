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
        List<Package> GetPackagesInTerminal();
        void ClearPackages();

        void SendAllPackages();

    }
}
