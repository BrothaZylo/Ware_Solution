using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ware
{
    /// <summary>
    /// Here's where the packages are packed on the pallets before shipment.
    /// </summary>
    public class Pallet(int maxPackages = 30) : IPallet
    {
        private readonly List<Package> packagesOnPallet = [];
        private int maxPackagesPerPallet = maxPackages;

        /// <summary>
        /// Used for AddPackageToPallet(Package package)
        /// </summary>
        public event EventHandler<PackageEventArgs>? AddPackageToPalletEvent;

        private void RaiseAddPackageToPalltEvent(Package package)
        {
            AddPackageToPalletEvent?.Invoke(this, new PackageEventArgs(package));
        }
        /// <summary>
        /// Adds a package to the pallet, if current one is full then it creats a new pallet.
        /// </summary>
        /// <param name="package">The package to be added.</param>
        public void AddPackageToPallet(Package package)
        {
            if (packagesOnPallet.Count + 1 > maxPackagesPerPallet)
            {
                ResetPallet();
            }
            RaiseAddPackageToPalltEvent(package);
            packagesOnPallet.Add(package);
        }

        /// <summary>
        /// Resets the pallet, clearing the current list of packages.
        /// </summary>
        private void ResetPallet()
        {
            packagesOnPallet.Clear();
        }

        /// <summary>
        /// Checks if current pallet is full.
        /// </summary>
        /// <returns>True if the pallet is full or false if not.</returns>
        public bool IsPalletFull()
        {
            return packagesOnPallet.Count > maxPackagesPerPallet - 1;
        }

        /// <summary>
        /// Allows to change the  maximum number of packages per pallet.
        /// </summary>
        /// <param name="maxPackages">The maximum number of packages allowed on the pallet.</param>
        public void SetMaxPackagesPerPallet(int maxPackages)
        {
            if (maxPackages < 1)
            {
                throw new NegativeNumberException("The number of packages per pallet must be greater than 0.");
            }

            maxPackagesPerPallet = maxPackages;
        }
    }
}
