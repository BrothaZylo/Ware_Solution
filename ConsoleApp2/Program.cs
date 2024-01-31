// See https://aka.ms/new-console-template for more information
using System.Xml.Linq;
using Ware;

CreatePackage u = new("Hestesko", "kjølevare", "fast", 13, 5);

Console.WriteLine(u.name);
Console.WriteLine(u.packageid);
List<StorageConfiguration.WareHouseSizeConfig> configlist =
[
    new() { Sizename = "Tiny", Totalunitsavailable = 5, Maxheightcm = 10.5, Maxwidthcm = 5},
    new() { Sizename = "Large", Totalunitsavailable = 4, Maxheightcm = 30, Maxwidthcm = 30 }
];

StorageConfiguration house = new("Frysevarer", 25, configlist);

house.WareHouseConfigPrint();
house.CreateStorage();
house.GetAllStorageInformationPrint();
Console.WriteLine("");
house.FindPackageSectionByIdPrint("EmptySlot: 1");
Console.WriteLine(house.FindPackageById("EmptySlot: 1"));
Console.WriteLine(house.PlacePackage(u));
Console.WriteLine(house.PlacePackage(u));