﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Ware.KittingAreas;
using Ware.Packages;
using Ware.Pallets;

namespace Ware.Terminals
{
    /// <summary>
    /// This is where the packages will leave the warehouse
    /// </summary>
    public class Terminal(string name) : ITerminal
    {
        private string name = name;
        private readonly List<Package> PackagesToSendOut = new List<Package>();
        private readonly Queue<Package> PackagesToSendOutQueue = new Queue<Package>();
        private readonly List<Pallet> PalletsInTerminal = new List<Pallet>();
        private readonly List<KittingBox> KittingBoxesInTerminal = new List<KittingBox>();

        /// <summary>
        /// This will add a package to a list which are the packages at the terminal.
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
        /// Adds a kitting box to the terminal
        /// </summary>
        /// <param name="kittingBox">The box that gets added to the terminal</param>
        public void AddKittingBox(KittingBox kittingBox)
        {
            if (KittingBoxesInTerminal.Contains(kittingBox))
            {
                throw new InvalidOperationException("KittingBox already exists in terminal.");
            }
            KittingBoxesInTerminal.Add(kittingBox);
        }

        /// <summary>
        /// Gets a list of packages in the terminal
        /// </summary>
        /// <returns>Returns a list of packages in the terminal</returns>
        public List<Package> GetPackagesInTerminal()
        {
            return PackagesToSendOut;
        }

        /// <summary>
        /// Gets the pallets in terminal
        /// </summary>
        /// <returns>Returns a list of all the pallets in terminal</returns>
        public List<Pallet> GetPalletsInTerminal()
        {
            return PalletsInTerminal;
        }

        /// <summary>
        /// Gets the kitting boxes in the terminal
        /// </summary>
        /// <returns>Returns a list of all kitting boxes in terminal</returns>
        public List<KittingBox> GetKittingBoxesInTerminal()
        {
            return KittingBoxesInTerminal;
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
        public void PrintPalletsInformation()
        {
            if (!PalletsInTerminal.Any())
            {
                Console.WriteLine($"There's no pallets in {name}.");
                return;
            }

            foreach (Pallet pallet in PalletsInTerminal)
            {
                int packageCount = pallet.PackagesOnPallet.Count;
                Console.WriteLine($"Pallet with {packageCount} packages:");
                foreach (Package package in pallet.GetPackagesOnPallet())
                {
                    Console.WriteLine($"Package: {package.Name}");
                }
            }
        }

        public void PrintKittingBoxesInformation()
        {
            if (!KittingBoxesInTerminal.Any())
            {
                Console.WriteLine($"There's no kitting boxes in {name}.");
                return;
            }

            foreach (KittingBox kittingBox in KittingBoxesInTerminal)
            {
                Console.WriteLine($"KittingBox: {kittingBox.Name} contains {kittingBox.GetPackages().Count} packages.");
                foreach (Package package in kittingBox.GetPackages())
                {
                    Console.WriteLine($" - Package: {package.Name}, Type: {package.Goods}, Height in cm: {package.Height}, Width in cm: {package.Width}");
                }
            }
        }


        /// <summary>
        /// Sends out a specific package and removes from list
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
                throw new PackageInvalidException("Package does not exist in terminal.");

            }
            return null;
        }

        /// <summary>
        /// Sends all the packages out
        /// </summary>
        public void SendAllPackages()
        {
            AddToQueue();
            while (PackagesToSendOutQueue.Count > 0)
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
        /// Sends out kitting box from terminal.
        /// </summary>
        /// <param name="kittingBox">the kitting box to send out.</param>
        public void SendOutKittingBox(KittingBox kittingBox)
        {
            if (!KittingBoxesInTerminal.Contains(kittingBox))
            {
                throw new InvalidOperationException("KittingBox not found in terminal.");
            }
            KittingBoxesInTerminal.Remove(kittingBox);
        }

        /// <summary>
        /// Sends out a pallet in the terminal clears it from the terminal.
        /// </summary>
        /// <param name="pallet">The pallet being sent out.</param>
        /// <exception cref="InvalidOperationException">Thrown when the pallet is not in the terminal.</exception>
        public void SendOutPallet(Pallet pallet)
        {
            if (PalletsInTerminal.Contains(pallet))
            {
                PalletsInTerminal.Remove(pallet);
            }
            else
            {
                throw new InvalidOperationException("The specified pallet is not in the terminal.");
            }
        }

        /// <summary>
        /// Sends out all the pallets in the terminal and clears them from the terminal.
        /// </summary>
        public void SendOutPallets()
        {
            foreach (Pallet pallet in PalletsInTerminal.ToList())
            {
                SendOutPallet(pallet);
            }
        }

        /// <summary>
        /// Name of the termial
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
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

        /// <summary>
        /// Used by AddPallet(Pallet pallet)
        /// </summary>
        public event EventHandler<PalletEventArgs>? PalletAddEvent;

        /// <summary>
        /// Transfers all packages from the send-out list to a queue and clears the list.
        /// </summary>
        private void AddToQueue()
        {
            foreach (Package p in PackagesToSendOut)
            {
                PackagesToSendOutQueue.Enqueue(p);
            }
            PackagesToSendOut.Clear();
        }

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