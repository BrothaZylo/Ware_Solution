// See https://aka.ms/new-console-template for more information

using Ware;

int start = 0;

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

while (start!=1)
{
    deliverySchedule.AddPackageToDay("Single", DayOfWeek.Monday, package6, DateTime.Now, DateTime.Now.AddMinutes(10));
    deliverySchedule.AddPackageToDay("Single", DayOfWeek.Monday, package7, DateTime.Now, DateTime.Now.AddMinutes(30));
    deliverySchedule.AddPackageToDay("Repeating", DayOfWeek.Tuesday, package8, DateTime.Now, DateTime.Now.AddMinutes(40));

    deliverySchedule.GetCalender();
    Console.WriteLine("");


    receivingDepartment.ReceivePackage(package6);
    receivingDepartment.ReceivePackage(package7);
    receivingDepartment.ReceivePackage(package8);
    receivingDepartment.SendFirstPackageToStorage();
    receivingDepartment.SendFirstPackageToStorage();
    receivingDepartment.SendFirstPackageToStorage();
    Console.WriteLine();
    Dry.GetAllStorageInformationPrint();

    Console.WriteLine();
    terminal.AddPackage(Dry.MovePackage(package6));
    terminal.AddPackage(Dry.MovePackage(package7));
    Console.WriteLine();
    terminal.GivingPackagesToDriver();

    Console.WriteLine();
    Console.WriteLine(package6.PackageId+" Was moved in " + Dry.GetTimeDeliveryToStorage() + " Minutes");
    Console.WriteLine(package7.PackageId+" Was moved in " + Dry.GetTimeDeliveryToStorage() + " Minutes");
    Console.WriteLine("");

    Dry.GetAllStorageInformationPrint();
    Console.WriteLine();

    Console.WriteLine("\nNext weeks Schedule:");
    deliverySchedule.ClearSchedule();
    deliverySchedule.GetCalender();
    start = 1;
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