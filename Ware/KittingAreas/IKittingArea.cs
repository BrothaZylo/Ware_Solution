using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ware.Packages;

namespace Ware.KittingAreas
{
    public interface IKittingArea
    {
        void CreateKittingBox(string packageName, string goodsType, double packageHeightCm, double packageWidthCm);
        void AddPackageToKittingBox(KittingBox kitBox, Package package);
        KittingBox? SendBox(KittingBox box);
        void SetMaxPackagesPerBox(int maxPackages);
        void SetTotalBoxesAvailable(int boxes);
        void AddPackageToKittingArea(Package package);
        void SchedulePackageForKittingArea(Package package);
        bool IsBoxFull(KittingBox box);
        List<Package> GetPackagesGoingToKittingArea();
        List<Package> GetPackagesInKittingArea();
        List<KittingBox> GetKittingBoxesInKittingArea();
    }
}
