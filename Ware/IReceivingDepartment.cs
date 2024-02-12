using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ware
{
    internal interface IReceivingDepartment
    {
        void SendFirstPackageToStorage();
        public void SendAllPackagesToStorage();
        void printlistpackage();
        string TravelTimeToStorage();
        List<string> SendPackagesToWarehouse();
        List<CreatePackage> GetPackageList();
        List<CreatePackage> GetAllPackages();

    }
}
