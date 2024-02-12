﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ware
{
    internal interface ITerminal
    {
        void AddPackage(CreatePackage packages);
        List<CreatePackage> GetPackagesInTerminal();
        void RemoveAllPackages();

    }
}
