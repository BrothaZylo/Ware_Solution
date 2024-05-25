using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ware.Packages;

namespace Ware
{
    public class KittingArea: IKittingArea
    {
        private List<Package> packagesInBox = new List<Package>();
        private List<Package> packagesGoingToKittingArea = new List<Package>();
        private List<Package> packagesInKittingArea = new List<Package>();
        private int totalBoxesAvailable;
        private int maxPackagesPerBox;
        private string kittingName;

        /// <summary>
        /// An area where packages are packed together into cardboard boxes.
        /// </summary>
        /// <param name="name">The name of the KittingArea.</param>
        /// <param name="initialBoxCount">Total cardboard boxes available.</param>
        /// <param name="maxPackagesPerBox">The total number of packages that can be placed in a box.</param>
        public KittingArea(string name, int initialBoxCount = 20, int maxPackagesPerBox = 10)
        {
            kittingName = name;
            totalBoxesAvailable = initialBoxCount;
            this.maxPackagesPerBox = maxPackagesPerBox;
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
        /// Checks if the current box is full.
        /// </summary>
        /// <returns>true if the current box is full, otherwise false.</returns>
        public bool IsBoxFull()
        {
            return packagesInBox.Count > maxPackagesPerBox - 1;
        }

        /// <summary>
        /// Gets the number of boxes remaining.
        /// </summary>
        /// <returns>The number of remaining boxes.</returns>
        public int BoxesRemaining()
        {
            return totalBoxesAvailable;
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
        /// Adds a package to the current cardboard box.
        /// </summary>
        /// <param name="package">The package that gets added to the box.</param>
        /// <exception cref="InvalidOperationException">Thrown when there are no cardboard boxes available.</exception>
        public void AddPackageToBox(Package package)
        {
            if (totalBoxesAvailable == 0)
            {
                throw new InvalidOperationException("No cardboard boxes available.");
            }

            if (packagesInBox.Count + 1 > maxPackagesPerBox)
            {
                PrepareNewBox();
            }
            for (int i = 0; i < packagesInKittingArea.Count; i++)
            {
                if (packagesInKittingArea[i] == package)
                {
                    RaisePackageAddToBoxEvent(package);
                    packagesInBox.Add(package);
                    packagesInKittingArea.RemoveAt(i);
                    return;
                }
            }
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
        /// Turns the box into a package object.
        /// </summary>
        /// <returns>Returns a new pacakge object..</returns>
        /// <exception cref="InvalidOperationException">Thrown when there's no packages in the box.</exception>
        public Package TurnBoxIntoPackage()
        {
            if (packagesInBox.Count == 0)
            {
                throw new InvalidOperationException("No packages in the box.");
            }

            string boxName = "Undefined";
            string goodsType = "Undefined";
            double totalHeight = packagesInBox.Max(p => p.Height);
            double totalWidth = packagesInBox.Max(p => p.Width);

            Package boxPackage = new Package(boxName, goodsType, totalHeight, totalWidth);

            packagesInBox.Clear();

            if (totalBoxesAvailable > 0)
                totalBoxesAvailable--;

            return boxPackage;
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
        /// Prepares a new box by clearing the current box and reduces the available box count.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown when there are no new boxes available to prepare.</exception>
        private void PrepareNewBox()
        {
            if (totalBoxesAvailable < 1)
            {
                throw new InvalidOperationException("No new boxes available to prepare.");
            }

            packagesInBox.Clear();
            totalBoxesAvailable--;
        }

        /// <summary>
        /// Used for AddPackageToBox(Package package)
        /// </summary>
        public event EventHandler<PackageEventArgs>? PackageAddToBoxEvent;

        private void RaisePackageAddToBoxEvent(Package package)
        {
            PackageAddToBoxEvent?.Invoke(this, new PackageEventArgs(package));
        }
    }
}