using System;
using System.Collections.Generic;
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

        private readonly PackageLogging packageLogging = new PackageLogging();
        private readonly ReceivingDepartment receiving = new ReceivingDepartment();
        private readonly Schedule schedule = new Schedule();
        private readonly Terminal terminal = new Terminal();


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
            }
        }

        private void SendDryPackagesToStorage()
        {
            NewDelay();
            receiving.SendAllPackagesToStorage(Dry);
            Console.WriteLine("Dry");
        }
        private void SendRefrigiratedPackagesToStorage()
        {
            NewDelay();
            receiving.SendAllPackagesToStorage(Refrigerated);
            Console.WriteLine("Refrigirated");
        }
        private void SendDangerousPackagesToStorage()
        {
            NewDelay();
            receiving.SendAllPackagesToStorage(Dangerous);
            Console.WriteLine("Dangerous");
        }

        private void SendPackagesToStorage()
        {
            SendDryPackagesToStorage();
            SendRefrigiratedPackagesToStorage();
            SendDangerousPackagesToStorage();
        }

        /// <summary>
        /// 
        /// </summary>
        /// Jin Vincent Necesario. “Task Parallel Library 101 Using C#.” C-Sharpcorner.com, 2023,
        /// www.c-sharpcorner.com/article/task-parallel-library-101-using-c-sharp/. 
        private void NewDelay()
        {
            var action = new Action(() =>
            {
                CalculateDelay();
                double timeMs = TravelTime() * 1000;
                Task.Delay((int)timeMs);
                Console.WriteLine("Hello Task World");

            });

            Task myTask = new Task(action);
            myTask.Start();

            myTask.Wait();
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

        private double TravelTime()
        {
            double time = Dry.TimeDeliveryToStorageSeconds;
            return time;
        }

        /// <summary>
        /// Starts the simulation with the added packages
        /// </summary>
        public void Run()
        {
            int start = 1;
            int stop = seconds;
            int delay = 1000;

            AddUnits();
            BuildStorages();
           
            RecievePackages();
            //SendPackagesToStorage();
            FromStorageToTerminal();
            

            while (start != stop)
            {
                Console.WriteLine(start);
                Thread.Sleep(delay);
                if (start == 1)
                {
                    SendPackagesToStorage();
                }
                


                start++;
            }



            //Dry.GetAllStorageInformationPrint();
            Console.WriteLine();
            //Refrigerated.GetAllStorageInformationPrint();
            Console.WriteLine();
            //Dangrous.GetAllStorageInformationPrint();

            Console.WriteLine("Simulation ended at: " + stop + " Seconds");
        }

    }
}
