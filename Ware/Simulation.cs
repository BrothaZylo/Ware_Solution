using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ware
{
    /// <summary>
    /// Simulates how the system would act with Refrigerated, Dangrous and Dry storage units.
    /// </summary>
    /// <param name="seconds">Seconds the simulation will run</param>
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

        private bool sendRecToStorage = true;
        private bool sendStorageToTerminal = true;
        private bool recPackages = true;
        private bool printStorage = true;
        private int listcounter, dry, refrigerated, dangerous;


        /// <summary>
        /// Adds packages that will run in the simulation. Only add packages with the goodtype of Refrigerated, Dangerous or Dry.
        /// </summary>
        /// <param name="package">package class object</param>
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
                    Dry.AddUnit("Autosized", 2, height + 10, width + 10);
                }
                if (goods == "Refrigerated")
                {
                    Refrigerated.AddUnit("Autosized", 2, height + 10, width + 10);
                }
                if (goods == "Dangerous")
                {
                    Dangerous.AddUnit("Autosized", 2, height + 10, width + 10);
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
                        Console.WriteLine(package.Name + " was received :O");
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
                        Console.WriteLine(package.Name + " was received xd");
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
                        Console.WriteLine(package.Name + " was received uu");
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
                        Console.WriteLine(package.Name + " was received ll");
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
                        Console.WriteLine(package.Name + " was received qq");
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
                if (Dry.IsSameTypeOfGoods(package))
                {
                Dry.MovePackageToTerminal(package,terminal);
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

        private int CalcSimTime(int instanceNumber)
        {
            int s = seconds;
            int i = instanceNumber;

            if (s > 59)
            {
                return s - s+i;
            }

            return s/s-i;
        }

        /// <summary>
        /// Starts the simulation with the added packages. 60 seconds recommended runtime.
        /// </summary>
        public void Run()
        {
            int start = 1;
            int stop = seconds;
            int delay = 1000;
            int sdelay = 4000;

            Console.WriteLine(" ---------------------");
            Console.WriteLine("| Simulation starting  |");
            Console.WriteLine("|      Loading...      |");
            Console.WriteLine(" ---------------------\n\n");

            CalcAmountGoodsTypeList();
            AddUnits();
            BuildStorages();
            listcounter += simulationPackages.Count;

            Thread.Sleep(sdelay);


            while (start != stop)
            {
                if (recPackages && start == CalcSimTime(2))
                {
                    RecievePackages();
                }

                if (sendRecToStorage && start == CalcSimTime(4))
                {
                    SendPackagesToStorage();
                    sendRecToStorage = false;
                }

                if (printStorage && start == CalcSimTime(7))
                {
                    PrintStorages();
                }

                if (sendStorageToTerminal && start == CalcSimTime(9))
                {
                    FromStorageToTerminal();
                }

                if (recPackages && start == CalcSimTime(10))
                {
                    RecievePackages();
                }

                if (recPackages && start == CalcSimTime(13))
                {
                    RecievePackages();
                }

                Console.WriteLine("\n---------------");
                Console.WriteLine("TimeStamp: "+start);
                Console.WriteLine("---------------\n");
                Thread.Sleep(delay);
                start++;
            }
            PrintStorages();
            Console.WriteLine("Simulation ended at: " + stop + " Seconds");
        }

    }
}
