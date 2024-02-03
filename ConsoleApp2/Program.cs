﻿// See https://aka.ms/new-console-template for more information
using Ware;
using static Ware.DeliverySchedule;

CreatePackage u = new("Hestesko", "kjølevare", "fast", 82.5, 43.4);

//Console.WriteLine(u.name);
//Console.WriteLine(u.packageid);

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
DeliverySchedule deliverySchedule = new DeliverySchedule();

deliverySchedule.AddPackageToDay("Single",DayOfWeek.Monday, flammekaster, DateTime.Now, DateTime.Now.AddMinutes(10));
deliverySchedule.AddPackageToDay("Single", DayOfWeek.Monday, iskrem, DateTime.Now, DateTime.Now.AddMinutes(130));
deliverySchedule.AddPackageToDay("Single", DayOfWeek.Monday, u, DateTime.Now, DateTime.Now.AddMinutes(17));
deliverySchedule.AddPackageToDay("Repeating", DayOfWeek.Tuesday, u, DateTime.Now, DateTime.Now.AddMinutes(40));
deliverySchedule.AddPackageToDay("Single", DayOfWeek.Tuesday, u, DateTime.Now, DateTime.Now.AddMinutes(40));

deliverySchedule.GetCalender();

//Console.WriteLine(deliverySchedule.GetCalender());

deliverySchedule.ClearSchedule();