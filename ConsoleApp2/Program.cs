// See https://aka.ms/new-console-template for more information
using Ware;

CreatePackage u = new("Hestesko", "Frysevarer", "fast", 3, 5);

List<StorageConfiguration.WareHouseSizeConfig> configlist =
[
    new() { Sizename = "Tiny", Totalunitsavailable = 5, Maxheightcm = 10.5, Maxwidthcm = 10},
    new() { Sizename = "Large", Totalunitsavailable = 4, Maxheightcm = 30, Maxwidthcm = 30 }
];
List<StorageConfiguration.WareHouseTimeConfig> configtime =
[
    new() { TimeDeliveryToStorageMinutes = 2, TimeStorageToTerminalMinutes = 2 }
];

StorageConfiguration house = new("Frysevarer", 25, configlist, configtime);

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
Console.WriteLine(house.GetTimeStorageToTerminalSeconds());