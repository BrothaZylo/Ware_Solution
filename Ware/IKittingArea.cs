using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ware
{
    public interface IKittingArea
    {
        void SchedulePackageForKittingArea(Package package);
        void AddPackageToKittingArea(Package package);
        void AddPackageToBox(Package package);
        bool IsBoxFull();
        void SetMaxPackagesPerBox(int maxPackages);
        void SetTotalBoxesAvailable(int boxes);
        int BoxesRemaining();
        List<Package> GetPackagesInKittingArea();
        List<Package> GetPackagesGoingToKittingArea();
    }
}
