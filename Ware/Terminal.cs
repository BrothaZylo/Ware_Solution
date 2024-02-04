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
            Console.WriteLine(packages.PackageId + " was moved to the Terminal");
            PackagesSentOut.Add(packages);
        }
        public void GivingPackagesToDriver()
        {
            foreach (CreatePackage package in PackagesSentOut)
            {
                Console.WriteLine($"{package.PackageId}, {package.Name} was transfered to the truckdriver");
            }
            PackagesSentOut.Clear();
        }
    }
}