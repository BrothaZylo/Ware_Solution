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
    new() { Sizename = "Tiny", Totalunitsavailable = 5, Maxheightcm = 10.5, Maxwidthcm = 10 },
    new() { Sizename = "Large", Totalunitsavailable = 4, Maxheightcm = 30, Maxwidthcm = 30 }
];
StorageConfiguration Refridgerated = new("Refridgerated", 25, configlistrefig, configtime);
CreatePackage package4 = new("Dragon", "Refridgerated", "fast", 2, 10);
CreatePackage package5 = new("Dinosaur", "Refridgerated", "fast", 7, 3);
CreatePackage package1 = new("Potato", "Refridgerated", "fast", 20, 10);
//




List<StorageConfiguration.WareHouseSizeConfig> configlistelec =
[
    new() { Sizename = "Tiny", Totalunitsavailable = 5, Maxheightcm = 10.5, Maxwidthcm = 10 },
    new() { Sizename = "Large", Totalunitsavailable = 4, Maxheightcm = 30, Maxwidthcm = 30 }
];
StorageConfiguration Electronics = new("Electronics", 25, configlistelec, configtime);
CreatePackage package2 = new("Speakers", "Electronics", "fast", 10, 30);
//




List<StorageConfiguration.WareHouseSizeConfig> configlistdang =
[
    new() { Sizename = "Tiny", Totalunitsavailable = 5, Maxheightcm = 10.5, Maxwidthcm = 10 },
    new() { Sizename = "Large", Totalunitsavailable = 4, Maxheightcm = 30, Maxwidthcm = 30 }
];
StorageConfiguration Dangerous = new("Dangerous", 25, configlistdang, configtime);
CreatePackage package3 = new("Propane tank", "Dangerous", "fast", 3, 5);
//




List<StorageConfiguration.WareHouseSizeConfig> configlistdry =
[
    new() { Sizename = "Tiny", Totalunitsavailable = 5, Maxheightcm = 10.5, Maxwidthcm = 10 },
    new() { Sizename = "Large", Totalunitsavailable = 4, Maxheightcm = 30, Maxwidthcm = 30 }
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
    Console.WriteLine(package6.packageid+" Was moved in " + Dry.GetTimeDeliveryToStorage() + " Minutes");
    Console.WriteLine(package7.packageid+" Was moved in " + Dry.GetTimeDeliveryToStorage() + " Minutes");
    Console.WriteLine("");

    Dry.GetAllStorageInformationPrint();
    Console.WriteLine();

    Console.WriteLine("\nNext weeks Schedule:");
    deliverySchedule.ClearSchedule();
    deliverySchedule.GetCalender();
    start = 1;
}
