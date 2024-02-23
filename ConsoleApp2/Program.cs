// See https://aka.ms/new-console-template for more information
using Ware;

Package u = new("Hestesko", "kjølevare", 82.5, 43.4);

Package uu = new("Pæreboks", "kulvare", 91.3, 15.7);

List<Storage.WareHouseSizeConfig> configlist =
[
    new() { sizeName = "Tiny", totalUnitsAvailable = 5, maxHeightCm = 10.5, maxWidthCm = 10},
    new() { sizeName = "Large", totalUnitsAvailable = 4, maxHeightCm = 30, maxWidthCm = 30 }
];


Package iskrem = new Package("Iskrem", "kjølevare",43,4);
Package flammekaster = new Package("Flammekaster", "Farlig gods", 89, 60);

PackageLogging packageHistory = new PackageLogging();

Schedule deliverySchedule = new Schedule();

Console.WriteLine(packageHistory.AddPackageLog(flammekaster.PackageId,"Varehuset"));
Console.WriteLine(packageHistory.AddPackageLog(flammekaster.PackageId, "Hylla"));
Console.WriteLine(packageHistory.AddPackageLog(flammekaster.PackageId, "Truck"));
Console.WriteLine(packageHistory.AddPackageLog(iskrem.PackageId, "Varehuset"));
Console.WriteLine(packageHistory.AddPackageLog(iskrem.PackageId, "Hylla"));
Console.WriteLine(packageHistory.AddPackageLog(iskrem.PackageId, "Truck"));

packageHistory.LogsPrint();


List<Storage.WareHouseSizeConfig> configlistdry =
[
    new() { sizeName = "Tiny", totalUnitsAvailable = 5, maxHeightCm = 10.5, maxWidthCm = 10 },
    new() { sizeName = "Large", totalUnitsAvailable = 4, maxHeightCm = 30, maxWidthCm = 30 }
];
Storage Dry = new("Dry goods", configlistdry);
Package package6 = new("Chips", "Dry goods", 15, 3);
Package package7 = new("Ost", "Dry goods", 7, 3);
Package package8 = new("Moose", "Dry goods", 7, 3);
Dry.Build();
Dry.PlacePackage(package8);
Dry.GetAllStorageInformationPrint();
//
Console.WriteLine(Dry.FindPackageById(package8.PackageId));
