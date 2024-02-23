using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ware
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="seconds"></param>
    public class Simulation(int seconds) : ISimulation
    {
        private readonly int seconds = seconds;
        private readonly List<Package> simulationPackages = [];


        /// <summary>
        /// Adds packages that will run in the simulation
        /// </summary>
        /// <param name="package"></param>
        public void Add(Package package)
        {
            simulationPackages.Add(package);
        }

        /// <summary>
        /// Starts the simulation with the added packages
        /// </summary>
        public void Run()
        {
            Console.WriteLine("Sim start");
        }

    }
}
