// See https://aka.ms/new-console-template for more information
using System.Reflection.Metadata.Ecma335;
using System.Xml.Linq;
using Ware;
using static Ware.Schedule;

Package u = new("Hestesko", "kjølevare", "fast", 82.5, 43.4);

Package uu = new("Pæreboks", "kulvare", "treg", 91.3, 15.7);

List<Storage.WareHouseSizeConfig> configlist =
[
    new() { sizeName = "Tiny", totalUnitsAvailable = 5, maxHeightCm = 10.5, maxWidthCm = 10},
    new() { sizeName = "Large", totalUnitsAvailable = 4, maxHeightCm = 30, maxWidthCm = 30 }
];
List<Storage.WareHouseTimeConfig> configtime =
[
    new() { TimeDeliveryToStorageMinutes = 2, TimeStorageToTerminalMinutes = 2 }
];



Package iskrem = new Package("Iskrem", "kjølevare", "fast",43,4);
Package flammekaster = new Package("Flammekaster", "Farlig gods", "fast", 89, 60);
// Test for å legge til enkelte pakker
/*
packageHistory.DeliveryHistory(iskrem, DateTime.Now);
packageHistory.DeliveryHistory(flammekaster, DateTime.Now);
packageHistory.PickTime(iskrem, DateTime.Now.AddHours(10));
*/

//  Test for å legge til en liste med varer
/*
List<Package> packages = new List<Package>();
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
Schedule deliverySchedule = new(new Dictionary<string, List<(string,Package, DateTime)>>());

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

Schedule deliverySchedule = new Schedule();


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

Schedule deliverySchedule = new DeliverySchedule(packageHistory);

Console.WriteLine(packageHistory.AddPackageLog(flammekaster.packageid,"Varehuset"));
Console.WriteLine(packageHistory.AddPackageLog(flammekaster.packageid, "Hylla"));
Console.WriteLine(packageHistory.AddPackageLog(flammekaster.packageid, "Truck"));
Console.WriteLine(packageHistory.AddPackageLog(iskrem.packageid, "Varehuset"));
Console.WriteLine(packageHistory.AddPackageLog(iskrem.packageid, "Hylla"));
Console.WriteLine(packageHistory.AddPackageLog(iskrem.packageid, "Truck"));

packageHistory.LogsPrint();


List<Storage.WareHouseSizeConfig> configlistdry =
[
    new() { sizeName = "Tiny", totalUnitsAvailable = 5, maxHeightCm = 10.5, maxWidthCm = 10 },
    new() { sizeName = "Large", totalUnitsAvailable = 4, maxHeightCm = 30, maxWidthCm = 30 }
];
Storage Dry = new("Dry goods", 25, configlistdry, configtime);
Package package6 = new("Chips", "Dry goods", "fast", 15, 3);
Package package7 = new("Ost", "Dry goods", "fast", 7, 3);
Package package8 = new("Moose", "Dry goods", "fast", 7, 3);
Dry.CreateStorage();
Dry.PlacePackage(package8);
Dry.GetAllStorageInformationPrint();
//
Console.WriteLine(Dry.FindPackageById(package8.PackageId));
