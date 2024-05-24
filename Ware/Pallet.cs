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
    public class Pallet : IPallet
    {
        private readonly List<Package> packagesOnPallet = new List<Package>();

        /// <summary>
        /// Gets a list of pallets as ReadOnly currently managed by the packing area.
        /// </summary>
        public IReadOnlyList<Package> PackagesOnPallet
        {
            get { return packagesOnPallet.AsReadOnly(); }
        }

        /// <summary>
        /// Gets a list of pallets currently managed by the packing area.
        /// </summary>
        public List<Package> GetPackagesOnPallet()
        {
            return packagesOnPallet;
        }

        /// <summary>
        /// Adds a package to the pallet.
        /// </summary>
        /// <param name="package">The package to be added.</param>
        public void AddPackage(Package package)
        {
            packagesOnPallet.Add(package);
        }

        /// <summary>
        /// Clears the list of packages on pallet.
        /// </summary>
        public void ClearPallet()
        {
            packagesOnPallet.Clear();
        }

        /// <summary>
        /// Used for AddPackageToPallet(Package package)
        /// </summary>
        public event EventHandler<PackageEventArgs>? AddPackageToPalletEvent;

        private void RaiseAddPackageToPalltEvent(Package package)
        {
            AddPackageToPalletEvent?.Invoke(this, new PackageEventArgs(package));
        }
    }
}
