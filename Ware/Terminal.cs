using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Ware
{
    /// <summary>
    /// This is where the packages will leave the warehouse
    /// </summary>
    public class Terminal : ITerminal
    {
        private readonly List<Package> PackagesToSendOut = new List<Package>();
        private readonly Queue<Package> PackagesToSendOutQueue = new Queue<Package>();
        private readonly List<Pallet> PalletsInTerminal = new List<Pallet>();

        

        /// <summary>
        /// This will add a package to a dictionary which are the packages at the terminal
        /// </summary>
        /// <param name="packages">A package object</param>
        public void AddPackage(Package packages)
        {
            if (PackagesToSendOut.Contains(packages))
            {
                throw new PackageInvalidException("Package already excist in terminal");
            }
            PackagesToSendOut.Add(packages);

            RaisePackageAddEvent(packages);
        }

        /// <summary>
        /// Adds a pallet to the terminal.
        /// </summary>
        /// <param name="pallet">The pallet to add to the terminal.</param>
        public void AddPallet(Pallet pallet)
        {
            List<Package> packages = pallet.GetPackagesOnPallet();
            PalletsInTerminal.Add(pallet);
            RaisePalletAddEvent(pallet);
        }

        /// <summary>
        /// Returns a dictionary of packages in the terminal
        /// </summary>
        /// <returns>Returns a dictionary of packages in the terminal</returns>
        public List<Package> GetPackagesInTerminal()
        {
            return PackagesToSendOut;
        }

        /// <summary>
        /// Prints out the name of all thee package objects in the terminal
        /// </summary>
        public void PrintPackageList()
        {
            foreach (Package p in PackagesToSendOut)
            {
                Console.WriteLine(p.Name);
            }
        }

        /// <summary>
        /// Prints all information of the pallets in the terminal.
        /// </summary>
        public void PrintPalletInformation()
        {
            foreach (var pallet in PalletsInTerminal)
            {
                int packageCount = pallet.PackagesInPallet();
                Console.WriteLine($"Pallet with {packageCount} packages.");
                foreach (var package in pallet.GetPackagesOnPallet())
                {
                    Console.WriteLine($"Package: {package.Name}");
                }
            }
        }

        /// <summary>
        /// Sends out a specific package and removes from dictionary
        /// </summary>
        /// <param name="package">A package object</param>
        /// <returns>A package if the package was found, else null</returns>
        public Package? SendPackage(Package package)
        {

            int numberOfPackages = PackagesToSendOut.Count;

            foreach (Package? p in PackagesToSendOut)
            {
                if (PackagesToSendOut.Contains(package))
                {
                    PackagesToSendOut.Remove(p);
                    RaisePackageSendEvent(package);
                    return p;
                }
            }
            int newCount = PackagesToSendOut.Count;
            if (newCount == numberOfPackages)
            {
                throw new PackageInvalidException("Package does not exist in terminal." );
            
            }
            return null;
        }

        /// <summary>
        /// Sends all the packages out
        /// </summary>
        public void SendAllPackages()
        {
            AddToQueue();
            while(PackagesToSendOutQueue.Count > 0)
            {
                Package package = PackagesToSendOutQueue.Dequeue();
                RaisePackageSendAllEvent(package);
            }
        }

        /// <summary>
        /// Clears all the packages in terminal
        /// </summary>
        public void ClearPackages()
        {
            PackagesToSendOut.Clear();
        }

        /// <summary>
        /// 
        /// </summary>
        private void AddToQueue()
        {
            foreach (Package p in PackagesToSendOut)
            {
                PackagesToSendOutQueue.Enqueue(p);
            }
            PackagesToSendOut.Clear();
        }

        /// <summary>
        /// Uses by SendAllPackages()
        /// </summary>
        public event EventHandler<PackageEventArgs>? PackageSendAllEvent;

        /// <summary>
        /// Used by AddPackage(Package packages)
        /// </summary>
        public event EventHandler<PackageEventArgs>? PackageAddEvent;

        /// <summary>
        /// SendPackage(Package package)
        /// </summary>
        public event EventHandler<PackageEventArgs>? PackageSendEvent;

        public event EventHandler<PalletEventArgs>? PalletAddEvent;

        private void RaisePalletAddEvent(Pallet pallet)
        {
            PalletAddEvent?.Invoke(this, new PalletEventArgs(pallet));
        }
        private void RaisePackageSendAllEvent(Package package)
        {
            PackageSendAllEvent?.Invoke(this, new PackageEventArgs(package));
        }
        private void RaisePackageAddEvent(Package package)
        {
            PackageAddEvent?.Invoke(this, new PackageEventArgs(package));
        }
        private void RaisePackageSendEvent(Package package)
        {
            PackageSendEvent?.Invoke(this, new PackageEventArgs(package));
        }
    }
}