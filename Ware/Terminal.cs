using System;
using System.Collections.Generic;
using System.Linq;
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
        /// <summary>
        /// This will add a package to a list which are the packages at the terminal
        /// </summary>
        /// <param packageName="packages">A package object</param>
        public void AddPackage(Package packages)
        {
            PackagesToSendOut.Add(packages);
        }
        /// <summary>
        /// Returns a list of packages in the terminal
        /// </summary>
        /// <returns>Returns a list of packages in the terminal</returns>
        public List<Package> GetPackagesInTerminal()
        {
            return PackagesToSendOut;
        }
        /// <summary>
        /// Prints out the packageName of all thee package objects in the terminal
        /// </summary>
        public void PrintPackageList()
        {
            foreach (Package p in PackagesToSendOut)
            {
                Console.WriteLine(p.Name);
            }
        }
        /// <summary>
        /// Sends out a specific package and removes from list
        /// </summary>
        /// <param packageName="package">A package object</param>
        public void SendPackage(Package package)
        {

            int numberOfPackages = PackagesToSendOut.Count;

            foreach (Package p in PackagesToSendOut)
            {
                if (PackagesToSendOut.Contains(package))
                {
                    PackagesToSendOut.Remove(p);
                    break;
                }
            }
            int newCount = PackagesToSendOut.Count;
            if (newCount == numberOfPackages)
            {
                throw new PackageInvalidException("Package does not exist in terminal." );
            
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