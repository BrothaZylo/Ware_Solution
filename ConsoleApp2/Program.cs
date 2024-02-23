// See https://aka.ms/new-console-template for more information
using Ware;

Package u = new("Hestesko", "kjølevare", 82.5, 43.4);

Package uu = new("Pæreboks", "kulvare", 91.3, 15.7);



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



Storage Dry = new("Dry goods");
Dry.AddUnit("Small", 5, 11, 10);
Dry.AddUnit("Large", 4, 90, 90);
Dry.AddUnit("BIIG", 2, 670, 50);
Package package6 = new("Chips", "Dry goods", 15, 3);
Package package7 = new("Ost", "Dry goods", 7, 3);
Package package8 = new("Moose", "Dry goods", 7, 3);
Dry.Build();
Dry.PlacePackage(package8);
Dry.GetAllStorageInformationPrint();
//
Console.WriteLine(Dry.FindPackageById(package8.PackageId));
