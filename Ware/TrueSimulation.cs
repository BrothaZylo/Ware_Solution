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
        private List<ReceivingDepartment> receivingDepartments = new List<ReceivingDepartment>();   

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
            receivingDepartments = [];
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

        public void AddReceivingDepartmentToSimulation(ReceivingDepartment receivingDepartment)
        {
            receivingDepartments.Add(receivingDepartment);
        }

        private bool CanRunSimulation()
        {
            if(packages.Count == 0 || storages.Count == 0 || terminals.Count == 0 || receivingDepartments.Count == 0)
            {
                return false;
            }
            return true;
        }

        private void ReceivePackage()
        {
            for (int i = 0; i < receivingDepartments.Count; i++)
            {
                if(packages.Count == 0)
                {
                    return;
                }
                Console.WriteLine("" + receivingDepartments[i].Name + " received: " + packages[0].PackageId);
                receivingDepartments[i].AddPackage(packages[0]);
                packages.Remove(packages[0]);
            }
        }

        private void SendFromReceivingToStorage()
        {
            foreach (ReceivingDepartment item in receivingDepartments)
            {
                foreach(Package p in item.GetPackageList())
                {
                    foreach(Storage storage in storages)
                    {
                        if(storage.GoodsType == p.Goods)
                        {
                            item.SendPackageToStorageAutomatic(p, storage);
                            Console.WriteLine(p.PackageId + " was sent to: " + storage.UniqueId);
                        }
                    }
                    return;
                }
            }
        }

        private void PathSelectorFromStorage()
        {
            //Terminal, kitting, packing
            return;
        }

        public void Run()
        {
            int delay = 1000;
            if (!CanRunSimulation())
            {
                Console.WriteLine("Not Running");
                return;
            }

            while (RunTimeSeconds != 0)
            {

                ReceivePackage();
                SendFromReceivingToStorage();
                PathSelectorFromStorage();


                Console.WriteLine("--------------");
                Thread.Sleep(delay);
                RunTimeSeconds--;
            }
        }

    }
}
