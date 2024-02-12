// See https://aka.ms/new-console-template for more information

using Ware;


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

Console.WriteLine("The simulation will run for 10 days where 1 day is 1 second.");
Console.WriteLine("");

// må legge til flere pakker i cases.
int timer = 0;
int totalDays = 10;
int currentDay = 1;

while (currentDay <= totalDays)
{
    Console.WriteLine($"Day {currentDay} of the simulation");

    switch (currentDay)
    {
        case 1:

            deliverySchedule.AddPackageToDay("Single", DayOfWeek.Monday, package6, DateTime.Now, DateTime.Now.AddMinutes(10));
            deliverySchedule.AddPackageToDay("Single", DayOfWeek.Monday, package7, DateTime.Now, DateTime.Now.AddMinutes(30));
            deliverySchedule.AddPackageToDay("Repeating", DayOfWeek.Monday, package8, DateTime.Now, DateTime.Now.AddMinutes(40));
            deliverySchedule.GetCalender();
            receivingDepartment.ReceivePackage(package6);
            receivingDepartment.ReceivePackage(package7);
            receivingDepartment.ReceivePackage(package8);
            break;
        case 2:
            deliverySchedule.AddPackageToDay("Single", DayOfWeek.Tuesday, package3, DateTime.Now, DateTime.Now.AddMinutes(20));
            deliverySchedule.GetCalender();
            receivingDepartment.ReceivePackage(package3);
            break;
        case 3:
            Console.WriteLine($"The Warehouse is quiet on day {currentDay}.");
            break;
        case 5:
            // coinflip chance of earthquacke
            Random random = new Random();
            int chance = random.Next(1, 2);

            if (chance == 1)
            {
                Console.WriteLine("Earthquake! RIP your warehouse is gone :D");
                deliverySchedule.ClearSchedule();
                Console.WriteLine("The staff celebartes their new upcoming holidays.");
            }
            else
            {
                Console.WriteLine("Somehow you feel very lucky that nothing happend on day 5 :)");
            }
            break;
        case 10:
            receivingDepartment.SendFirstPackageToStorage();
            receivingDepartment.SendFirstPackageToStorage();
            receivingDepartment.SendFirstPackageToStorage();
            receivingDepartment.SendFirstPackageToStorage();
            Dry.GetAllStorageInformationPrint();
            break;

        default:
            Console.WriteLine($"Nothing happend on day {currentDay}.");
            break;
    }

    Thread.Sleep(1000);
    currentDay++;
}

Console.WriteLine("Simulation ended.");
