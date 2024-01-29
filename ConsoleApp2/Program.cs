// See https://aka.ms/new-console-template for more information
using Ware;

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
List<CreatePackage> packages = new List<CreatePackage>();
packages.Add(iskrem);
packages.Add(flammekaster);

packageHistory.SeveralDelivery(packages, DateTime.Now);
packageHistory.SeveralPickup(packages, DateTime.Now.AddDays(4));




var alllog = packageHistory.AllHistory();
foreach (var items in alllog)
{
    Console.WriteLine($"ID: {items.Key.packageid}   " +
        $"      Name: {items.Key.name}" +
        $"      Type: {items.Key.goods}" +
        $"      Speed: {items.Key.speed}" +
        $"      Height: {items.Key.height}" +
        $"      Time Arrived: {items.Value.DeliveryTime}" +
        $"      Time Sent out: {items.Value.PickupTime}");
}
/*
var onelog = packageHistory.OnePackageHistory(iskrem.packageid);
foreach (var items in onelog)
{ 
    Console.WriteLine($"{items.package.packageid}" +
        $"      Name: {items.package.name}" +
        $"      Type: {items.package.goods}" +
        $"      Speed: {items.package.speed}" +
        $"      Height: {items.package.height}" +
        $"      Time Arrived: {items.deliveryTime}" +
        $"      Time Sent out{items.pickupTime}");

}*/