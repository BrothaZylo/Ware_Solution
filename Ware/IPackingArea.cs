﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ware
{
    internal interface IPackingArea
    {
        void AddPackageToPallet(Package package);
        bool IsPalletFull();
        void SetMaxPackagesPerPallet(int maxPackages);
    }
}
