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

        private void BuildStorages()
        {
            Dry.Build();
            Refrigerated.Build();
            Dangerous.Build();
        }

        private void RecievePackages()
        {
            foreach(Package package in simulationPackages)
            {
                receiving.AddPackage(package);
                Console.WriteLine(package.Name + " was received");
            }
        }

        private void SendDryPackagesToStorage()
        {
            receiving.SendAllPackagesToStorage(Dry);
        }
        private void SendRefrigiratedPackagesToStorage()
        {
            receiving.SendAllPackagesToStorage(Refrigerated);
        }
        private void SendDangerousPackagesToStorage()
        {
            receiving.SendAllPackagesToStorage(Dangerous);
        }

        private void SendPackagesToStorage()
        {
            SendDryPackagesToStorage();
            SendRefrigiratedPackagesToStorage();
            SendDangerousPackagesToStorage();
            Console.WriteLine("Fra mottak til storage");
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
            Console.WriteLine("From Storage to Terminal");
        }
        private static void PrintStorages()
        {
            Dry.GetAllStorageInformationPrint();
            Console.WriteLine();
            Refrigerated.GetAllStorageInformationPrint();
            Console.WriteLine();
            Dangerous.GetAllStorageInformationPrint();
        }

        private void CalculateDelay()
        {
            double enSmartKalkulasjon = seconds / 3;

            Dry.TimeDeliveryToStorageSeconds = enSmartKalkulasjon;
            Refrigerated.TimeDeliveryToStorageSeconds = enSmartKalkulasjon;
            Dangerous.TimeDeliveryToStorageSeconds = enSmartKalkulasjon;

            Dry.TimeStorageToTerminalSeconds = enSmartKalkulasjon;
            Refrigerated.TimeStorageToTerminalSeconds = enSmartKalkulasjon;
            Dangerous.TimeStorageToTerminalSeconds = enSmartKalkulasjon;
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

            AddUnits();
            BuildStorages();
           
            //SendPackagesToStorage();
            //FromStorageToTerminal();
            

            while (start != stop)
            {
                if (recPackages)
                {
                    RecievePackages();
                    recPackages = false;
                }

                if (sendRecToStorage && start == CalcSimTime(1))
                {
                    SendPackagesToStorage();
                    sendRecToStorage = false;
                }

                if(sendStorageToTerminal && start == CalcSimTime(2))
                {
                    FromStorageToTerminal();
                }

                Console.WriteLine(start);
                Thread.Sleep(delay);
                start++;
            }
            PrintStorages();
            Console.WriteLine("Simulation ended at: " + stop + " Seconds");
        }

    }
}
