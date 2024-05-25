using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ware.Packages;

namespace Ware
{
    public interface IKittingArea
    {
        void AddPackageToBox(Package package);
        bool IsBoxFull();
        void SetMaxPackagesPerBox(int maxPackages);
        void SetTotalBoxesAvailable(int boxes);
        int BoxesRemaining();
    }
}
