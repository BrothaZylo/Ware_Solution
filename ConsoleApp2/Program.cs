// See https://aka.ms/new-console-template for more information
using Ware;
namespace ConsoleApp2
{
    internal class Program
    {
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
            storage.GetAllStorageInformationPrint();
            storage.PlacePackageAutomatic(package1);
            storage.PlacePackageAutomatic(package7);
            Console.WriteLine();
            storage.GetAllStorageInformationPrint();
            Console.WriteLine();
            storage.UnitShelfsPrint();

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

        }













    }

}
