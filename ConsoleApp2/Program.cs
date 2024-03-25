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

        static void Package_p(Package package, Storage storage)
        {
            Console.WriteLine(package.PackageId+ " was sent to: "+ storage.GoodsType);
            Environment.Exit(0);
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

            Storage storage = new(refrigerated);
            storage.AddShelf("Big", 11, 100, 100);
            storage.AddShelf("Tiny", 4, 33, 33);
            storage.AddShelf("Mid", 2, 63, 63);
            storage.Build();

            Storage storage2 = new(dry);
            storage2.AddShelf("Big", 11, 100, 100);
            storage2.AddShelf("Tiny", 4, 33, 33);
            storage2.AddShelf("Mid", 2, 63, 63);
            storage2.Build();

            //-----------------------------------------------------------//
            //--------------------------Events---------------------------//
            //-----------------------------------------------------------//

            ReceivingDepartment receivingDepartment = new();
            receivingDepartment.PackageEvent += Package_p;
            receivingDepartment.AddPackage(package7);
            receivingDepartment.AddPackage(package5);
            receivingDepartment.AddPackage(package1);
            receivingDepartment.AddPackage(package2);
            receivingDepartment.SendAllPackagesToStorage(storage);
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


