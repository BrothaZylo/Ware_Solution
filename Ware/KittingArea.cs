using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ware.Packages;

namespace Ware
{
    /// <summary>
    /// Kitting area - packing packages in boxes
    /// </summary>
    public class KittingArea: IKittingArea
    {
        private List<Package> packagesGoingToKittingArea = new List<Package>();
        private List<Package> packagesInKittingArea = new List<Package>();
        private List<KittingBox> kittingBoxes = new List<KittingBox>();
        private int totalBoxesAvailable;
        private int maxPackagesPerBox;
        private string kittingName;
        private int kittingBoxCount = 1;

        /// <summary>
        /// An area where packages are packed together into cardboard boxes.
        /// </summary>
        /// <param name="name">The name of the KittingArea.</param>
        /// <param name="initialBoxCount">Total cardboard boxes available.</param>
        /// <param name="packagesPerBox">The total number of packages that can be placed in a box.</param>
        public KittingArea(string name, int initialBoxCount = 20, int packagesPerBox = 5)
        {
            kittingName = name;
            totalBoxesAvailable = initialBoxCount;
            maxPackagesPerBox = packagesPerBox;
        }

        /// <summary>
        /// Creates a kittingbox
        /// </summary>
        public void CreateKittingBox(string packageName, string goodsType, double packageHeightCm, double packageWidthCm)
        {
            KittingBox newKittingBox = new KittingBox(packageName + " " + kittingBoxCount, goodsType, packageHeightCm, packageWidthCm);
            kittingBoxes.Add(newKittingBox);
            kittingBoxCount++;
            newKittingBox.MaxPackagesPerBox = maxPackagesPerBox;
        }

        /// <summary>
        /// Adds a package to a box
        /// </summary>
        /// <param name="kitBox">wich box</param>
        /// <param name="package">package in kittingarea</param>
        public void AddPackageToKittingBox(KittingBox kitBox, Package package)
        {
            foreach (KittingBox box in kittingBoxes)
            {
                if (box == kitBox)
                {
                    if (box.GetPackages().Count < maxPackagesPerBox)
                    {
                        box.AddPackage(package);
                        RaisePackageAddToBoxEvent(package);
                        packagesInKittingArea.Remove(package);
                        return;
                    }
                    else
                    {
                        throw new InvalidOperationException("Kitting box full.");
                    }
                }
            }
            throw new InvalidOperationException("Kitting box is not in KittingArea.");
        }

        /// <summary>
        /// Send a box to a chosen location
        /// </summary>
        /// <param name="box">box you want to send</param>
        /// <returns>the box if its in the kittingarea, else null</returns>
        public KittingBox? SendBox(KittingBox box)
        {
            foreach(KittingBox kittingBox in kittingBoxes)
            {
                if(box == kittingBox)
                {
                    kittingBoxes.Remove(kittingBox);
                    return box;
                }
            }
            return null;
        }

        /// <summary>
        /// Sets the maximum number of packages that each box can contain.
        /// </summary>
        /// <param name="maxPackages">The maximum number of packages.</param>
        /// <exception cref="ArgumentException">Thrown when if maximum number of packages is less than 1.</exception>
        public void SetMaxPackagesPerBox(int maxPackages)
        {
            if (maxPackages < 1)
            {
                throw new ArgumentException("The number of packages per box must be greater than 0.");
            }

            maxPackagesPerBox = maxPackages;
        }

        /// <summary>
        /// Sets the total number of cardboard boxes available.
        /// </summary>
        /// <param name="boxes">The total number of boxes.</param>
        /// <exception cref="ArgumentException">Thrown when if number of boxes is less than 0.</exception>
        public void SetTotalBoxesAvailable(int boxes)
        {
            if (boxes < 0)
            {
                throw new ArgumentException("The total number of boxes cannot be negative.");
            }

            totalBoxesAvailable = boxes;
        }

        /// <summary>
        /// Packages located in KittingArea
        /// </summary>
        /// <param name="package">package object you want to put in KittingArea</param>
        public void AddPackageToKittingArea(Package package)
        {
            packagesInKittingArea.Add(package);
        }

        /// <summary>
        /// Indivates if a package is going to KittingArea
        /// </summary>
        /// <param name="package">package object you want to put in KittingArea</param>
        public void SchedulePackageForKittingArea(Package package)
        {
            packagesGoingToKittingArea.Add(package);
        }

        /// <summary>
        /// Checks if the current box is full.
        /// </summary>
        /// <returns>true if the current box is full, otherwise false.</returns>
        public bool IsBoxFull(KittingBox box)
        {
            if (box.GetPackages().Count == maxPackagesPerBox)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Gets all the kittingboxes in the kitting area
        /// </summary>
        /// <returns>list of all kittingboxes</returns>
        public List<KittingBox> GetKittingBoxesInKittingArea()
        {
            return kittingBoxes;
        }

        /// <summary>
        /// Gets all the packages in Kitting Area
        /// </summary>
        /// <returns>List of all the packages in KittingArea</returns>
        public List<Package> GetPackagesInKittingArea()
        {
            return packagesInKittingArea;
        }

        /// <summary>
        /// Gets all the packages wich are scheduled to go to kitting area in Kitting Area
        /// </summary>
        /// <returns>List of all the packages in KittingArea</returns>
        public List<Package> GetPackagesGoingToKittingArea()
        {
            return packagesGoingToKittingArea;
        }

        /// <summary>
        /// Amount of boxes available for kitting
        /// </summary>
        public int TotalBoxesAvailable
        {
            get { return totalBoxesAvailable; }
            set { totalBoxesAvailable = value; }
        }

        /// <summary>
        /// Max amount of max boxes available
        /// </summary>
        public int MaxPackagesPerBox
        {
            get { return maxPackagesPerBox; }
            set { maxPackagesPerBox = value; }
        }

        /// <summary>
        /// Name of KittingArea.
        /// </summary>
        public string KittingName
        {
            get { return kittingName; }
            set { kittingName = value; }
        }
        
        /// <summary>
        /// Used for AddPackageToBox(Package package)
        /// </summary>
        public event EventHandler<PackageEventArgs>? PackageAddToBoxEvent;

        private void RaisePackageAddToBoxEvent(Package package)
        {
            PackageAddToBoxEvent?.Invoke(null, new PackageEventArgs(package));
        }
    }
}