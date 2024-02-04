using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ware
{
    internal interface IStorageConfiguration
    {
        void WareHouseConfigPrint();

        void CreateStorage();

        string PlacePackage(CreatePackage package);

        string MovePackageById(string packageid);

        CreatePackage MovePackage(CreatePackage package);

        void GetAllStorageInformationPrint();

        string GetStorageNameById(int shelfnumber);

        string FindPackageSectionById(string packageid);

        string FindPackageById(string packageid);

        bool IsSpotTaken(string storagename);

        bool IsSameTypeOfGoods(CreatePackage package);

        int GetTimeDeliveryToStorage();

        int GetTimeStorageToTerminal();

        int GetTimeDeliveryToStorageSeconds();

        int GetTimeStorageToTerminalSeconds();
    }
}
