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
    public class Terminal
{
    public List<CreatePackage> PackagesSentOut = new List<CreatePackage>();
        /// <summary>
        /// This will add a package to a list which are the packages at the terminal
        /// </summary>
        /// <param name="packages">A package object</param>
        public void AddPackage(CreatePackage packages)
        {
            Console.WriteLine(packages.packageid + " was moved to the Terminal");
            PackagesSentOut.Add(packages);
        }
        /// <summary>
        /// This will remove the packages from the warehouse
        /// </summary>
        public void GivingPackagesToDriver()
        {
            foreach (CreatePackage package in PackagesSentOut)
            {
                Console.WriteLine($"{package.packageid}, {package.name} was transfered to the truckdriver");
            }
            PackagesSentOut.Clear();
        }
    }
}