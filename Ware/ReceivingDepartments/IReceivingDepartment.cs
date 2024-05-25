using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ware.Storages;
using Ware.Packages;

namespace Ware.ReceivingDepartments
{
    internal interface IReceivingDepartment
    {
        void AddPackage(Package package);

        void SendPackageToStorage(Package package, Storage storage, string shelfId);
        void SendPackageToStorage(Package package, Storage storage, string shelfId1, string shelfId2);
        void SendPackageToStorage(Package package, Storage storage, string shelfId1, string shelfId2, string shelfId3);
        void SendFirstPackageToStorage(Storage storageConfiguration);
        public void SendAllPackagesToStorage(Storage storageConfiguration);
        void GetAllPackagePrint();
        List<Package> GetPackageList();
        List<Package> GetAllPackages();

    }
}
