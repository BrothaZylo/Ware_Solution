using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Ware.Packages;

namespace Ware
{
    public class KittingBox(string packageName, string goodsType, double packageHeightCm, double packageWidthCm) : Package(packageName, goodsType, packageHeightCm, packageWidthCm)
    {
        private List<Package> boxes = new List<Package>();
        private int maxPackages = 0;


        public void AddPackage(Package package)
        {
            boxes.Add(package);
        }

        /// <summary>
        /// Getter and setter for max amount of packages per box
        /// </summary>
        public int MaxPackagesPerBox
        {
            get { return maxPackages; }
            set { maxPackages = value; }
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
