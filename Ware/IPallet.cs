﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ware
{
    internal interface IPallet
    {
        IReadOnlyList<Package> PackagesOnPallet { get; }
        void SchedulePackageToPack(Package package);
        List<Package> GetScheduledPackages();
        List<Package> GetPackagesOnPallet();
        void AddPackage(Package package);
        void ClearPallet();
    }
}
