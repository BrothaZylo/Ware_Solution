// See https://aka.ms/new-console-template for more information
using System.Runtime.CompilerServices;
using Ware;
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

            Package package6 = new("Cream", refrigerated, 84, 43);
            Package package7 = new("Ice", refrigerated, 18, 39);
            //-----------------------------------------------------------//
            //----------------------Storage Build------------------------//
            //-----------------------------------------------------------//

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



            //------------------------------------------------------------//
            //---------------------- Pallets Setup -----------------------//
            //------------------------------------------------------------//

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
            palletStorage.AddShelf("Tiny", 2, 3);
            palletStorage.AddShelf("Mid", 3, 4);

            palletStorage.BuildStorage();

            Console.WriteLine("pallets placed in storage");
            palletStorage.PlacePalletAutomatic(pallet1);
            palletStorage.PlacePalletAutomatic(pallet2);
            palletStorage.PlacePallet(pallet3, "Shelf-2", 1, 1);
            palletStorage.PrintAllPalletStorageInformation();

            Terminal terminal = new Terminal();

            palletStorage.SendPalletToTerminalAutomatic(pallet1, terminal);
            palletStorage.SendPalletToTerminal("Shelf-2", 1, 1, terminal);

            Console.WriteLine("\npallet 1 and 3 sent to terminal but ont pallet 2");
            palletStorage.PrintAllPalletStorageInformation();

            Console.WriteLine("\ncurrent pallets in terminal");
            terminal.PrintPalletInformation();

            TimeEstimate t = new TimeEstimate();
            t.SetTimeStorage(storage, 1, 5, 10);
            t.SetTimeStorage(storage, 6, 8, 20);
            t.prnt();

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
            /
            //----------------------------------------------------------//
            //-----------------------Simulation-------------------------//
            //----------------------------------------------------------//

            /
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


        }
    }
}


