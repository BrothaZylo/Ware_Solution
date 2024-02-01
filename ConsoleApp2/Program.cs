// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;
using Ware;

class Program
{
    static void Main(string[] args)
    {
        CreatePackage u = new CreatePackage("Hestesko", "Frysevarer", "fast", 3, 5);

        Console.WriteLine(u.name);
        Console.WriteLine(u.packageid);
        List<StorageConfiguration.WareHouseSizeConfig> configlist =
        [
            new StorageConfiguration.WareHouseSizeConfig { Sizename = "Tiny", Totalunitsavailable = 5, Maxheightcm = 10.5, Maxwidthcm = 10},
            new StorageConfiguration.WareHouseSizeConfig { Sizename = "Large", Totalunitsavailable = 4, Maxheightcm = 30, Maxwidthcm = 30 }
        ];

        StorageConfiguration house = new StorageConfiguration("Frysevarer", 25, configlist);

        house.WareHouseConfigPrint();
        house.CreateStorage();
        house.GetAllStorageInformationPrint();
        Console.WriteLine("");
        Console.WriteLine(house.PlacePackage(u));
        Console.WriteLine(house.PlacePackage(u));
        Console.WriteLine();
        Console.WriteLine(house.FindPackageById(u.packageid));
        Console.WriteLine();
        Console.WriteLine(house.FindPackageSectionById(u.packageid));
        Console.WriteLine();
        Console.WriteLine(house.IsSpotTaken("FrysevarerShelfID: 1"));
        Console.WriteLine(house.IsSpotTaken(house.GetStorageNameById(1)));
        Console.WriteLine(house.GetStorageNameById(1));
        Console.WriteLine(house.FindPackageById(u.packageid));
        Console.WriteLine(house.FindPackageSectionById(u.packageid));
        house.GetAllStorageInformationPrint();

        ReceivingDepartment receivingDept = new ReceivingDepartment(house);

        receivingDept.ReceivePackage(u);

        List<string> placementResults = receivingDept.SendPackagesToWarehouse();
        foreach (var result in placementResults)
        {
            Console.WriteLine(result);
        }
    }
}