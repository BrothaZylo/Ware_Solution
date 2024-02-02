// See https://aka.ms/new-console-template for more information
using Ware;
using static Ware.DeliverySchedule;

CreatePackage u = new("Hestesko", "kjølevare", "fast", 82.5, 43.4);

Console.WriteLine(u.name);
Console.WriteLine(u.packageid);

PackageHistory packageHistory = new PackageHistory(new Dictionary<CreatePackage, (DateTime DeliveryTime, DateTime PickupTime)>());

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


//Hente infoen om en enkelt vare
packageHistory.OnePackageHistory(iskrem.packageid);

*/



List<DeliverySchedule.DeliveryList> packageDates =
[
    new() { Day = "Monday", Packages = iskrem, DeliveryTime = DateTime.Now, DeliveryType = 1},
    new() { Day = "Tuesday", Packages = flammekaster, DeliveryTime = DateTime.Now, DeliveryType = 1 }


];

DeliverySchedule schedule = new DeliverySchedule(packageDates);

schedule.CreateSchedule();
schedule.SchedulePrint();
