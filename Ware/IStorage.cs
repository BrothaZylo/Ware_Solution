using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ware
{
    internal interface IStorage
    {
        void UnitSpecsPrint();

        void Build();

        void PlacePackage(Package package);

        Package? MovePackageById(string packageid);

        Package? MovePackage(Package package);

        void MovePackageToTerminal(Package package, Terminal terminal);

        void GetAllStorageInformationPrint();
        Dictionary<string, (Package?, string, double, double, bool)> GetAllStorageInformationAsDictionary();

        string GetStorageNameById(int shelfnumber);

        string FindPackageSectionById(string packageid);

        string FindPackageById(string packageid);

        bool IsSpotTaken(string storagename);

        bool IsSameTypeOfGoods(Package package);

        void AddUnit(string sizeName, int totalUntsAvailable, double maxHeightCm, double maxWidthCm);
    }
}
