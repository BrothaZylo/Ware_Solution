using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Ware.Packages;

namespace Ware
{
    /// <summary>
    /// Here's where the packages are packed on the pallets before shipment.
    /// </summary>
    public class Pallet : IPallet
    {
        private readonly List<Package> packagesOnPallet = new List<Package>();
        private List<Package> schedulePackagesIn = new List<Package>();
        string palletId = GeneratePalletId();
        private string palletName;

        /// <summary>
        /// The name of the Pallet.
        /// </summary>
        /// <param name="name">Name given to the Pallet.</param>
        public Pallet(string name)
        {
            palletName = name;
        }

        /// <summary>
        /// Gets a list of pallets as ReadOnly currently managed by the packing area.
        /// </summary>
        public IReadOnlyList<Package> PackagesOnPallet
        {
            get { return packagesOnPallet.AsReadOnly(); }
        }

        /// <summary>
        /// Gets the pallet id
        /// </summary>
        /// <returns>palletid</returns>
        public string PalletId
        {
            get { return palletId; }
            set { palletId = value; }
        }

        /// <summary>
        /// Gets the pallet name.
        /// </summary>
        /// <returns>name</returns>
        public string PalletName
        {
            get { return palletName; }
            set { palletName = value; }
        }

        public void SchedulePackageToPack(Package package)
        {
            schedulePackagesIn.Add(package);
        }

        public List<Package> GetScheduledPackages()
        {
            return schedulePackagesIn;
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
        /// Generates an ID for the pallet.
        /// </summary>
        /// <returns>Returns the pallet id</returns>
        private static string GeneratePalletId()
        {
            string selection = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz123456789";
            char[] newid = new char[8];
            for (int i = 0; i < 8; i++)
            {
                Random rand = new();
                newid[i] = selection[rand.Next(selection.Length)];
            }
            string generatedid = new(newid);
            return generatedid;
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
