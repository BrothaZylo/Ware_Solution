using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ware
{
    internal interface IReceivingDepartment
    {
        void AddPackage(Package package);
        void SendFirstPackageToStorage(Storage storageConfiguration);
        public void SendAllPackagesToStorage(Storage storageConfiguration);
        void GetAllPackagePrint();
        List<Package> GetPackageList();
        List<Package> GetAllPackages();

    }
}
