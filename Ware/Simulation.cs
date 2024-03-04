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
    /// <param packageName="seconds">Seconds the simulation will run</param>
    public class Simulation(int seconds) : ISimulation
    {
        private readonly int seconds = seconds;
        private readonly List<Package> simulationPackages = [];
        private static readonly Storage Dry = new("Dry");
        private static readonly Storage Refrigerated = new("Refrigerated");
        private static readonly Storage Dangerous = new("Dangerous");

        private readonly PackageLogging packageLogging = new();
        private readonly ReceivingDepartment receiving = new();
        private readonly Schedule schedule = new();
        private readonly Terminal terminal = new();

        private int dry, refrigerated, dangerous;


        /// <summary>
        /// Adds packages that will run in the simulation. Only add packages with the goodtype of Refrigerated, Dangerous or Dry.
        /// </summary>
        /// <param packageName="package">package class object</param>
        public void AddPackage(Package package)
        {
            simulationPackages.Add(package);
        }


        private Package GetFirstPackage()
        {
            Package first = simulationPackages[0];
            simulationPackages.RemoveAt(0);
            return first;
        }

        private void AddUnits()
        {
            for (int index = 0; index < simulationPackages.Count; index++)
            {
                double height = simulationPackages[index].Height;
                double width = simulationPackages[index].Width;
                string goods = simulationPackages[index].Goods;

                if (goods == "Dry")
                {
                    Dry.AddUnit("Autosized", 3, height + 10, width + 10);
                }
                if (goods == "Refrigerated")
                {
                    Refrigerated.AddUnit("Autosized", 3, height + 10, width + 10);
                }
                if (goods == "Dangerous")
                {
                    Dangerous.AddUnit("Autosized", 3, height + 10, width + 10);
                }
            }
        }

        private static void BuildStorages()
        {
            Dry.Build();
            Refrigerated.Build();
            Dangerous.Build();
        }

        private void CalcAmountGoodsTypeList()
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

        private void RecievePackages()
        {
            if (dry > refrigerated && dry > dangerous && dry != 0)
            {
                foreach(Package package in simulationPackages)
                {
                    if (package.Goods == "Dry")
                    {
                        receiving.AddPackage(package);
                        Console.WriteLine(package.Name + " was received");
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
                        Console.WriteLine(package.Name + " was received");
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
                        Console.WriteLine(package.Name + " was received");
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
                        Console.WriteLine(package.Name + " was received");
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
                        Console.WriteLine(package.Name + " was received");
                    }
                }
                refrigerated = 0;
            }
        }

        private void SendPackagesToStorage()
        {
            receiving.SendAllPackagesToStorage(Dry);
            receiving.SendAllPackagesToStorage(Dangerous);
            receiving.SendAllPackagesToStorage(Refrigerated);
            Console.WriteLine("Packages was sent from the the receiving department to storage");
        }


        private void FromStorageToTerminal()
        {

            foreach (Package package in simulationPackages)
            {
                try
                {
                    if (Dry.IsSameTypeOfGoods(package))
                    {
                        Dry.MovePackageToTerminal(package, terminal);
                    }
                    if (Refrigerated.IsSameTypeOfGoods(package))
                    {
                        Refrigerated.MovePackageToTerminal(package, terminal);
                    }
                    if (Dangerous.IsSameTypeOfGoods(package))
                    {
                        Dangerous.MovePackageToTerminal(package, terminal);
                    }
                }
                catch (PackageInvalidException e)
                {
                    Console.WriteLine(e);
                }

            }
            Console.WriteLine("Packages was sent from the the storage to the terminal");
        }
        private static void PrintStorages()
        {
            Dry.GetAllStorageInformationPrint();
            Console.WriteLine();
            Refrigerated.GetAllStorageInformationPrint();
            Console.WriteLine();
            Dangerous.GetAllStorageInformationPrint();
        }


        private static double TravelTime()
        {
            double time = Dry.TimeDeliveryToStorageSeconds;
            return time;
        }


        private int CalcSimTime(int percentageNumber)
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
            foreach(Package package in simulationPackages)
            {
                Random rand = new();
                int u = rand.Next(0, 30);
                schedule.AddPackage("single", DayOfWeek.Monday, package, AutomaticTimeCreator(u));
            }
        }


        /// <summary>
        /// Starts the simulation with the added packages. 60 seconds recommended runtime.
        /// </summary>
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

            CalcAmountGoodsTypeList();
            AddUnits();
            BuildStorages();
            CreateSchedule();

            Thread.Sleep(startDelay);


            while (start != stop)
            {
                if (start == CalcSimTime(5))
                {
                    schedule.GetSchedule();
                }
                if (start == CalcSimTime(10))
                {
                    RecievePackages();
                }

                if (start == CalcSimTime(20))
                {
                    SendPackagesToStorage();
                }

                if (start == CalcSimTime(30))
                {
                    PrintStorages();
                }

                if (start == CalcSimTime(40))
                {
                    FromStorageToTerminal();
                }

                if (start == CalcSimTime(11))
                {
                    RecievePackages();
                }

                if (start == CalcSimTime(11))
                {
                    RecievePackages();
                }
                if (start == CalcSimTime(99)) 
                { 
                    RecievePackages(); 
                }

                Console.WriteLine("\n---------------");
                Console.WriteLine("TimeStamp: "+start);
                Console.WriteLine("---------------\n");
                Thread.Sleep(delay);
                start++;
            }
            Console.WriteLine("Simulation ended at: " + stop + " Seconds");
        }

    }
}
