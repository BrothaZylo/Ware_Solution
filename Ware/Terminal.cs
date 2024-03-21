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
        public List<Package> PackagesToSendOut = new List<Package>();
        public Queue<Package> PackagesToSendOutQueue = new Queue<Package>();


        // EVents
        public event EventHandler<PackageEventArgs> PackageAdded;
        public event EventHandler<PackageEventArgs> PackageSent;
        public event EventHandler AllPackagesSent;


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

            PackageAdded?.Invoke(packages, new PackageEventArgs(packages));
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
        /// Sends out a specific package and removes from dictionary
        /// </summary>
        /// <param name="package">A package object</param>
        public void SendPackage(Package package)
        {

            int numberOfPackages = PackagesToSendOut.Count;

            foreach (Package p in PackagesToSendOut)
            {
                if (PackagesToSendOut.Contains(package))
                {
                    PackagesToSendOut.Remove(p);
                    PackageSent?.Invoke(p, new PackageEventArgs(package));
                    break;
                }
            }
            int newCount = PackagesToSendOut.Count;
            if (newCount == numberOfPackages)
            {
                throw new PackageInvalidException("Package does not exist in terminal." );
            
            } 

        }
        private void AddToQueue()
        {
            foreach(Package p in PackagesToSendOut) 
            {
                PackagesToSendOutQueue.Enqueue(p);
            }
            PackagesToSendOut.Clear();
        }

        public void SendAllPackages()
        {
            AddToQueue();
            while(PackagesToSendOutQueue.Count > 0)
            {
                Package package = PackagesToSendOutQueue.Dequeue();
                PackageSent?.Invoke(package, new PackageEventArgs(package));
            }
        }


        /// <summary>
        /// Clears all the packages in terminal
        /// </summary>
        public void ClearPackages()
        {
            PackagesToSendOut.Clear();
        }
    }
}