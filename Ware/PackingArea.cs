using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ware;

namespace Ware
{
    public class PackingArea
    {
        private readonly List<Pallet> pallets = new List<Pallet>();
        private readonly List<Package> packagesInPackingArea = new List<Package>();
        private readonly List<Package> schedulePackagesForPackingArea = new List<Package>();

        /// <summary>
        /// Stores a package in the packing area until it can be added to a pallet.
        /// </summary>
        /// <param name="package">The package to be stored.</param>
        public void ReceivePackage(Package package)
        {
            packagesInPackingArea.Add(package);
        }

        /// <summary>
        /// Plans where the package is going to go in the furture
        /// </summary>
        /// <param name="package">Package object</param>
        public void SchedulePackage(Package package)
        {
            schedulePackagesForPackingArea.Add(package);
        }

        /// <summary>
        /// Adds a stored package to a specified pallet. Throws an exception if the pallet is full.
        /// </summary>
        /// <param name="package">The package to add.</param>
        /// <param name="pallet">The pallet to add the package to.</param>
        public void AddToPallet(Package package, Pallet pallet)
        {
            if (!pallet.IsPalletFull())
            {
                pallet.AddPackageToPallet(package);
                packagesInPackingArea.Remove(package);
                RaiseAddToPalletEvent(package);
            }
            else
            {
                throw new InvalidOperationException("Pallet is full, can't add more packages.");
            }
        }

        /// <summary>
        /// gets all the packages scheduled to go to PackingArea
        /// </summary>
        /// <returns>List of all scheduled packages</returns>
        public List<Package> GetScheduledPackagesForPackingArea()
        {
            return schedulePackagesForPackingArea;
        }

        /// <summary>
        /// Gets all the packages placed in PackingArea
        /// </summary>
        /// <returns>List of all the packages in PackingArea</returns>
        public List<Package> GetPackagesInPackingArea()
        {
            return packagesInPackingArea;
        }

        public IReadOnlyList<Pallet> Pallets
        {
            get { return pallets.AsReadOnly(); }
        }

        public event EventHandler<PackageEventArgs> PackageAddedToPallet;

        private void RaiseAddToPalletEvent(Package package)
        {
            PackageAddedToPallet?.Invoke(this, new PackageEventArgs(package));
        }
    }
}