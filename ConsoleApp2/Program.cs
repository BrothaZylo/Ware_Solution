// See https://aka.ms/new-console-template for more information
using System.Runtime.CompilerServices;
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
            Storage storage2 = new(dry);
            storage2.AddShelf("Big", 11, 100, 100);
            storage2.AddShelf("Tiny", 4, 33, 33);
            storage2.AddShelf("Mid", 2, 63, 63);
            storage.Build();
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

            ReceivingDepartment receivingDepartment = new ReceivingDepartment();

            receivingDepartment.PackageAdded += ReceivingDepartmentAddPackage;
            receivingDepartment.AllPackagesSentToStorage += ReceivingDeapartmentSendAllPackages;


            receivingDepartment.AddPackage(package1);
            receivingDepartment.AddPackage(package2);

            receivingDepartment.SendAllPackagesToStorage(storage2);

        }
        private static void ReceivingDepartmentAddPackage(object sender, PackageEventArgs e)
        {
            Console.WriteLine($"Package was added to Receving department: {e.Package.Name}");
        }

        private static void ReceivingDeapartmentSendAllPackages(object sender, EventArgs e)
        {
            Console.WriteLine($"All Packages Sent from recevingdepartment to Storage");
        }
        */
            /* Terminal event handler 
                        Terminal terminal = new Terminal();
                        terminal.PackageAdded += TerminalPackageAdded;
                        terminal.PackageSent += TerminalPackageSent;
                        terminal.AllPackagesSent += TerminalAllPackagesSent;

                        terminal.AddPackage(package1);
                        terminal.AddPackage(package2);
                        terminal.AddPackage(package3);
                        terminal.AddPackage(package4);

                        terminal.SendAllPackages();

                    }
                    private static void TerminalPackageAdded(object sender, PackageEventArgs e)
                    {
                        Console.WriteLine($"Package was added to Terminal: {e.Package.Name}");
                    }

                    private static void TerminalPackageSent(object sender, PackageEventArgs e)
                    {
                        Console.WriteLine($"Package was sent from Terminal: {e.Package.Name}");
                    }

                    private static void TerminalAllPackagesSent(object sender, EventArgs e)
                    {
                        Console.WriteLine("All packages sent from Terminal.");
                    }

            */




        }
    }

}


