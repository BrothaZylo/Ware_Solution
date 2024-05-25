using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ware.PackageLogs;
using Ware.Persons;
using Ware.Storages;
using Ware.Packages;
using Ware.Schedules;
using Ware.Equipments;

namespace Ware
{
    /// <summary>
    /// Events for when something is created
    /// </summary>
    public class ObjectCreationEventArgs
    {
        /// <summary>
        /// Storage object
        /// </summary>
        public Storage? Storage { get; private set; }

        /// <summary>
        /// Pallet object
        /// </summary>
        public Pallet? Pallet { get; private set; }

        /// <summary>
        /// PalletStorage object
        /// </summary>
        public PalletStorage? PalletStorage { get; private set; }

        /// <summary>
        /// Package object
        /// </summary>
        public Package? Package { get; private set; }

        /// <summary>
        /// Person object
        /// </summary>
        public Person? Person { get; private set; }

        /// <summary>
        /// Aisle object
        /// </summary>
        public Aisle? Aisle { get; private set; }

        /// <summary>
        /// Assignment object
        /// </summary>
        public Assignment? Assignment { get; private set; }

        /// <summary>
        /// Packagelogging object
        /// </summary>
        public PackageLogging? PackageLogging { get; private set; }

        /// <summary>
        /// Schedule object
        /// </summary>
        public Schedule? Schedule { get; private set; }

        /// <summary>
        /// Equipment object
        /// </summary>
        public Equipment? Equipment { get; private set; }


        /// <summary>
        /// Empty ObjectCreationEventArgs
        /// </summary>
        public ObjectCreationEventArgs()
        {
        }

        /// <summary>
        /// ObjectCreationEventArgs containing Equipment object
        /// </summary>
        /// <param name="equipment"></param>
        public ObjectCreationEventArgs(Equipment? equipment)
        {
            Equipment = equipment;
        }

        /// <summary>
        /// ObjectCreationEventArgs containing storage object
        /// </summary>
        /// <param name="storage">Storage object</param>
        public ObjectCreationEventArgs(Storage? storage)
        {
            Storage = storage;
        }

        /// <summary>
        /// ObjectCreationEventArgs containing pallet object
        /// </summary>
        /// <param name="pallet">Pallet object</param>
        public ObjectCreationEventArgs(Pallet? pallet)
        {
            Pallet = pallet;
        }

        /// <summary>
        /// ObjectCreationEventArgs containing PalletStorag object
        /// </summary>
        /// <param name="palletStorage">PalletStorage object</param>
        public ObjectCreationEventArgs(PalletStorage? palletStorage)
        {
            PalletStorage = palletStorage;
        }

        /// <summary>
        /// ObjectCreationEventArgs containing Package object
        /// </summary>
        /// <param name="package">Package object</param>
        public ObjectCreationEventArgs(Package? package)
        {
            Package = package;
        }

        /// <summary>
        /// ObjectCreationEventArgs containing Schedule object
        /// </summary>
        /// <param name="schedule">Schedule object</param>
        public ObjectCreationEventArgs(Schedule? schedule)
        {
            Schedule = schedule;
        }

        /// <summary>
        /// ObjectCreationEventArgs containing PackageLogging object
        /// </summary>
        /// <param name="packageLogging">PackageLogging object</param>
        public ObjectCreationEventArgs(PackageLogging? packageLogging)
        {
            PackageLogging = packageLogging;
        }

        /// <summary>
        /// ObjectCreationEventArgs containing Person object
        /// </summary>
        /// <param name="person">Person object</param>
        public ObjectCreationEventArgs(Person? person)
        {
            Person = person;
        }

        /// <summary>
        /// ObjectCreationEventArgs containing Aisle object
        /// </summary>
        /// <param name="aisle">Aisle object</param>
        public ObjectCreationEventArgs(Aisle? aisle)
        {
            Aisle = aisle;
        }

        /// <summary>
        /// ObjectCreationEventArgs containing Assignment object
        /// </summary>
        /// <param name="assignment">Assignment object</param>
        public ObjectCreationEventArgs(Assignment? assignment)
        {
            Assignment = assignment;
        }
    }
}
