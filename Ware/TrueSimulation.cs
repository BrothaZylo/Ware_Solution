using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ware
{
    public class TrueSimulation
    {
        private int RunTimeSeconds = 10;
        private List<Package> packages = new List<Package>();
        private List<Storage> storages = new List<Storage>();
        private List<Pallet> pallets = new List<Pallet>();
        private List<PalletStorage> palletStorages = new List<PalletStorage>();
        private List<Person> persons = new List<Person>();
        private List<Terminal> terminals = new List<Terminal>();
        private List<Equipment> equipments = new List<Equipment>();
        private List<KittingArea> kittingAreas = new List<KittingArea>();
        private List<PackingArea> packingAreas = new List<PackingArea>();

        public TrueSimulation(int runTimeSeconds)
        {
            RunTimeSeconds = runTimeSeconds;
            packages = [];
            storages = [];
            pallets = [];
            palletStorages = [];
            persons = [];
            terminals = [];
            equipments = [];
            kittingAreas = [];
            packingAreas = [];
        }

        public void AddPackageToSimulation(Package package)
        {
            packages.Add(package);
        }

        public void AddStorageToSimulation(Storage storage)
        {
            storages.Add(storage);
        }

        public void AddPalletToSimulation(Pallet pallet)
        {
            pallets.Add(pallet);
        }

        public void AddPalletStorageToSimulation(PalletStorage palletStorage)
        {
            palletStorages.Add(palletStorage);
        }

        public void AddPersonToSimulation(Person person)
        {
            persons.Add(person);
        }

        public void AddTerminalToSimulation(Terminal terminal)
        {
            terminals.Add(terminal);
        }

        public void AddEquipmentToSimulation(Equipment equipment)
        {
            equipments.Add(equipment);
        }

        public void AddKittingAreaToSimulation(KittingArea kittingArea)
        {
            kittingAreas.Add(kittingArea);
        }

        public void AddPackingAreaToSimulation(PackingArea packingArea)
        {
            packingAreas.Add(packingArea);
        }

    }
}
