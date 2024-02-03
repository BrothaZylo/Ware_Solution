using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ware
{ 
    public class Terminal
{
    public List<CreatePackage> PackagesSentOut = new List<CreatePackage>();

        public void AddPackage(CreatePackage packages)
        {
            Console.WriteLine(packages.packageid + " was moved to the Terminal");
            PackagesSentOut.Add(packages);
        }
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