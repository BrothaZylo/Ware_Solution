// See https://aka.ms/new-console-template for more information

using Ware;


// Configuring the time it will take to the storage
List<StorageConfiguration.WareHouseTimeConfig> configtime =
[
    new() { TimeDeliveryToStorageMinutes = 2, TimeStorageToTerminalMinutes = 5 }
];
/**************************************************************/

// Configuring the size of the refrigirated units
List<StorageConfiguration.WareHouseSizeConfig> configListRefridge =
[
    new() { SizeName = "Tiny", TotalUnitsAvailable = 5, MaxHeightCm = 10.5, MaxWidthCm = 10 },
    new() { SizeName = "Large", TotalUnitsAvailable = 4, MaxHeightCm = 30, MaxWidthCm = 30 }
];
StorageConfiguration Refridgerated = new("Refridgerated", 25, configListRefridge, configtime);

CreatePackage package1 = new("Milk", "Refridgerated", "fast", 10, 30);
CreatePackage package2 = new("Eggs", "Refridgerated", "fast", 10, 30);
CreatePackage package3 = new("Fruit Juice", "Refridgerated", "fast", 10, 30);


/**************************************************************/

// Configuring the size of the electronic units
List<StorageConfiguration.WareHouseSizeConfig> configListElectronics =
[
    new() { SizeName = "Tiny", TotalUnitsAvailable = 5, MaxHeightCm = 10.5, MaxWidthCm = 10 },
    new() { SizeName = "Large", TotalUnitsAvailable = 4, MaxHeightCm = 30, MaxWidthCm = 30 }
];
StorageConfiguration Electronics = new("Electronics", 25, configListElectronics, configtime);


CreatePackage package4 = new("Speakers", "Electronics", "fast", 10, 30);
CreatePackage package5 = new("Camera", "Electronics", "fast", 10, 30);
CreatePackage package6 = new("Laptop", "Electronics", "fast", 10, 30);


/**************************************************************/

// Configuring the size of the Dangerouos goods units
List<StorageConfiguration.WareHouseSizeConfig> configListDangerous =
[
    new() { SizeName = "Tiny", TotalUnitsAvailable = 5, MaxHeightCm = 10.5, MaxWidthCm = 10 },
    new() { SizeName = "Large", TotalUnitsAvailable = 4, MaxHeightCm = 30, MaxWidthCm = 30 }
];
StorageConfiguration Dangerous = new("Dangerous", 25, configListDangerous, configtime);


CreatePackage package7 = new("Propane tank", "Dangerous", "fast", 3, 5);
CreatePackage package8 = new("Firework", "Dangerous", "fast", 3, 5);
CreatePackage package9 = new("You", "Dangerous", "fast", 3, 5);


/**************************************************************/

// Configuring the size of the dry goods units

List<StorageConfiguration.WareHouseSizeConfig> configListDryGoods =
[
    new() { SizeName = "Tiny", TotalUnitsAvailable = 5, MaxHeightCm = 10.5, MaxWidthCm = 10 },
    new() { SizeName = "Large", TotalUnitsAvailable = 4, MaxHeightCm = 30, MaxWidthCm = 30 }
];
StorageConfiguration DryGoods = new("Dry", 25, configListDangerous, configtime);


CreatePackage package10 = new("Propane tank", "Dry", "fast", 3, 5);
CreatePackage package11 = new("Propane tank", "Dry", "fast", 3, 5);
CreatePackage package12 = new("Propane tank", "Dry", "fast", 3, 5);


/**************************************************************/

ReceivingDepartment receivingDepartmentForRefrigiated = new ReceivingDepartment(Refridgerated);
ReceivingDepartment receivingDepartmentForElectronics = new ReceivingDepartment(Electronics);
ReceivingDepartment receivingDepartmentForDangerousGoods = new ReceivingDepartment(Dangerous);
ReceivingDepartment receivingDepartmentForDryGoods = new ReceivingDepartment(DryGoods);

DeliverySchedule deliverySchedule = new DeliverySchedule();
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

deliverySchedule.AddPackageToDay("Single", DayOfWeek.Thursday, package10, DateTime.Now.AddHours(120), DateTime.Now.AddHours(340));
deliverySchedule.AddPackageToDay("Single", DayOfWeek.Thursday, package11, DateTime.Now.AddHours(120), DateTime.Now.AddHours(340));
deliverySchedule.AddPackageToDay("Single", DayOfWeek.Thursday, package12, DateTime.Now.AddHours(120), DateTime.Now.AddHours(340));




DryGoods.CreateStorage();

Terminal terminal = new Terminal();
PackageHistory packageHistory = new PackageHistory();

DateTime startDate = DateTime.Today;
DateTime endDate = DateTime.Today.AddDays(4);

while (startDate < endDate)
{

    if(deliverySchedule.HasPackagesThisDay(startDate.DayOfWeek))
    {
        Console.WriteLine($"Packages at : {startDate.ToString("dd-MM-yyyy")}");
        // Packages that arrive
        var packages = deliverySchedule.GetPackagesForToday(startDate.DayOfWeek);
        foreach(var package in packages)
        {
            receivingDepartmentForDryGoods.ReceivePackage(package.Item2);
            packageHistory.AddPackageLog(package.Item2.PackageId, "Reciving department");
            
        }
        Thread.Sleep(2000);
        TimeSpan delay = new TimeSpan(0, 0, 1);
        Thread.Sleep(delay);
        // Store the packages

        var packagesInReciving = receivingDepartmentForDryGoods.GetPackageList();
        foreach (var package in packagesInReciving)
        {
            packageHistory.AddPackageLog(package.PackageId, "Storage/Shelf");
        }
        receivingDepartmentForDryGoods.SendAllPackagesToStorage();
        Thread.Sleep(2000);
        Thread.Sleep(delay);


        // Before delivery
        List<CreatePackage> packageList = receivingDepartmentForDryGoods.GetAllPackages();

        foreach (var package in DryGoods.GetAllStorageInformationAsDictionary().Values)
        {
            foreach(CreatePackage packagee in packageList)
            {
                if (packagee.PackageId == package.Item1)
                {
                    terminal.AddPackage(packagee);
                    packageHistory.AddPackageLog(package.Item1, "Before delivery/Terminal");

                    DryGoods.MovePackageById(packagee.PackageId);

                }
            }
        }
        Thread.Sleep(2000);
        Thread.Sleep(delay);

        // Giving packages to delviery
        List<CreatePackage> packagesInTerminal = terminal.GetPackagesInTerminal();
        foreach (var package in packagesInTerminal)
        {
            packageHistory.AddPackageLog(package.PackageId, "Sent out / Delivered");
        }
        terminal.RemoveAllPackages();
        Thread.Sleep(2000);
        Thread.Sleep(delay);
    }
    startDate = startDate.AddDays(1);
    
}

packageHistory.GetPackageLog();
Console.WriteLine("Simulation ended");


/*

//
List<StorageConfiguration.WareHouseTimeConfig> configtime =
[
    new() { TimeDeliveryToStorageMinutes = 2, TimeStorageToTerminalMinutes = 5 }
];
//



List<StorageConfiguration.WareHouseSizeConfig> configlistrefig =
[
    new() { SizeName = "Tiny", TotalUnitsAvailable = 5, MaxHeightCm = 10.5, MaxWidthCm = 10 },
    new() { SizeName = "Large", TotalUnitsAvailable = 4, MaxHeightCm = 30, MaxWidthCm = 30 }
];
StorageConfiguration Refridgerated = new("Refridgerated", 25, configlistrefig, configtime);
CreatePackage package4 = new("Dragon", "Refridgerated", "fast", 2, 10);
CreatePackage package5 = new("Dinosaur", "Refridgerated", "fast", 7, 3);
CreatePackage package1 = new("Potato", "Refridgerated", "fast", 20, 10);
//




List<StorageConfiguration.WareHouseSizeConfig> configlistelec =
[
    new() { SizeName = "Tiny", TotalUnitsAvailable = 5, MaxHeightCm = 10.5, MaxWidthCm = 10 },
    new() { SizeName = "Large", TotalUnitsAvailable = 4, MaxHeightCm = 30, MaxWidthCm = 30 }
];
StorageConfiguration Electronics = new("Electronics", 25, configlistelec, configtime);
CreatePackage package2 = new("Speakers", "Electronics", "fast", 10, 30);
//




List<StorageConfiguration.WareHouseSizeConfig> configlistdang =
[
    new() { SizeName = "Tiny", TotalUnitsAvailable = 5, MaxHeightCm = 10.5, MaxWidthCm = 10 },
    new() { SizeName = "Large", TotalUnitsAvailable = 4, MaxHeightCm = 30, MaxWidthCm = 30 }
];
StorageConfiguration Dangerous = new("Dangerous", 25, configlistdang, configtime);
CreatePackage package3 = new("Propane tank", "Dangerous", "fast", 3, 5);
//




List<StorageConfiguration.WareHouseSizeConfig> configlistdry =
[
    new() { SizeName = "Tiny", TotalUnitsAvailable = 5, MaxHeightCm = 10.5, MaxWidthCm = 10 },
    new() { SizeName = "Large", TotalUnitsAvailable = 4, MaxHeightCm = 30, MaxWidthCm = 30 }
];
StorageConfiguration Dry = new("Dry goods", 25, configlistdry, configtime);
CreatePackage package6 = new("Chips", "Dry goods", "fast", 15, 3);
CreatePackage package7 = new("Ost", "Dry goods", "fast", 7, 3);
CreatePackage package8 = new("Moose", "Dry goods", "fast", 7, 3);
//


Terminal terminal = new Terminal();
DeliverySchedule deliverySchedule = new();
ReceivingDepartment receivingDepartment = new(Dry);
Dry.CreateStorage();

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
    Console.WriteLine(package6.PackageId + " Was moved in " + Dry.GetTimeDeliveryToStorage() + " Minutes");
    Console.WriteLine(package7.PackageId + " Was moved in " + Dry.GetTimeDeliveryToStorage() + " Minutes");
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
PackageHistory packageHistory = new PackageHistory(new Dictionary<CreatePackage, (DateTime DeliveryTime, DateTime PickupTime)>());

packageHistory.DeliveryHistory(package1, DateTime.Now);
packageHistory.DeliveryHistory(package2, DateTime.Now);
packageHistory.DeliveryHistory(package3, DateTime.Now);
packageHistory.DeliveryHistory(package6, DateTime.Now);

packageHistory.PickTime(package1, DateTime.Now.AddHours(2));
packageHistory.PickTime(package2, DateTime.Now.AddHours(48));
packageHistory.PickTime(package3, DateTime.Now.AddHours(24));
packageHistory.PickTime(package6, DateTime.Now.AddHours(8));


packageHistory.AllHistoryInfo();

packageHistory.OnePackageHistory(package1.PackageId);
*/
