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
        private readonly List<KittingBox> kittingBoxes = new List<KittingBox>();
        private int totalBoxesAvailable;
        private int maxPackagesPerBox;
        private string kittingName;

        /// <summary>
        /// An area where packages are packed together into cardboard boxes.
        /// </summary>
        /// <param name="name">The name of the KittingArea.</param>
        /// <param name="initialBoxCount">Total cardboard boxes available.</param>
        /// <param name="packagesPerBox">The total number of packages that can be placed in a box.</param>
        public KittingArea(string name, int initialBoxCount = 20, int packagesPerBox = 10)
        {
            kittingName = name;
            totalBoxesAvailable = initialBoxCount;
            maxPackagesPerBox = packagesPerBox;
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
            return packagesInBox.Count >= maxPackagesPerBox;
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
        /// Add Kitting boxes to kitting Area.
        /// </summary>
        /// <param name="box"></param>
        public void AddKittingBoxToArea(KittingBox box)
        {
            kittingBoxes.Add(box);
        }

        /// <summary>
        /// Adds a package to the current box.
        /// </summary>
        /// <param name="package">The package that gets added to the box.</param>
        public void AddPackageToBox(Package package)
        {
            if (totalBoxesAvailable == 0)
            {
                throw new InvalidOperationException("No boxes available.");
            }

            if (CardBox == null)
            {
                throw new InvalidOperationException("There is no kitting box set.");
            }

            if (IsBoxFull())
            {
                PrepareNewBox();
            }

            CardBox.AddPackage(package);
        }

        /// <summary>
        /// Adds packages in kitting box.
        /// </summary>
        /// <returns>Returns Kittingbox with the packages.</returns>
        /// <exception cref="InvalidOperationException">Used when there's no packages to add.</exception>
        public KittingBox AddIntoKittingBox()
        {
            if (CardBox == null || CardBox.GetPackages().Count == 0)
            {
                throw new InvalidOperationException("No box or no packages in the box to add.");
            }

            KittingBox boxToUse = CardBox;
            CardBox = null;
            return boxToUse;
        }


        /// <summary>
        /// Prepares a new box by clearing the current box and reduces the available box count.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown when there are no new boxes available to prepare.</exception>
        public void PrepareNewBox()
        {
            if (totalBoxesAvailable < 1)
            {
                throw new InvalidOperationException("No new boxes available to prepare.");
            }

            packagesInBox.Clear();
            totalBoxesAvailable--;
        }

        /// <summary>
        /// Prints all kitting boxes and packages in a kitting box.
        /// </summary>
        public void PrintAllKittingBoxes()
        {
            if (!kittingBoxes.Any())
            {
                Console.WriteLine("There are no kitting boxes in the Kitting Area.");
                return;
            }

            Console.WriteLine("Listing all kitting boxes in the Kitting Area:");
            foreach (KittingBox box in kittingBoxes)
            {
                Console.WriteLine($"KittingBox: {box.Name}, Type: {box.Goods}, Height: {box.Height} cm, and Width; {box.Width} cm");
                foreach (Package package in box.GetPackages())
                {
                    Console.WriteLine($"  Package: {package.Name}, Type: {package.Goods}, Height: {package.Height} cm, and Width; {package.Width} cm");
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
        /// Gets or sets the KittingBox active in the Kitting Area.
        /// </summary>
        public KittingBox CardBox
        {
            get { return cardBox; }
            set { cardBox = value; }
        }

        /// <summary>
        /// Returns active KittingBox.
        /// </summary>
        /// <returns></returns>
        public KittingBox GetKittingBox()
        {
            return cardBox;
        }

        private KittingBox cardBox;

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