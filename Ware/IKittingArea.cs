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
        void CreateKittingBox();
        void AddPackageToBox(Package package);
        bool IsBoxFull(KittingBox box);
        void SetMaxPackagesPerBox(int maxPackages);
        void SetTotalBoxesAvailable(int boxes);
        KittingBox? SendBox(KittingBox box);
        void AddPackageToKittingBox(KittingBox kitBox, Package package);
        void AddPackageToKittingArea(Package package);
        void PrepareNewBox();
        void PrintAllKittingBoxes();
        void SchedulePackageForKittingArea(Package package);
        List<Package> GetPackagesGoingToKittingArea();
        List<Package> GetPackagesInKittingArea();
    }
}
