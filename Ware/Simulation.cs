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
    /// <param name="seconds"></param>
    public class Simulation(int seconds) : ISimulation
    {
        private readonly int seconds = seconds;
        private readonly List<Package> simulationPackages = [];
        private readonly Storage Dry = new("Dry");
        private readonly Storage Refrigerated = new("Refrigerated");
        private readonly Storage Dangrous = new("Dangerous");


        /// <summary>
        /// Adds packages that will run in the simulation. Only add packages with the goodtype of Refrigerated, Dangerous or Dry.
        /// </summary>
        /// <param name="package"></param>
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
                    Dangrous.AddUnit("Autosized", 2, height + 10, width + 10);
                }
            }
        }

        private void BuildStorages()
        {
            Dry.Build();
            Refrigerated.Build();
            Dangrous.Build();
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


            while (start != stop)
            {
                Console.WriteLine(start);
                Thread.Sleep(delay);
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
