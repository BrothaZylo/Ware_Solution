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
        private int maxPackages = 10;

        /// <summary>
        /// Adds a package in the box
        /// </summary>
        /// <param name="package">the package being placed in the box</param>
        public void AddPackage(Package package)
        {
            boxes.Add(package);
        }

        /// <summary>
        /// Gets a packages in the box.
        /// </summary>
        /// <returns>Returns a list of packages that is in the box</returns>
        public List<Package> GetPackages()
        {
            return boxes;
        }

        /// <summary>
        /// Clears the box list.
        /// </summary>
        public void ClearPackages()
        {
            boxes.Clear();
        }

        /// <summary>
        /// Getter and setter for max amount of packages per box
        /// </summary>
        public int MaxPackagesPerBox
        {
            get { return maxPackages; }
            set { maxPackages = value; }
        }

    }
}
