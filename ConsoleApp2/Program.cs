// See https://aka.ms/new-console-template for more information
using System.Reflection.Metadata.Ecma335;
using System.Xml.Linq;
using Ware;
using static Ware.DeliverySchedule;

CreatePackage u = new("Hestesko", "kjølevare", "fast", 82.5, 43.4);

CreatePackage uu = new("Pæreboks", "kulvare", "treg", 91.3, 15.7);

List<StorageConfiguration.WareHouseSizeConfig> configlist =
[
    new() { SizeName = "Tiny", TotalUnitsAvailable = 5, MaxHeightCm = 10.5, MaxWidthCm = 10},
    new() { SizeName = "Large", TotalUnitsAvailable = 4, MaxHeightCm = 30, MaxWidthCm = 30 }
];
List<StorageConfiguration.WareHouseTimeConfig> configtime =
[
    new() { TimeDeliveryToStorageMinutes = 2, TimeStorageToTerminalMinutes = 2 }
];



CreatePackage iskrem = new CreatePackage("Iskrem", "kjølevare", "fast",43,4);
CreatePackage flammekaster = new CreatePackage("Flammekaster", "Farlig gods", "fast", 89, 60);
// Test for å legge til enkelte pakker
/*
packageHistory.DeliveryHistory(iskrem, DateTime.Now);
packageHistory.DeliveryHistory(flammekaster, DateTime.Now);
packageHistory.PickTime(iskrem, DateTime.Now.AddHours(10));
*/

//  Test for å legge til en liste med varer
/*
List<CreatePackage> packages = new List<CreatePackage>();
packages.Add(iskrem);
packages.Add(flammekaster);

packageHistory.SeveralDelivery(packages, DateTime.Now);
packageHistory.SeveralPickup(packages, DateTime.Now.AddDays(4));


//Hente infoen om alle varene 
packageHistory.AllHistoryInfo();
*/

//Hente infoen om en enkelt vare
//packageHistory.OnePackageHistory(iskrem.packageid);


/*
DeliverySchedule deliverySchedule = new(new Dictionary<string, List<(string,CreatePackage, DateTime)>>());

deliverySchedule.AddPackage("Monday",iskrem, DateTime.Now);
deliverySchedule.AddPackage("Monday", u, DateTime.Now);
deliverySchedule.AddPackage("Tuesday", flammekaster, DateTime.Now);
deliverySchedule.AddPackage("Tuesday", flammekaster, DateTime.Now);
deliverySchedule.AddPackage("Monday", u, DateTime.Now);
deliverySchedule.AddPackage("Tuesday", flammekaster, DateTime.Now);
deliverySchedule.AddPackage("Tuesday", flammekaster, DateTime.Now);
deliverySchedule.AddPackage("Tuesday", flammekaster, DateTime.Now);
deliverySchedule.AddPackage("Tuesday", flammekaster, DateTime.Now);




deliverySchedule.SchedulePackages("Monday");
deliverySchedule.SchedulePackages("Tuesday");
*/
/*

DeliverySchedule deliverySchedule = new DeliverySchedule();


deliverySchedule.AddPackageToDay("Single",DayOfWeek.Monday, flammekaster, DateTime.Now, DateTime.Now.AddMinutes(10));
deliverySchedule.AddPackageToDay("Single", DayOfWeek.Monday, iskrem, DateTime.Now, DateTime.Now.AddMinutes(130));
deliverySchedule.AddPackageToDay("Single", DayOfWeek.Monday, u, DateTime.Now, DateTime.Now.AddMinutes(17));
deliverySchedule.AddPackageToDay("Repeating", DayOfWeek.Tuesday, u, DateTime.Now, DateTime.Now.AddMinutes(40));
deliverySchedule.AddPackageToDay("Single", DayOfWeek.Tuesday, u, DateTime.Now, DateTime.Now.AddMinutes(40));

deliverySchedule.GetCalender();

//Console.WriteLine(deliverySchedule.GetCalender());

deliverySchedule.ClearSchedule();

*/
PackageLogging packageHistory = new PackageLogging();

DeliverySchedule deliverySchedule = new DeliverySchedule(packageHistory);

Console.WriteLine(packageHistory.AddPackageLog(flammekaster.packageid,"Varehuset"));
Console.WriteLine(packageHistory.AddPackageLog(flammekaster.packageid, "Hylla"));
Console.WriteLine(packageHistory.AddPackageLog(flammekaster.packageid, "Truck"));
Console.WriteLine(packageHistory.AddPackageLog(iskrem.packageid, "Varehuset"));
Console.WriteLine(packageHistory.AddPackageLog(iskrem.packageid, "Hylla"));
Console.WriteLine(packageHistory.AddPackageLog(iskrem.packageid, "Truck"));

packageHistory.LogsPrint();


List<StorageConfiguration.WareHouseSizeConfig> configlistdry =
[
    new() { SizeName = "Tiny", TotalUnitsAvailable = 5, MaxHeightCm = 10.5, MaxWidthCm = 10 },
    new() { SizeName = "Large", TotalUnitsAvailable = 4, MaxHeightCm = 30, MaxWidthCm = 30 }
];
StorageConfiguration Dry = new("Dry goods", 25, configlistdry, configtime);
CreatePackage package6 = new("Chips", "Dry goods", "fast", 15, 3);
CreatePackage package7 = new("Ost", "Dry goods", "fast", 7, 3);
CreatePackage package8 = new("Moose", "Dry goods", "fast", 7, 3);
Dry.CreateStorage();
Dry.PlacePackage(package8);
Dry.GetAllStorageInformationPrint();
//
Console.WriteLine(Dry.FindPackageById(package8.PackageId));
