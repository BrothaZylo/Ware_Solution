using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ware
{
    internal interface IStorage
    {
        void SizeConfigPrint();

        void Build();

        string PlacePackage(Package package);

        string MovePackageById(string packageid);

        Package MovePackage(Package package);

        void GetAllStorageInformationPrint();

        string GetStorageNameById(int shelfnumber);

        string FindPackageSectionById(string packageid);

        string FindPackageById(string packageid);

        bool IsSpotTaken(string storagename);

        bool IsSameTypeOfGoods(Package package);

    }
}
