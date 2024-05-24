// See https://aka.ms/new-console-template for more information
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using Ware;
using Ware.Scheduler;
using static Ware.Scheduler.Schedule;
namespace ConsoleApp2
{
    internal class Program
    {
        //------------------------------------------------------------//
        //---------------------Usefull functions----------------------//
        //------------------------------------------------------------//
        static Package ObjCreate(string name, string goodstype, double wid, double heig)
        {
            return new Package(name, goodstype, wid, heig);
        }

        static void Package_p(object? sender, PackageEventArgs e)
        {
            Console.WriteLine(e.Package.PackageId+ " was sent to: "+ e.Storage.GoodsType);
        }
        static void Storage_P(object? sender, StorageEventArgs e)
        {
            Console.WriteLine();
        }

        static void Main(string[] args)
        {


            //------------------------------------------------------------//
            //----------------------Default values------------------------//
            //------------------------------------------------------------//

            string dry = "Dry";
            string dangerous = "Dangerous";
            string refrigerated = "Refrigerated";



            //----------------------------------------------------------//
            //--------------------Default packages----------------------//
            //----------------------------------------------------------//

            Package package1 = new("Chips", dry, 15, 3);
            Package package2 = new("Ost", dry, 14, 23);

            Package package3 = new("Moose", dangerous, 84, 43);
            Package package4 = new("ebb", dangerous, 84, 43);
            Package package5 = new("eee", dangerous, 84, 43);

            Package package6 = new("Cream", refrigerated, 12, 22);
            Package package7 = new("Ice", refrigerated, 18, 12);
            Package package8 = new("Eggs", refrigerated, 18, 12);
            Package package9 = new("Big Moose", refrigerated, 18, 12);
            Package package10 = new("Mini Moose", refrigerated, 18, 12);
            Package package11 = new("Duck", refrigerated, 18, 12);

            
            ScheduleRepeatingModule repeatingModule = new ScheduleRepeatingModule();
            repeatingModule.AddPackageWeekly(package1, "24.12.2024", DayOfWeek.Sunday, TransferType.Delivery);
            repeatingModule.AddPackageWeekly(package2, "22.12.2024", DayOfWeek.Sunday, TransferType.Receive);
            repeatingModule.AddPackageWeekly(package3, "12.12.2024", DayOfWeek.Monday, TransferType.Receive);
            repeatingModule.AddPackageWeekly(package4, "03.12.2024", DayOfWeek.Tuesday, TransferType.Delivery);
            repeatingModule.AddPackageWeekly(package5, "29.12.2024", DayOfWeek.Wednesday, TransferType.Delivery);
            repeatingModule.AddPackageWeekly(package11, "29.12.2024", DayOfWeek.Wednesday, TransferType.Delivery);
            repeatingModule.AddPackageWeekly(package6, "15.12.2024", DayOfWeek.Friday, TransferType.Receive);
            repeatingModule.AddPackageWeekly(package7, "09.12.2024", DayOfWeek.Saturday, TransferType.Delivery);
            repeatingModule.AddPackageDaily(package9, "09.12.2024", TransferType.Receive);
            Schedule schedule = new(repeatingModule);

            schedule.AddPackage(package10, "22.03.2024", DayOfWeek.Thursday, TransferType.Delivery);

            schedule.PrintSchedule();

            Console.WriteLine("-----");

            schedule.DeletePackage(package9);
            schedule.PrintSchedule();

            //-----------------------------------------------------------//
            //----------------------Storage Build------------------------//
            //-----------------------------------------------------------//
            /*
            Storage storage = new(refrigerated, "Uteliggeer");
            storage.AddShelf("Big", 11, 100, 100);
            storage.AddShelf("Tiny", 4, 33, 33);
            storage.AddShelf("Mid", 2, 63, 63);
            storage.Build();

            Storage storage2 = new(dry, "B");
            storage2.AddShelf("Big", 11, 100, 100);
            storage2.AddShelf("Tiny", 4, 33, 33);
            storage2.AddShelf("Mid", 2, 63, 63);
            storage2.Build();

            //-----------------------------------------------------------//
            //----------------------Receiving Build----------------------//
            //-----------------------------------------------------------//
            
            ReceivingDepartment rex = new();
            rex.AddPackage(package1);
            rex.AddPackage(package2);
            rex.AddPackage(package6);

            rex.SendPackageToStorage(package1, storage2, "B101");
            storage2.GetAllStorageInformationPrint();
            Dictionary<string, (Package?, string, double, double, bool)> s = storage2.GetAllStorageInformationAsDictionary();

            Console.WriteLine(storage2.UniqueId);
            foreach(string package in s.Keys)
            {
                Console.WriteLine(package);
            }
            
            //------------------------------------------------------------//
            //---------------------- Pallets Setup -----------------------//
            //------------------------------------------------------------//
            /*
            Pallet pallet1 = new Pallet();
            pallet1.AddPackageToPallet(package3);
            pallet1.AddPackageToPallet(package4);
            pallet1.AddPackageToPallet(package5);

            Pallet pallet2 = new Pallet();
            pallet2.AddPackageToPallet(package1);
            pallet2.AddPackageToPallet(package2);

            Pallet pallet3 = new Pallet();
            pallet3.AddPackageToPallet(package6);
            pallet3.AddPackageToPallet(package7);

            PalletStorage palletStorage = new PalletStorage("Ebay");
            palletStorage.AddShelf("Tiny", 2, 3);
            palletStorage.AddShelf("Mid", 3, 4);

            PalletStorage palletStorage1 = new PalletStorage("Amazon");
            palletStorage1.AddShelf("Tiny", 2, 3);

            palletStorage.BuildStorage();
            palletStorage1.BuildStorage();

            Console.WriteLine("pallets placed in storage");
            palletStorage.PlacePalletAutomatic(pallet1);
            palletStorage.PlacePalletAutomatic(pallet2);
            palletStorage.PlacePallet(pallet3, "Shelf-2", 1, 1);
            palletStorage.PrintAllPalletStorageInformation();
            Console.WriteLine("\nThe packages in pallet in Shelf-2 at Floor: 2 Position: 2");
            palletStorage.PrintPalletInformation("Shelf-2", 1, 1);

            Console.WriteLine("\nsecond pallet storage");
            palletStorage1.PrintAllPalletStorageInformation();

            Terminal terminal = new Terminal();

            palletStorage.SendPalletToTerminalAutomatic(pallet1, terminal);

            Console.WriteLine("\npallet 1 and 3 sent to terminal but ont pallet 2");
            palletStorage.PrintAllPalletStorageInformation();

            Console.WriteLine("\ncurrent pallets in terminal");
            terminal.PrintPalletInformation();

            TimeEstimate t = new TimeEstimate();
            Console.WriteLine("\n");
            t.SetTimeStorageGetPackage(storage, TimeEstimate.PlaceOrGetBox.GET, 1, 5, 10);
            t.SetTimeStorageGetPackage(storage, TimeEstimate.PlaceOrGetBox.PLACE, 6, 8, 20);
            t.GetStorageTimeToGetPackagePrint();


            Console.WriteLine("\n");
            t.GetStorageTimeToGetPackageDictionary(storage);

            ReceivingDepartment receivingDepartment = new ReceivingDepartment();
            receivingDepartment.AddPackage(package6);
            receivingDepartment.SendAllPackagesToStorage(storage);

            Aisle aisle = new Aisle("Kj�l");
            aisle.AddStorage(storage);
            aisle.FindPackage(package6);
            aisle.removeStorage(storage);

            PalletAisle palletAisle = new PalletAisle("pallet");
            palletAisle.AddPalletStorage(palletStorage);
            palletAisle.GetAllPalletPrints();

            */
            //-----------------------------------------------------------//
            //--------------------------Events---------------------------//
            //-----------------------------------------------------------//

            /*
            ReceivingDepartment receivingDepartment = new();
            receivingDepartment.SendAllPackageEvent += Package_p;
            receivingDepartment.AddPackage(package7);
            receivingDepartment.AddPackage(package5);
            receivingDepartment.AddPackage(package1);
            receivingDepartment.AddPackage(package2);
            receivingDepartment.SendAllPackagesToStorage(storage);

            */




            //------------------------------------------------------------//
            //---------------------- Pallets Setup -----------------------//
            //------------------------------------------------------------//
            /*
            Pallet pallet1 = new Pallet();
            pallet1.AddPackageToPallet(package3);
            pallet1.AddPackageToPallet(package4);
            pallet1.AddPackageToPallet(package5);

            Pallet pallet2 = new Pallet();
            pallet2.AddPackageToPallet(package1);
            pallet2.AddPackageToPallet(package2);

            Pallet pallet3 = new Pallet();
            pallet3.AddPackageToPallet(package6);
            pallet3.AddPackageToPallet(package7);

            PalletStorage palletStorage = new PalletStorage();
            palletStorage.AddShelf("Tiny", 3, 4);

            palletStorage.BuildStorage();

            try
            {
                palletStorage.PlacePallet(pallet1, "Shelf-1", 0);
                palletStorage.PlacePallet(pallet2, "Shelf-1", 1);
                palletStorage.PlacePallet(pallet3, "Shelf-2", 3);

                palletStorage.PrintAllPalletStorageInformation();
            }
            catch (Exception message)
            {
                Console.WriteLine($"An error occurred: {message.Message}");
            }
            */

            //----------------------------------------------------------//
            //----------------------Access Level------------------------//
            //----------------------------------------------------------//
            /*
            Person karl = new("Karl Oogus", 45, CrewList.AccessLevel.EMPLOYEE);
            Equipment forklift = new("forklift", 2);
            Equipment highvaluegoods = new("High Value Goods Door", 1);
            forklift.AddAccessLevel(CrewList.AccessLevel.CEO);

            forklift.GetAccessLevelPrint("forklift");
            highvaluegoods.AddAccessLevel(CrewList.AccessLevel.EMPLOYEE);
            bool x = forklift.HasAccess(karl);
            Console.WriteLine(x);
            */
            //----------------------------------------------------------//
            //-----------------------Simulation-------------------------//
            //----------------------------------------------------------//


            /*
            Simulation sim = new(30);

            sim.AddPackage(package1);
            sim.AddPackage(package2);
            sim.AddPackage(package3);
            sim.AddPackage(package4);
            sim.AddPackage(package5);
            sim.AddPackage(package6);
            sim.AddPackage(package7);
            sim.Run();
            */

            /*
            TrueSimulation sim = new(35);
            ReceivingDepartment r1 = new("Rec1");
            ReceivingDepartment r2 = new("Rec2");
            Terminal terminal1 = new("Terminal1");
            KittingArea k1 = new();
            PackingArea p1 = new();
            PackingArea p2 = new();


            k1.SchedulePackageForKittingArea(package1);
            p1.SchedulePackage(package7);
            p1.SchedulePackage(package10);
            p2.SchedulePackage(package9);

            Storage storage1 = new(refrigerated, "A");
            storage1.AddShelf("Big", 11, 100, 100);
            storage1.AddShelf("Tiny", 4, 33, 33);
            storage1.AddShelf("Mid", 5, 63, 63);
            storage1.Build();

            Storage storage2 = new(dry, "B");
            storage2.AddShelf("Big", 11, 100, 100);
            storage2.AddShelf("Tiny", 4, 33, 33);
            storage2.AddShelf("Mid", 5, 63, 63);
            storage2.Build();

            Pallet pallet1 = new();
            pallet1.SchedulePackageToPack(package7);
            pallet1.SchedulePackageToPack(package10);

            EquipmentForklift equipmentForklift = new("Bigboy Lifter", 1);

            PalletStorage palletStorage1 = new("storPallet");
            palletStorage1.AddShelf("big", 15, 7);
            palletStorage1.BuildStorage();

            Person person = new("oga", 32, AccessLevel.OPERATOR);
            equipmentForklift.AddAccessLevel(AccessLevel.OPERATOR);
            

            sim.AddPersonToSimulation(person);

            sim.AddPalletStorageToSimulation(palletStorage1);

            sim.AddEquipmentToSimulation(equipmentForklift);

            sim.AddPalletToSimulation(pallet1);

            sim.AddReceivingDepartmentToSimulation(r1);
            //sim.AddReceivingDepartmentToSimulation(r2);

            sim.AddPackingAreaToSimulation(p1);
            //sim.AddPackingAreaToSimulation(p2);
            sim.AddKittingAreaToSimulation(k1);

            sim.AddPackageToSimulation(package1);
            sim.AddPackageToSimulation(package2);
            sim.AddPackageToSimulation(package6);
            sim.AddPackageToSimulation(package7);
            sim.AddPackageToSimulation(package8);
            sim.AddPackageToSimulation(package9);
            sim.AddPackageToSimulation(package10);
            sim.AddPackageToSimulation(package11);

            sim.AddStorageToSimulation(storage1);
            sim.AddStorageToSimulation(storage2);

            sim.AddTerminalToSimulation(terminal1);
            sim.Run();
            */
            /*
            EquipmentDoor doorEquipment = new("hDoor", 1);
            Person person = new("oga",32, AccessLevel.EXTRA1);
            doorEquipment.AddAccessLevel(AccessLevel.EXTRA1);

            EquipmentList equipmentList = new EquipmentList();
            equipmentList.AddEquipment(doorEquipment);
            Equipment equipment = new("eghg", 2);

            StorageSmall storageSmall = new(dry, "dD");
            storageSmall.AddShelf("Big", 11, 100, 100);
            storageSmall.Build();
            */
        }
    }
}


