using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ware.Scheduler;

namespace Ware
{
    /// <summary>
    /// Simulate the API
    /// </summary>
    public class TrueSimulation
    {
        private int runTimeInSeconds = 20;
        private List<Package> packages = new List<Package>();
        private List<Package> packagesTmp = new List<Package>();
        private List<Storage> storages = new List<Storage>();
        private List<Pallet> pallets = new List<Pallet>();
        private List<PalletStorage> palletStorages = new List<PalletStorage>();
        private List<Person> persons = new List<Person>();
        private List<Terminal> terminals = new List<Terminal>();
        private List<Equipment> equipments = new List<Equipment>();
        private List<KittingArea> kittingAreas = new List<KittingArea>();
        private List<PackingArea> packingAreas = new List<PackingArea>();
        private List<ReceivingDepartment> receivingDepartments = new List<ReceivingDepartment>();   
        private List<Schedule> schedules = new List<Schedule>();

        /// <summary>
        /// Simulate the API
        /// </summary>
        /// <param name="runTimeSeconds"></param>
        public TrueSimulation(int runTimeSeconds)
        {
            runTimeInSeconds = runTimeSeconds;
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
            packagesTmp = [];
            schedules = [];
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

        public void AddScheduleToSimulation(Schedule schedule)
        {
            schedules.Add(schedule);
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
                packagesTmp.Add(packages[0]);
                packages.Remove(packages[0]);
            }
        }

        private void SendFromReceivingToStorage()
        {
            if (packages.Count == 0)
            {
                foreach (ReceivingDepartment item in receivingDepartments)
                {
                    foreach (Package p in item.GetPackageList())
                    {
                        foreach (Storage storage in storages)
                        {
                            if (storage.GoodsType == p.Goods)
                            {
                                item.SendPackageToStorageAutomatic(p, storage);
                                Console.WriteLine(p.PackageId + " was sent to: " + storage.GetPackagePlacement(p));
                            }
                        }
                        return;
                    }
                }
            }
        }

        private void PathSelectorFromStorage()
        {
            foreach (ReceivingDepartment receivingDepartment in receivingDepartments)
            {
                if (receivingDepartment.GetPackageList().Count != 0)
                {
                    return;
                }
            }

            if (kittingAreas.Count != 0 && packages.Count == 0 && packagesTmp.Count != 0)
            {
                foreach (KittingArea area in kittingAreas)
                {
                    if(area.GetPackagesGoingToKittingArea().Count == area.GetPackagesInKittingArea().Count)
                    {
                        continue;
                    }
                    for (int i = 0; i < area.GetPackagesGoingToKittingArea().Count ; i++)
                    {
                        foreach (Package item in packagesTmp)
                        {
                            if (area.GetPackagesGoingToKittingArea()[i] == item)
                            {
                                foreach(Storage storage in storages)
                                {
                                    if (storage.GetPackage(item.PackageId) == item)
                                    {
                                        try
                                        {
                                            area.AddPackageToKittingArea(storage.MovePackage(item));
                                            Console.WriteLine($"{item.PackageId} {item.Name} was moved to {area}");
                                            packagesTmp.Remove(item);
                                            return;
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine(ex);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            if (packingAreas.Count != 0 && packages.Count == 0 && packagesTmp.Count != 0)
            {
                foreach(PackingArea packingArea in packingAreas)
                {
                    if(packingArea.GetScheduledPackagesForPackingArea().Count == packingArea.GetPackagesInPackingArea().Count)
                    {
                        continue;
                    }
                    for (int i = 0; i < packingArea.GetScheduledPackagesForPackingArea().Count ; i++)
                    {
                        foreach(Package item in packagesTmp)
                        {
                            if (packingArea.GetScheduledPackagesForPackingArea()[i] == item)
                            {
                                foreach(Storage storage in storages)
                                {
                                    if (storage.GetPackage(item.PackageId) == item)
                                    {
                                        try
                                        {
                                            packingArea.ReceivePackage(storage.MovePackage(item));
                                            Console.WriteLine($"{item.PackageId} {item.Name} was moved to {packingArea}");
                                            packagesTmp.Remove(item);
                                            return;
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine(ex);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            
            foreach(Terminal terminal in terminals)
            {
                foreach(Storage storage in storages )
                {
                    foreach(Package item in packagesTmp)
                    {
                        if (storage.GetPackage(item.PackageId) == item)
                        {
                            try
                            {
                                Package curPackage = storage.MovePackage(item);
                                if (curPackage == item)
                                {
                                    terminal.AddPackage(curPackage);
                                    Console.WriteLine($"{curPackage.PackageId} {curPackage.Name} was sent to {terminal.Name}");
                                    packagesTmp.Remove(item);
                                    return;
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex);
                            }
                        }
                    }
                }
            }
            return;
        }

        private void TerminalSendAway()
        {
            foreach (Terminal terminal in terminals)
            {
                if(terminal.GetPackagesInTerminal().Count != 0)
                {
                    Console.WriteLine($"{terminal.GetPackagesInTerminal()[0].PackageId} {terminal.GetPackagesInTerminal()[0].Name} was sent out of the {terminal.Name}");
                    terminal.SendPackage(terminal.GetPackagesInTerminal()[0]);
                }
            }
        }

        private void PackingAreaPalletCreation()
        {
            int scheduledpackages = 0;
            if(pallets.Count == 0)
            {
                return;
            }
            foreach (Pallet pallet in pallets)
            {
                if(pallet.GetScheduledPackages().Count != 0)
                {
                    scheduledpackages++;
                }
                if(pallet.GetScheduledPackages().Count == 0)
                {
                    continue;
                }
            }
            if(scheduledpackages == 0)
            {
                return;
            }

            foreach(PackingArea packingArea in packingAreas)
            {
                foreach(Pallet pallet in pallets)
                {
                    foreach(Package scheduled in pallet.GetScheduledPackages())
                    {
                        for(int i = 0; i < packingArea.GetPackagesInPackingArea().Count; i++)
                        {
                            if (scheduled == packingArea.GetPackagesInPackingArea()[i])
                            {
                                packingArea.AddToPallet(scheduled, pallet);
                                Console.WriteLine($"{scheduled.PackageId} {scheduled.Name} was placed in {pallet}");
                                return;
                            }
                        }
                    }
                }
            }
        }

        private void PersonUseForkliftPlacePallet()
        {
            foreach(Equipment equipment in equipments)
            {
                foreach(Person person in persons)
                {
                    if (equipment.HasAccess(person))
                    {
                        equipment.UseEquipment(person);
                        foreach(PalletStorage palletStorage in palletStorages)
                        {
                            foreach(Pallet pallet in pallets)
                            {
                                if(pallet.GetScheduledPackages().Count == pallet.GetPackagesOnPallet().Count)
                                {
                                    palletStorage.PlacePalletAutomatic(pallet);
                                    Console.WriteLine($"{pallet} was placed in {palletStorage.StorageName} by {person.Name} using {equipment.Name}");
                                    pallets.Remove(pallet);
                                    return;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void PalletStorageToTerminal()
        {
            foreach (Equipment equipment in equipments)
            {
                foreach (Person person in persons)
                {
                    if (equipment.HasAccess(person))
                    {
                        equipment.UseEquipment(person);
                        foreach (PalletStorage palletStorage in palletStorages)
                        {
                            foreach(Pallet pallet in pallets)
                            {
                                foreach(Terminal terminal in terminals)
                                {
                                    /*
                                    if(pallet == palletStorage.GetPallet(pallet))
                                    {
                                        palletStorage.SendPalletToTerminalAutomatic(pallet, terminal);
                                    }
                                    */
                                    //ingen implementasjon
                                    return;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void ScheduleCreation()
        {
            if (schedules.Count == 0)
            {
                return;
            }
            foreach(Schedule schedule in schedules)
            {
                //fix ;(
            }
        }

        public void Run()
        {
            int delay = 1000;
            if (!CanRunSimulation())
            {
                Console.WriteLine("Not Running");
                return;
            }

            while (runTimeInSeconds != 0)
            {
                PalletStorageToTerminal();
                PersonUseForkliftPlacePallet();
                PackingAreaPalletCreation();
                TerminalSendAway();
                //from kitting -> Terminal -()ingen implementasjon
                PathSelectorFromStorage();
                SendFromReceivingToStorage();
                ReceivePackage();
                //Schedule




                Console.WriteLine("--------------");
                Thread.Sleep(delay);
                runTimeInSeconds--;
            }
        }

    }
}
