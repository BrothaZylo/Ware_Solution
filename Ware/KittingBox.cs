using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Ware.Packages;

namespace Ware
{
    public class KittingBox : Package
    {
        private List<Package> boxes = new List<Package>();

        public KittingBox(string packageName = "Consolidated Box", string goodsType = "Mixed Goods", double packageHeightCm = 10, double packageWidthCm = 10)
            : base(packageName, goodsType, packageHeightCm, packageWidthCm)
        {
        }

        public void AddPackage(Package package)
        {
            boxes.Add(package);
        }

        public List<Package> GetPackages()
        {
            return boxes;
        }

        public void ClearPackages()
        {
            boxes.Clear();
        }
    }
}
