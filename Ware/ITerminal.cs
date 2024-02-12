using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ware
{
    internal interface ITerminal
    {
        public void AddPackage(CreatePackage packages);
        public List<CreatePackage> GetPackagesInTerminal();


    }
}
