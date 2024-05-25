using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ware
{
    public interface IKittingArea
    {
        void SetMaxPackagesPerBox(int maxPackages);
        void SetTotalBoxesAvailable(int boxes);
        bool IsBoxFull();
        int BoxesRemaining();
        void AddPackageToKittingArea(Package package);
        void AddPackageToBox(Package package);
        void SchedulePackageForKittingArea(Package package);
        Package TurnBoxIntoPackage();
        List<Package> GetPackagesGoingToKittingArea();
        List<Package> GetPackagesInKittingArea();
    }
}
