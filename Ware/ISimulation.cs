﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ware
{
    internal interface ISimulation
    {
        void Add(Package package);
        void Run();
    }
}
