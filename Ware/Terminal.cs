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
        public List<CreatePackage> PackagesSentOut = new List<CreatePackage>();
        /// <summary>
        /// This will add a package to a list which are the packages at the terminal
        /// </summary>
        /// <param name="packages">A package object</param>
        public void AddPackage(CreatePackage packages)
        {
            PackagesSentOut.Add(packages);
        }
        /// <summary>
        /// Returns a list of packages in the terminal
        /// </summary>
        /// <returns>Returns a list of packages in the terminal</returns>
        public List<CreatePackage> GetPackagesInTerminal()
        {
            return PackagesSentOut;
        }
        /// <summary>
        /// Sends out a specific package and removes from list
        /// </summary>
        /// <param name="package">A package object</param>
        public void SendPakcage(CreatePackage package)
        {
            foreach (CreatePackage p in PackagesSentOut)
            {
                if (PackagesSentOut.Contains(package))
                {
                    PackagesSentOut.Remove(p);
                }
            }
        }


        /// <summary>
        /// Clears all the packages in terminal
        /// </summary>
        public void ClearPackages()
        {
            PackagesSentOut.Clear();
        }
    }
}