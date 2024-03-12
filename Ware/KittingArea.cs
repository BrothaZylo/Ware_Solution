using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ware
{
    /// <summary>
    /// An area where packages are packed together into cardboard boxes.
    /// </summary>
    public class KittingArea : IKittingArea
    {
        private List<Package> packagesInBox = new List<Package>();
        private int totalBoxesAvailable;
        private int maxPackagesPerBox;

        /// <summary>
        /// Uses new instance of KittingArea class.
        /// </summary>
        /// <param name="initialBoxCount">Total cardboard boxes available.</param>
        /// <param name="maxPackagesPerBox">The total number of packages that can be placed in a box.</param>
        public KittingArea(int initialBoxCount = 20, int maxPackagesPerBox = 10)
        {
            totalBoxesAvailable = initialBoxCount;
            maxPackagesPerBox = maxPackagesPerBox;
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

            packagesInBox.Add(package);
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
        /// Checks if the current box is full.
        /// </summary>
        /// <returns>true if the current box is full, otherwise false.</returns>
        public bool IsBoxFull()
        {
            return packagesInBox.Count > maxPackagesPerBox - 1;
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
        /// Gets the number of boxes remaining.
        /// </summary>
        /// <returns>The number of remaining boxes.</returns>
        public int BoxesRemaining()
        {
            return totalBoxesAvailable;
        }
    }
}