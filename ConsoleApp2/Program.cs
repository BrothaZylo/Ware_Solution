// See https://aka.ms/new-console-template for more information
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using Ware;
using Ware.Equipments;
using Ware.KittingAreas;
using Ware.Packages;
using Ware.PackingAreas;
using Ware.Pallets;
using Ware.PalletStorages;
using Ware.Persons;
using Ware.ReceivingDepartments;
using Ware.Simulations;
using Ware.Storages;
using Ware.Terminals;
using static Ware.Schedules.Schedule;
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
            Console.WriteLine(e.package.PackageId + " was sent to: " + e.storage.GoodsType);
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
            Package package2 = new("Ost", dry, 14, 2);

            Package package3 = new("Moose", dangerous, 8, 3);
            Package package4 = new("Nuke", dangerous, 8, 3);
            Package package5 = new("TNT", dangerous, 4, 3);

            Package package6 = new("Cream", refrigerated, 12, 22);
            Package package7 = new("Ice", refrigerated, 18, 12);
            Package package8 = new("Eggs", refrigerated, 18, 12);
            Package package9 = new("Big Moose", refrigerated, 18, 12);
            Package package10 = new("Mini Moose", refrigerated, 18, 12);
            Package package11 = new("Duck", refrigerated, 18, 12);

            /*

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

            */

            //------------------------------------------------------------//
            //---------------------- Kitting Area  -----------------------//
            //------------------------------------------------------------//

            /*
            KittingArea kittingArea = new("kittingAreee");

            kittingArea.CreateKittingBox();

            List<KittingBox> kit = kittingArea.GetKittingBoxesInKittingArea();

            foreach (KittingBox box in kit)
            {
                kittingArea.AddPackageToKittingBox(box, package9);
                Console.WriteLine(box.PackageId);
            }

            Terminal terminal = new(" terminal");
            terminal.AddPackage(kit[0]);
            */

            /*
            Storage storage = new(dry, "Uteliggeer");
            storage.AddShelf("Big", 11, 100, 100);
            storage.Build();

            Terminal terminal = new Terminal("terminal");

            KittingArea kittingArea = new KittingArea("KittingArea");
            kittingArea.AddPackageToKittingArea(package1);
            kittingArea.AddPackageToKittingArea(package2);


            KittingBox kit = new KittingBox("kitbox1", dry, 10, 10);

            kit.AddPackage(package1);
            kit.AddPackage(package2);

            

            //storage
            storage.PlacePackageAutomatic(kit);
            storage.GetAllStorageInformationPrint();


            //terminal
            terminal.AddPackage(kit);
            terminal.PrintPackageList();
            terminal.SendPackage(kit);
            Console.WriteLine("Send ut pakke kit og printer");
            terminal.PrintPackageList();
            */
            /*

            kittingArea.CardBox = new KittingBox("Dangerous", dry, 10, 10);
            kittingArea.AddPackageToBox(package1);
            kittingArea.AddPackageToBox(package2);
            KittingBox foodBox = kittingArea.AddIntoKittingBox();


            kittingArea.CardBox = new KittingBox("Dangerous", dry, 10, 10);
            kittingArea.AddPackageToBox(package3);
            kittingArea.AddPackageToBox(package4);
            kittingArea.AddPackageToBox(package5);
            KittingBox dangerousBox = kittingArea.AddIntoKittingBox();

            //Teste id og gods
            Console.WriteLine(foodBox.PackageId);
            Console.WriteLine(foodBox.Goods);

            //adder boxene i kittingarea og printer
            kittingArea.AddKittingBoxToArea(foodBox);
            kittingArea.AddKittingBoxToArea(dangerousBox);
            kittingArea.PrintAllKittingBoxes();

            //TEster terminal kitting
            Console.WriteLine("\nAdding foodbox in terminal:");
            terminal.AddKittingBox(foodBox);
            terminal.PrintKittingBoxesInformation();
            Console.WriteLine("\nAfter sending out foodbox");
            terminal.SendOutKittingBox(foodBox);
            terminal.PrintKittingBoxesInformation();
            */



            //-----------------------------------------------------------//
            //----------------------Storage Build------------------------//
            //-----------------------------------------------------------//

            /*
            Storage storage = new(refrigerated, "Uteliggeer");
            storage.AddShelf("Big", 11, 100, 100);
            storage.AddShelf("Tiny", 4, 33, 33);
            storage.AddShelf("Mid", 2, 63, 63);
            storage.Build();

            storage.PlacePackageAutomatic(kittingbox);
            Console.WriteLine("Package id: " + kittingbox.PackageId);
            */

            /*
            Storage storage2 = new(dry, "B");
            storage2.AddShelf("Big", 11, 100, 100);
            storage2.AddShelf("Tiny", 4, 33, 33);
            storage2.AddShelf("Mid", 2, 63, 63);
            storage2.Build();
            */
            //-----------------------------------------------------------//
            //----------------------Receiving Build----------------------//
            //-----------------------------------------------------------//

            /*
            ReceivingDepartment rex = new("Rec Depart");
            rex.AddPackage(package1);
            rex.AddPackage(package2);
            rex.AddPackage(package3);
            rex.AddPackage(package4);
            rex.AddPackage(package5);
            rex.AddPackage(package6);
            rex.AddPackage(package7);
            */

            /*
            rex.SendPackageToStorage(package1, storage2, "B101");
            storage2.GetAllStorageInformationPrint();
            Dictionary<string, (Package?, string, double, double, bool)> s = storage2.GetAllStorageInformationAsDictionary();

            Console.WriteLine(storage2.UniqueId);
            foreach(string package in s.Keys)
            {
                Console.WriteLine(package);
            }
            */

            //------------------------------------------------------------//
            //---------------------- Pallets Setup -----------------------//
            //------------------------------------------------------------//
            /*
            PackingArea packingArea = new PackingArea("Packing area");
            packingArea.SendPackageToPackingArea(package3);
            packingArea.SendPackageToPackingArea(package4);
            packingArea.SendPackageToPackingArea(package5);

            Pallet pallet1 = new Pallet();
            packingArea.AddPackageOnPallet(package3, pallet1);
            packingArea.AddPackageOnPallet(package4, pallet1);
            packingArea.AddPackageOnPallet(package5, pallet1);

            Pallet pallet2 = new Pallet();
            packingArea.AddPackageOnPallet(package1, pallet2);
            packingArea.AddPackageOnPallet(package2, pallet2);

            Pallet pallet3 = new Pallet();
            packingArea.AddPackageOnPallet(package6, pallet3);
            packingArea.AddPackageOnPallet(package7, pallet3);




            PalletStorage palletStorage = new PalletStorage("PalletStorage_1");
            palletStorage.AddShelf("Tiny", 3);
            palletStorage.AddShelf("Mid", 4);
            palletStorage.BuildStorage();

            Console.WriteLine("pallets placed in storage");
            palletStorage.PlacePalletAutomatic(pallet1);
            palletStorage.PlacePallet(pallet3, "PalletStorage_1 : Shelf 005");
            palletStorage.PrintAllPalletStorageInformation();

            Console.WriteLine("\nCheck if pallet in storage");
            Pallet? foundShelf = palletStorage.GetPallet(pallet1);
            if (foundShelf != null)
            {
                Console.WriteLine($"Pallet found on shelf: {foundShelf}");
            }
            else
            {
                Console.WriteLine("Pallet not found.");
            }

            Console.WriteLine("\nCheck pallet not in storage");
            Pallet? notFoundShelf = palletStorage.GetPallet(pallet2);
            if (notFoundShelf != null)
            {
                Console.WriteLine($"Pallet found on shelf: {notFoundShelf}");
            }
            else
            {
                Console.WriteLine("Pallet not found.");
            }

            Terminal terminal = new Terminal("Terminal numba 1");

            palletStorage.SendPalletToTerminal(pallet1, terminal);
            palletStorage.SendPalletToTerminal(pallet3, terminal);

            Console.WriteLine("\npallet 1 and 3 sent to terminal");
            palletStorage.PrintAllPalletStorageInformation();

            Console.WriteLine("\ncurrent pallets in terminal");
            terminal.PrintPalletsInformation();

            Console.WriteLine("\nSend out pallet 1, current pallets in terminal");
            terminal.SendOutPallet(pallet1);
            terminal.PrintPalletsInformation();

            Console.WriteLine("\nSend out all pallets, current pallets in terminal");
            terminal.SendOutPallets();
            terminal.PrintPalletsInformation();
            */


            /*
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
            Pallet pallet1 = new Pallet("Palelt1");

            Pallet pallet2 = new Pallet("Pallet2");

            Pallet pallet3 = new Pallet("Pallet3");
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


            Simulation sim = new(35);


            sim.ReceivedPackageEvent += OnPackageReceived;
            sim.SendToStoragePackageEvent += OnPackageToStorage;
            sim.AddToKittingAreaPackageEvent += OnPackageToKittingArea;
            sim.AddToPackingAreaEvent += OnPackageToPackingArea;
            sim.SendPackageToTerminalEvent += OnPackageToTerminal;
            sim.SendPackageOutTerminalEvent += OnPackageSentOutOfTerminal;
            sim.PlacePackageOntoPalletEvent += OnPackagePlacedOntoPallet;
            sim.SendPalletToPalletStorageEvent += OnPalletToPalletStorage;
            sim.SendPalletToPalletToTerminalEvent += OnPalletToTerminal;
            sim.BoxCreatedInKittingAreaEvent += OnBoxCreatedInKittingArea;
            sim.PackagedAddedInBoxEvent += OnPackageAddedInBox;
            sim.SendBoxToTerminalEvent += OnBoxToTerminal;

            ReceivingDepartment r1 = new("Receiving 1");
            ReceivingDepartment r2 = new("Receiving 2");
            Terminal terminal1 = new("Terminal1");
            KittingArea k1 = new("Kitting Area 133");
            PackingArea p1 = new("Packing Area 1");
            PackingArea p2 = new("Packing Area 2");

            k1.SchedulePackageForKittingArea(package1);
            k1.SchedulePackageForKittingArea(package8);
            p1.SchedulePackage(package7);
            p1.SchedulePackage(package10);
            p2.SchedulePackage(package9);

            Storage storage1 = new("Refrigerated", "A");
            storage1.AddShelf("Big", 11, 100, 100);
            storage1.AddShelf("Tiny", 4, 33, 33);
            storage1.AddShelf("Mid", 5, 63, 63);
            storage1.Build();

            Storage storage2 = new("Dry", "B");
            storage2.AddShelf("Big", 11, 100, 100);
            storage2.AddShelf("Tiny", 4, 33, 33);
            storage2.AddShelf("Mid", 5, 63, 63);
            storage2.Build();

            Pallet pallet1 = new("pallet 4386");
            pallet1.SchedulePackageToPack(package7);
            pallet1.SchedulePackageToPack(package10);

            EquipmentForklift equipmentForklift = new("Bigboy Lifter", 1);
            equipmentForklift.AddAccessLevel(AccessLevel.OPERATOR);

            PalletStorage palletStorage1 = new("Pallet Storage 8147");
            palletStorage1.AddShelf("big", 15);
            palletStorage1.BuildStorage();

            Person person = new("Ben", 32, AccessLevel.OPERATOR);


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
        private static void OnPackageReceived(object o, PackageEventArgs args)
        {

            Console.WriteLine($"Package {args.package.Name} was received in {args.receivingDepartment.Name}");
        }

        private static void OnPackageToStorage(object o, PackageEventArgs args)
        {

            Console.WriteLine($"Package {args.package.Name} was sent to storage");
        }

        private static void OnPackageToKittingArea(object sender, PackageEventArgs e)
        {

            Console.WriteLine($"Package {e.package.Name} was sent to kitting area");
        }

        private static void OnPackageToPackingArea(object sender, PackageEventArgs e)
        {

            Console.WriteLine($"Package {e.package.Name} was sent to packing area");
        }

        private static void OnPackageToTerminal(object sender, PackageEventArgs e)
        {

            Console.WriteLine($"Package {e.package.Name} was sent to terminal");
        }

        private static void OnPackageSentOutOfTerminal(object sender, PackageEventArgs e)
        {

            Console.WriteLine($"Package {e.package.Name} was sent out of terminal");
        }

        private static void OnPackagePlacedOntoPallet(object sender, PackageEventArgs e)
        {

            Console.WriteLine($"Package {e.package.Name} was placed onto pallet");
        }

        private static void OnPalletToPalletStorage(object sender, PackageEventArgs e)
        {

            Console.WriteLine($"Pallet {e.pallet.PalletName} was sent to {e.palletStorage.StorageName} by {e.person.Name} using {e.equipment.Name}");
        }

        private static void OnPalletToTerminal(object sender, PackageEventArgs e)
        {

            Console.WriteLine($"Pallet {e.pallet.PalletName} was sent to {e.terminal.Name}");
        }

        private static void OnBoxCreatedInKittingArea(object sender, PackageEventArgs e)
        {
    
            Console.WriteLine($"Box: {e.box.Name} was created in kitting area: {e.kittingArea.KittingName}");
        }

        private static void OnPackageAddedInBox(object sender, PackageEventArgs e)
        {

            Console.WriteLine($"{e.package.Name} was put in {e.box.Name} ");
        }

        private static void OnBoxToTerminal(object sender, PackageEventArgs e)
        {

            Console.WriteLine($"{e.package.Name} was sent to {e.terminal.Name}");
        }
    }
}
