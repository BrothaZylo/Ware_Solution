// See https://aka.ms/new-console-template for more information
using System.Xml.Linq;
using Ware;
using static Ware.DeliverySchedule;

CreatePackage u = new("Hestesko", "kjølevare", "fast", 82.5, 43.4);
CreatePackage uu = new("Pæreboks", "kulvare", "treg", 91.3, 15.7);
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

PackageHistory packageHistory = new PackageHistory(new Dictionary<CreatePackage, (DateTime DeliveryTime, DateTime PickupTime)>());

CreatePackage iskrem = new CreatePackage("Iskrem", "kjølevare", "fast",43,4);
CreatePackage flammekaster = new CreatePackage("Flammekaster", "Farlig gods", "fast", 89, 60);
// Test for å legge til enkelte pakker

packageHistory.DeliveryHistory(iskrem, DateTime.Now);
packageHistory.DeliveryHistory(flammekaster, DateTime.Now);
packageHistory.PickTime(iskrem, DateTime.Now.AddHours(10));


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
packageHistory.OnePackageHistory(iskrem.packageid);


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

DeliverySchedule deliverySchedule = new DeliverySchedule();

CreatePackage xd = CreatePackage.SendToWareHouse(u);
Console.WriteLine(xd.name+" "+xd.packageid);
List<CreatePackage> e = [u, uu];
Console.WriteLine(e);
for(int i = 0; i < e.Count; i++)
{
    Console.WriteLine(e[i].name);
}
