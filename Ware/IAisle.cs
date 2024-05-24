﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ware
{
    public interface IAisle
    {
        void AddStorage(Storage storage);
        void removeStorage(Storage storage);
        void GetPackagesInAislesPrint();
        string? FindPackage(Package package);

    }
}
