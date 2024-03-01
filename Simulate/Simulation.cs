// See https://aka.ms/new-console-template for more information

using Ware;


Storage Refridgerated = new("Refridgerated");
Refridgerated.AddUnit("Big", 15, 100, 100);

Package package1 = new("Milk", "Refridgerated", 10, 30);
Package package2 = new("Eggs", "Refridgerated", 10, 30);
Package package3 = new("Fruit Juice", "Refridgerated", 10, 30);


/**************************************************************/

// Configuring the size of the electronic units
Storage Electronics = new("Electronics");
Electronics.AddUnit("Big", 15, 100, 100);


Package package4 = new("Speakers", "Electronics", 10, 30);
Package package5 = new("Camera", "Electronics", 10, 30);
Package package6 = new("Laptop", "Electronics", 10, 30);


/**************************************************************/

// Configuring the size of the Dangerouos goods units
Storage Dangerous = new("Dangerous");
Dangerous.AddUnit("Big", 15, 100, 100);

Package package7 = new("Propane tank", "Dangerous", 3, 5);
Package package8 = new("Firework", "Dangerous", 3, 5);
Package package9 = new("You", "Dangerous", 3, 5);


/**************************************************************/

// Configuring the size of the dry goods units


Storage DryGoods = new("Dry");
DryGoods.AddUnit("Big", 15, 100, 100);


Package package10 = new("1 tank", "Dry", 3, 5);
Package package11 = new("2 tank", "Dry", 3, 5);
Package package12 = new("3 tank", "Dry", 3, 5);
Package package13 = new("4 tank", "Dry", 3, 5);
Package package14 = new("5 tank", "Dry", 3, 5);
Package package15 = new("6 tank", "Dry", 3, 5);
Package package16 = new("7 tank", "Dry", 3, 5);
Package package17 = new("8 tank", "Dry", 3, 5);
Package package18 = new("8 tank", "Dry", 3, 5);



/**************************************************************/

ReceivingDepartment receivingDepartmentForRefrigiated = new ReceivingDepartment(Refridgerated);
ReceivingDepartment receivingDepartmentForElectronics = new ReceivingDepartment(Electronics);
ReceivingDepartment receivingDepartmentForDangerousGoods = new ReceivingDepartment(Dangerous);
ReceivingDepartment receivingDepartmentForDryGoods = new ReceivingDepartment(DryGoods);

Schedule deliverySchedule = new Schedule();
/*
deliverySchedule.AddPackageToDay("Single", DayOfWeek.Monday, package1, DateTime.Now.AddHours(1), DateTime.Now.AddHours(16));
deliverySchedule.AddPackageToDay("Single", DayOfWeek.Monday, package2, DateTime.Now.AddHours(1), DateTime.Now.AddHours(16));
deliverySchedule.AddPackageToDay("Single", DayOfWeek.Monday, package3, DateTime.Now.AddHours(1), DateTime.Now.AddHours(16));
deliverySchedule.AddPackageToDay("Single", DayOfWeek.Tuesday, package4, DateTime.Now.AddHours(5), DateTime.Now.AddHours(20));
deliverySchedule.AddPackageToDay("Single", DayOfWeek.Tuesday, package5, DateTime.Now.AddHours(5), DateTime.Now.AddHours(20));
deliverySchedule.AddPackageToDay("Single", DayOfWeek.Tuesday, package6, DateTime.Now.AddHours(5), DateTime.Now.AddHours(20));
deliverySchedule.AddPackageToDay("Single", DayOfWeek.Wednesday, package7, DateTime.Now.AddHours(9), DateTime.Now.AddHours(24));
deliverySchedule.AddPackageToDay("Single", DayOfWeek.Wednesday, package8, DateTime.Now.AddHours(9), DateTime.Now.AddHours(24));
deliverySchedule.AddPackageToDay("Single", DayOfWeek.Wednesday, package9, DateTime.Now.AddHours(9), DateTime.Now.AddHours(24));
*/

// To add packages you have to create new package objects and then add it to the schedule
// Hvis du bruker repeating blir det jordskjelv, så hold unna :)

deliverySchedule.AddPackage("Single", DayOfWeek.Monday, package10, DateTime.Now.AddHours(120), DateTime.Now.AddHours(340));
deliverySchedule.AddPackage("Single", DayOfWeek.Tuesday, package11, DateTime.Now.AddHours(120), DateTime.Now.AddHours(340));
deliverySchedule.AddPackage("Single", DayOfWeek.Wednesday, package12, DateTime.Now.AddHours(120), DateTime.Now.AddHours(340));
deliverySchedule.AddPackage("Single", DayOfWeek.Thursday, package13, DateTime.Now.AddHours(120), DateTime.Now.AddHours(340));
deliverySchedule.AddPackage("Single", DayOfWeek.Thursday, package14, DateTime.Now.AddHours(120), DateTime.Now.AddHours(340));
deliverySchedule.AddPackage("Single", DayOfWeek.Friday, package15, DateTime.Now.AddHours(120), DateTime.Now.AddHours(340));
deliverySchedule.AddPackage("Single", DayOfWeek.Saturday, package16, DateTime.Now.AddHours(120), DateTime.Now.AddHours(340));
deliverySchedule.AddPackage("Repeating", DayOfWeek.Sunday, package17, DateTime.Now.AddHours(120), DateTime.Now.AddHours(340));




DryGoods.Build();

Terminal terminal = new Terminal();
PackageLogging packageHistory = new PackageLogging();

DateTime startDate = DateTime.Today;
DateTime endDate = DateTime.Today.AddDays(7); // maks 7 dager :)

int counter = 0;

while (startDate < endDate)
{
    deliverySchedule.GetSchedule();
    if (counter == 6)
    {
        deliverySchedule.ClearSchedule();
    }
    if(deliverySchedule.CheckDay(startDate.DayOfWeek))
    {
        Console.WriteLine($"Packages at : {startDate.ToString("dd-MM-yyyy")}");
        // Packages that arrive
        var packages = deliverySchedule.GetPackageDay(startDate.DayOfWeek);
        foreach(var package in packages)
        {
            receivingDepartmentForDryGoods.AddPackage(package.Item2);
            packageHistory.AddPackageLog(package.Item2.PackageId, "Reciving department" );    
        }
        Thread.Sleep(1000);
        TimeSpan delay = new TimeSpan(0, 0, 1);
        Thread.Sleep(delay);
        // Store the packages

        var packagesInReciving = receivingDepartmentForDryGoods.GetPackageList();
        foreach (var package in packagesInReciving)
        {
            packageHistory.AddPackageLog(package.PackageId, "Storage/Shelf");
        }
        receivingDepartmentForDryGoods.SendAllPackagesToStorage();
        Thread.Sleep(1000);
        Thread.Sleep(delay);


        // Before delivery
        List<Package> packageList = receivingDepartmentForDryGoods.GetAllPackages();

        foreach (var package in DryGoods.GetAllStorageInformationAsDictionary().Values)
        {
            foreach(Package packagee in packageList)
            {
                if (packagee.PackageId == package.Item1)
                {
                    terminal.AddPackage(packagee);
                    packageHistory.AddPackageLog(package.Item1, "Before delivery/Terminal");
                    Console.WriteLine();

                    DryGoods.MovePackage(packagee);

                }
            }
        }
        Thread.Sleep(1000);
        Thread.Sleep(delay);

        // Giving packages to delviery
        List<Package> packagesInTerminal = terminal.GetPackagesInTerminal();
        foreach (var package in packagesInTerminal)
        {
            packageHistory.AddPackageLog(package.PackageId, "Sent out / Delivered");

        }
        terminal.ClearPackages();
        Thread.Sleep(1000);
        Thread.Sleep(delay);
    }
    startDate = startDate.AddDays(1);
    counter++;
    
}
Console.WriteLine(packageHistory.TrackPackage(package10.PackageId));
packageHistory.LogsPrint();
Console.WriteLine("Simulation ended");


/*

//
List<Storage.WareHouseTimeConfig> configtime =
[
    new() { TimeDeliveryToStorageMinutes = 2, TimeStorageToTerminalMinutes = 5 }
];
//



List<Storage.WareHouseSizeConfig> configlistrefig =
[
    new() { SizeName = "Tiny", TotalUnitsAvailable = 5, MaxHeightCm = 10.5, MaxWidthCm = 10 },
    new() { SizeName = "Large", TotalUnitsAvailable = 4, MaxHeightCm = 30, MaxWidthCm = 30 }
];
Storage Refridgerated = new("Refridgerated", 25, configlistrefig, configtime);
Package package4 = new("Dragon", "Refridgerated", "fast", 2, 10);
Package package5 = new("Dinosaur", "Refridgerated", "fast", 7, 3);
Package package1 = new("Potato", "Refridgerated", "fast", 20, 10);
//




List<Storage.WareHouseSizeConfig> configlistelec =
[
    new() { SizeName = "Tiny", TotalUnitsAvailable = 5, MaxHeightCm = 10.5, MaxWidthCm = 10 },
    new() { SizeName = "Large", TotalUnitsAvailable = 4, MaxHeightCm = 30, MaxWidthCm = 30 }
];
Storage Electronics = new("Electronics", 25, configlistelec, configtime);
Package package2 = new("Speakers", "Electronics", "fast", 10, 30);
//




List<Storage.WareHouseSizeConfig> configlistdang =
[
    new() { SizeName = "Tiny", TotalUnitsAvailable = 5, MaxHeightCm = 10.5, MaxWidthCm = 10 },
    new() { SizeName = "Large", TotalUnitsAvailable = 4, MaxHeightCm = 30, MaxWidthCm = 30 }
];
Storage Dangerous = new("Dangerous", 25, configlistdang, configtime);
Package package3 = new("Propane tank", "Dangerous", "fast", 3, 5);
//




List<Storage.WareHouseSizeConfig> configlistdry =
[
    new() { SizeName = "Tiny", TotalUnitsAvailable = 5, MaxHeightCm = 10.5, MaxWidthCm = 10 },
    new() { SizeName = "Large", TotalUnitsAvailable = 4, MaxHeightCm = 30, MaxWidthCm = 30 }
];
Storage Dry = new("Dry goods", 25, configlistdry, configtime);
Package package6 = new("Chips", "Dry goods", "fast", 15, 3);
Package package7 = new("Ost", "Dry goods", "fast", 7, 3);
Package package8 = new("Moose", "Dry goods", "fast", 7, 3);
//


Terminal terminal = new Terminal();
Schedule deliverySchedule = new();
ReceivingDepartment receivingDepartment = new(Dry);
Dry.Build();

int timer = 0;

while (timer!=60)
{
    if (timer == 0)
    {
        deliverySchedule.AddPackageToDay("Single", DayOfWeek.Monday, package6, DateTime.Now, DateTime.Now.AddMinutes(10));
        deliverySchedule.AddPackageToDay("Single", DayOfWeek.Monday, package7, DateTime.Now, DateTime.Now.AddMinutes(30));
        deliverySchedule.AddPackageToDay("Repeating", DayOfWeek.Tuesday, package8, DateTime.Now, DateTime.Now.AddMinutes(40));
        deliverySchedule.GetCalender();
        receivingDepartment.ReceivePackage(package6);
        receivingDepartment.ReceivePackage(package7);
        receivingDepartment.ReceivePackage(package8);
        Console.WriteLine("");
    }

    if (timer == 3)
    {
        receivingDepartment.SendFirstPackageToStorage();
        receivingDepartment.SendFirstPackageToStorage();
        receivingDepartment.SendFirstPackageToStorage();
        Console.WriteLine();
        Dry.GetAllStorageInformationPrint();
    }


    Console.WriteLine();
    terminal.AddPackage(Dry.MovePackage(package6));
    terminal.AddPackage(Dry.MovePackage(package7));
    Console.WriteLine();
    terminal.GivingPackagesToDriver();

    Console.WriteLine();
    Console.WriteLine(package6.packageId + " Was moved in " + Dry.GetTimeDeliveryToStorage() + " Minutes");
    Console.WriteLine(package7.packageId + " Was moved in " + Dry.GetTimeDeliveryToStorage() + " Minutes");
    Console.WriteLine("");

    Dry.GetAllStorageInformationPrint();
    Console.WriteLine();

    Console.WriteLine("\nNext weeks Schedule:");
    deliverySchedule.ClearSchedule();
    deliverySchedule.GetCalender();

    Thread.Sleep(1000);
    TimeSpan delay = new TimeSpan(0, 0, 1);
    Console.WriteLine("Timer "+timer);
    Thread.Sleep(delay);
    timer++;
}
Console.WriteLine();
PackageHistory packageHistory = new PackageHistory(new Dictionary<Package, (DateTime DeliveryTime, DateTime PickupTime)>());

packageHistory.DeliveryHistory(package1, DateTime.Now);
packageHistory.DeliveryHistory(package2, DateTime.Now);
packageHistory.DeliveryHistory(package3, DateTime.Now);
packageHistory.DeliveryHistory(package6, DateTime.Now);

packageHistory.PickTime(package1, DateTime.Now.AddHours(2));
packageHistory.PickTime(package2, DateTime.Now.AddHours(48));
packageHistory.PickTime(package3, DateTime.Now.AddHours(24));
packageHistory.PickTime(package6, DateTime.Now.AddHours(8));


packageHistory.AllHistoryInfo();

packageHistory.OnePackageHistory(package1.packageId);
*/
