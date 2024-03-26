using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Ware
{
    /// <summary>
    /// Simulates how the system would act with Refrigerated, Dangrous and Dry storage units.
    /// </summary>
    /// <param name="seconds">Seconds the simulation will run</param>
    public class Simulation(int seconds = 60) : ISimulation
    {
        private readonly int seconds = seconds;
        private readonly List<Package> simulationPackages = [];
        private static readonly Storage Dry = new("Dry", "A");
        private static readonly Storage Refrigerated = new("Refrigerated", "B");
        private static readonly Storage Dangerous = new("Dangerous", "C");

        private readonly PackageLogging packageLogging = new();
        private ReceivingDepartment receiving = new();
        private readonly Schedule schedule = new();
        private readonly Terminal terminal = new();

        private bool a = true;
        private bool b = true;
        private bool c = true;

        private int dry, refrigerated, dangerous;

        //Define delegate
        //Define an event based on the delegate
        // Raise the event
    

        /// <summary>
        /// Adds packages that will run in the simulation. Only add packages with the goodtype of Refrigerated, Dangerous or Dry.
        /// </summary>
        /// <param name="package">package class object</param>
        public void AddPackage(Package package)
        {
            simulationPackages.Add(package);
        }

        public void Run()
        {
            int start = 1;
            int stop = seconds;
            int delay = 1000;
            int startDelay = 4000;

            Console.WriteLine(" ---------------------");
            Console.WriteLine("| Simulation starting  |");
            Console.WriteLine("|      Loading...      |");
            Console.WriteLine(" ---------------------\n\n");

            CalculateAmountOfGoodsType();
            AddUnits();
            BuildStorages();
            CreateSchedule();

            Thread.Sleep(startDelay);
            int randomDelay = RandomDelay();


            while (start != stop)
            {
                if (start == CalculateSimulationTime(5))
                {
                    schedule.GetSchedule();
                }
                if (start == CalculateSimulationTime(10))
                {
                    ReceivePackages();
                }

                if (start == CalculateSimulationTime(15))
                {
                    SendPackagesToStorage();
                }

                if (start == CalculateSimulationTime(25))
                {
                    PrintStorages();
                }
                if (start == CalculateSimulationTime(30))
                {
                    FromStorageToTerminal();
                }
                if (start == CalculateSimulationTime(35))
                {
                    FromTerminalAndAway();
                }
                /***********************************/
                if (start == CalculateSimulationTime(40))
                {
                    ReceivePackages();
                }

                if (start == CalculateSimulationTime(45))
                {
                    SendPackagesToStorage();
                }

                if (start == CalculateSimulationTime(50))
                {
                    PrintStorages();
                }
                if (start == CalculateSimulationTime(55))
                {
                    FromStorageToTerminal();
                }
                if (start == CalculateSimulationTime(60))
                {
                    FromTerminalAndAway();
                }

                /*********************/
                if (start == CalculateSimulationTime(65 + randomDelay))
                {
                    ReceivePackages();
                }

                if (start == CalculateSimulationTime(70 + randomDelay))
                {
                    SendPackagesToStorage();
                }

                if (start == CalculateSimulationTime(75 + randomDelay))
                {
                    PrintStorages();
                }
                if (start == CalculateSimulationTime(80 + randomDelay))
                {
                    FromStorageToTerminal();
                }

                if (start == CalculateSimulationTime(81 + randomDelay))
                {
                    FromTerminalAndAway();
                }

                Console.WriteLine("\n---------------");
                Console.WriteLine("TimeStamp: " + start);
                Console.WriteLine("---------------\n");
                Thread.Sleep(delay);
                start++;
            }
            Console.WriteLine("Simulation ended at: " + (stop) + " Seconds\n");

            Console.WriteLine("The total delay was " + randomDelay + " seconds in this simulation ");
        }

        private static void BuildStorages()
        {
            Dry.Build();
            Refrigerated.Build();
            Dangerous.Build();
        }

        private void CalculateAmountOfGoodsType()
        {
            foreach (Package package in simulationPackages)
            {
                if (package.Goods == "Refrigerated")
                {
                    refrigerated++;
                }
                if (package.Goods == "Dry")
                {
                    dry++;
                }
                if (package.Goods == "Dangerous")
                {
                    dangerous++;
                }
            }
        }
        
        private void ReceivePackages()
        {
          
            Random rand = new Random();

            if (a)
            {
                a = false;
                foreach (Package package in simulationPackages)
                {
                    if (package.Goods == "Dry")
                    {
                        receiving.AddPackage(package);
                    }
                }
                return;
            }
            if (b)
            {
                b = false;
                foreach (Package package in simulationPackages)
                {
                    if (package.Goods == "Dangerous")
                    {
                        receiving.AddPackage(package);
                    }
                }
                return;
            }
            if (c)
            {
                c = false;
                foreach (Package package in simulationPackages)
                {
                    if (package.Goods == "Refrigirated")
                    {
                        receiving.AddPackage(package);
                    }
                }
                return;
            }

        }

        /*
        private void RecievePackages()
        {
            if (dry > refrigerated && dry > dangerous && dry != 0)
            {
                foreach(Package package in simulationPackages)
                {
                    if (package.Goods == "Dry")
                    {
                        receiving.AddPackage(package);
                        receiving.PackageAddedToReceivingDepartment += OnPackageReceieved;
                    }
                }
                dry = 0;
            }
            if (refrigerated > dry && refrigerated > dangerous && refrigerated != 0)
            {
                foreach (Package package in simulationPackages)
                {
                    if (package.Goods == "Refrigerated")
                    {
                        receiving.AddPackage(package);
                        receiving.PackageAddedToReceivingDepartment += OnPackageReceieved;

                    }
                }
                refrigerated = 0;
            }
            if (dangerous > refrigerated && dangerous > dry && dangerous != 0)
            {
                foreach (Package package in simulationPackages)
                {
                    if (package.Goods == "Dangerous")
                    {
                        receiving.AddPackage(package);
                        receiving.PackageAddedToReceivingDepartment += OnPackageReceieved;

                    }
                }
                dangerous = 0;
            }
            if(dry == dangerous && dry != 0 || dry == refrigerated && dry != 0)
            {
                foreach (Package package in simulationPackages)
                {
                    if (package.Goods == "Dry")
                    {
                        receiving.AddPackage(package);
                        receiving.PackageAddedToReceivingDepartment += OnPackageReceieved;

                    }
                }
                dry = 0;
            }
            if (refrigerated == dangerous && refrigerated != 0)
            {
                foreach (Package package in simulationPackages)
                {
                    if (package.Goods == "Refrigerated")
                    {
                        receiving.AddPackage(package);
                        receiving.PackageAddedToReceivingDepartment += OnPackageReceieved;

                    }
                }
                refrigerated = 0;
            }
        }
        */
        
        private void AddUnits()
        {
            for (int index = 0; index < simulationPackages.Count; index++)
            {
                double height = simulationPackages[index].Height;
                double width = simulationPackages[index].Width;
                string goods = simulationPackages[index].Goods;

                if (goods == "Dry")
                {
                    Dry.AddShelf("Autosized", 3, height + 10, width + 10);
                }
                if (goods == "Refrigerated")
                {
                    Refrigerated.AddShelf("Autosized", 3, height + 10, width + 10);
                }
                if (goods == "Dangerous")
                {
                    Dangerous.AddShelf("Autosized", 3, height + 10, width + 10);
                }
            }
        }

        private void SendPackagesToStorage()
        {
            try
            {
                receiving.SendAllPackagesToStorage(Dry);
                receiving.SendAllPackagesToStorage(Dangerous);
                receiving.SendAllPackagesToStorage(Refrigerated);

            }
            catch (PackageInvalidException e)
            {
                Console.WriteLine(e);
            }

        }

        private void FromDryToTerminal()
        {
            foreach (KeyValuePair<string, (Package?, string, double, double, bool)> a in Dry.GetAllStorageInformationAsDictionary())
            {
                try
                {
                    foreach (Package package in simulationPackages)
                    {
                        if (Dry.IsSameTypeOfGoods(package) && package == a.Value.Item1)
                        {
                            Dry.MovePackageToTerminal(package, terminal);
                        }
                    }
                }
                catch (PackageInvalidException e)
                {
                    Console.WriteLine(e);
                }
            }
        }

        private void FromRefrigiratedToTerminal()
        {
            foreach (KeyValuePair<string, (Package?, string, double, double, bool)> a in Refrigerated.GetAllStorageInformationAsDictionary())
            {
                try
                {
                    foreach (Package package in simulationPackages)
                    {
                        if (Refrigerated.IsSameTypeOfGoods(package) && package == a.Value.Item1)
                        {
                            Refrigerated.MovePackageToTerminal(package, terminal);
                        }
                    }
                }
                catch (PackageInvalidException e)
                {
                    Console.WriteLine(e);
                }
            }
        }

        private void FromDangerousToTerminal()
        {
            foreach (KeyValuePair<string, (Package?, string, double, double, bool)> a in Dangerous.GetAllStorageInformationAsDictionary())
            {
                try
                {
                    foreach (Package package in simulationPackages)
                    {
                        if (Dangerous.IsSameTypeOfGoods(package) && package == a.Value.Item1)
                        {
                            Dangerous.MovePackageToTerminal(package, terminal);
                        }
                    }
                }
                catch (PackageInvalidException e)
                {
                    Console.WriteLine(e);
                }
            }
        }

        private void FromStorageToTerminal()
        {
            FromDryToTerminal();
            FromRefrigiratedToTerminal();
            FromDangerousToTerminal();
        }

        private static void PrintStorages()
        {
            Dry.GetAllStorageInformationPrint();
            Console.WriteLine();
            Refrigerated.GetAllStorageInformationPrint();
            Console.WriteLine();
            Dangerous.GetAllStorageInformationPrint();
        }

        private int CalculateSimulationTime(int percentageNumber)
        {
            int s = seconds;

            return percentageNumber * s / 100;
        }

        private DateTime AutomaticTimeCreator(int percentageRunSim)
        {
            int s = seconds;
            int sum = percentageRunSim * s / 100;

            DateTime day = DateTime.Now;
            day.AddMinutes(sum);
            return day;
        }

        private void CreateSchedule()
        {
            foreach (Package package in simulationPackages)
            {
                Random rand = new();
                int u = rand.Next(0, 30);
                schedule.AddPackage("single", DayOfWeek.Monday, package, AutomaticTimeCreator(u));
            }
        }

        private void FromTerminalAndAway()
        {
            terminal.SendAllPackages();
        }

        private int RandomDelay()
        {
            Random r = new Random();
            int randomNumber = r.Next(0, 2);
            Console.WriteLine(randomNumber);
            if (randomNumber == 1)
            {
                Console.WriteLine("Pick up has been delayed");
                Random rr = new Random();
                return rr.Next(5, 10);

            }
            return 0;
        }

        /// <summary>
        /// Starts the simulation with the added packages. 60 seconds recommended runtime.
        /// </summary>
        public static void OnAllPackagesSentToStorage(object o, PackageEventArgs args)
        {
            Console.WriteLine($"Packages was sent from the the receiving department to storage {args.Storage.GoodsType}");
        }

        private static void OnPackageReceieved(object o, PackageEventArgs args)
        {
            Console.WriteLine($"Package {args.Package.Name} with Id: {args.Package.PackageId} was received");
        }
    }
}
