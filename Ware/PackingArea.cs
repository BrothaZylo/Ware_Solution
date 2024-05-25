using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Ware;

namespace Ware
{
    /// <summary>
    /// Stores the packages, and then used to place packages on pallets. 
    /// </summary>
    public class PackingArea
    {
        private readonly List<Pallet> pallets = new List<Pallet>();
        private readonly List<Package> packagesInPackingArea = new List<Package>();
        private int maxPackagesPerPallet = 30;
        private string areaName;

        /// <summary>
        /// Gets a list of pallets as ReadOnly currently managed by the packing area.
        /// </summary>
        public IReadOnlyList<Pallet> Pallets
        {
            get { return pallets.AsReadOnly(); }
        }

        /// <summary>
        /// The name of PackingArea.
        /// </summary>
        /// <param name="name">Name given to PackingArea.</param>
        public PackingArea(string name)
        { 
            areaName = name;
        }

        /// <summary>
        /// Sends a package to PackingArea.
        /// </summary>
        /// <param name="package">The package to be stored.</param>
        public void SendPackageToPackingArea(Package package)
        {
            packagesInPackingArea.Add(package);
        }

        /// <summary>
        /// Adds a stored package to a specified pallet. Throws an exception if the pallet is full.
        /// </summary>
        /// <param name="package">The package to add.</param>
        /// <param name="pallet">The pallet to add the package to.</param>
        public void AddPackageOnPallet(Package package, Pallet pallet)
        {
            if (pallet.PackagesOnPallet.Count >= maxPackagesPerPallet)
            {
                throw new InvalidOperationException("Pallet is full, can't add more packages.");
            }
            pallet.AddPackage(package);
            packagesInPackingArea.Remove(package);
            RaiseAddOnPalletEvent(package);
        }

        /// <summary>
        /// Allows to change the  maximum number of packages per pallet.
        /// </summary>
        /// <param name="maxPackages">The maximum number of packages allowed on the pallet.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the number of maxPackages is</exception>
        public void SetMaxPackagesPerPallet(int maxPackages)
        {
            if (maxPackages < 1)
            {
                throw new ArgumentOutOfRangeException("The number of packages per pallet must be greater than 0.");
            }
            maxPackagesPerPallet = maxPackages;
        }

        /// <summary>
        /// Prints the information of each package on the pallet.
        /// </summary>
        /// <param name="pallet">The pallet being checked to see which packages are on it.</param>
        public void PrintPalletInformation(Pallet pallet)
        {
            Console.WriteLine($"Pallet with {pallet.PackagesOnPallet.Count} packages:");
            foreach (Package package in pallet.PackagesOnPallet)
            {
                Console.WriteLine($"Package: {package.Name}");
            }
        }

        /// <summary>
        /// Creates a pallet ready to place packages on. 
        /// </summary>
        /// <returns>Makes a new palllet.</returns>
        public Pallet CreateNewPallet()
        {
            Pallet newPallet = new Pallet();
            pallets.Add(newPallet);
            return newPallet;
        }

        /// <summary>
        /// The name of the PackingArea.
        /// </summary>
        public string AreaName
        {
            get { return areaName; }
            set { areaName = value; }
        }

        /// <summary>
        /// Used when a package is successfully added to a pallet.
        /// </summary>
        public event EventHandler<PackageEventArgs> PackageAddedOnPallet;

        /// <summary>
        /// Raises the PackageAddedOnPallet event with provided package details.
        /// </summary>
        /// <param name="package">The package that was added to a pallet.</param>
        private void RaiseAddOnPalletEvent(Package package)
        {
            PackageAddedOnPallet?.Invoke(this, new PackageEventArgs(package));
        }
    }
}