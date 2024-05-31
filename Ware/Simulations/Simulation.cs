using Ware.Packages;
using Ware.Schedules;
using Ware.Storages;
using Ware.Persons;
using Ware.Equipments;
using Ware.ReceivingDepartments;
using Ware.Terminals;

namespace Ware.Simulations
{
    /// <summary>
    /// Simulate the API
    /// </summary>
    public class Simulation : ISimulation
    {
        private int runTimeInSeconds = 20;
        private List<Package> packages = new List<Package>();
        private List<Package> packagesTmp = new List<Package>();
        private List<Storage> storages = new List<Storage>();
        private List<Pallet> pallets = new List<Pallet>();
        private List<Pallet> palletsTmp = new List<Pallet>();
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
        public Simulation(int runTimeSeconds)
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
            palletsTmp = [];
        }

        /// <summary>
        /// Adds a package to the simulation
        /// </summary>
        /// <param name="package">Package object</param>
        public void AddPackageToSimulation(Package package)
        {
                packages.Add(package);
        }

        /// <summary>
        /// Adds a Storage to the simulation
        /// </summary>
        /// <param name="storage">Storage object</param>
        public void AddStorageToSimulation(Storage storage)
        {
            storages.Add(storage);
        }

        /// <summary>
        /// Adds a Pallet to the simulation
        /// </summary>
        /// <param name="pallet">Pallet object</param>
        public void AddPalletToSimulation(Pallet pallet)
        {
            pallets.Add(pallet);
        }

        /// <summary>
        /// Adds a PalletStorage to the simulation
        /// </summary>
        /// <param name="palletStorage">PalletStorage object</param>
        public void AddPalletStorageToSimulation(PalletStorage palletStorage)
        {
            palletStorages.Add(palletStorage);
        }

        /// <summary>
        /// Adds a Person to the simulation
        /// </summary>
        /// <param name="person">Person object</param>
        public void AddPersonToSimulation(Person person)
        {
            persons.Add(person);
        }

        /// <summary>
        /// Adds a Terminal to the simulation
        /// </summary>
        /// <param name="terminal">Terminal object</param>
        public void AddTerminalToSimulation(Terminal terminal)
        {
            terminals.Add(terminal);
        }

        /// <summary>
        /// Adds an Equipment to the simulation
        /// </summary>
        /// <param name="equipmentForklift">Equipment Object</param>
        public void AddEquipmentToSimulation(Equipment equipmentForklift)
        {
            equipments.Add(equipmentForklift);
        }

        /// <summary>
        /// Adds a KittingArea to the simulation
        /// </summary>
        /// <param name="kittingArea">KittingArea object</param>
        public void AddKittingAreaToSimulation(KittingArea kittingArea)
        {
            kittingAreas.Add(kittingArea);
        }

        /// <summary>
        /// Adds a PackingArea to the simulation
        /// </summary>
        /// <param name="packingArea">PackingArea object</param>
        public void AddPackingAreaToSimulation(PackingArea packingArea)
        {
            packingAreas.Add(packingArea);
        }

        /// <summary>
        /// Adds a ReceivingDepartment to the simulation
        /// </summary>
        /// <param name="receivingDepartment">ReceivingDepartment object</param>
        public void AddReceivingDepartmentToSimulation(ReceivingDepartment receivingDepartment)
        {
            receivingDepartments.Add(receivingDepartment);
        }

        /// <summary>
        /// Adds a Schedule to the simulation
        /// </summary>
        /// <param name="schedule">schedule object</param>
        public void AddScheduleToSimulation(Schedule schedule)
        {
            schedules.Add(schedule);
        }

        /// <summary>
        /// Run the simulation after setup
        /// </summary>
        public void Run()
        {
            int delay = 10;
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
                KittingAreaToTerminal();
                PathSelectorFromStorage();
                SendFromReceivingToStorage();
                ReceivePackage();

                Console.WriteLine("--------------");
                Thread.Sleep(delay);
                runTimeInSeconds--;
            }
        }

        public event EventHandler<PackageEventArgs>? ReceivedPackageEvent;

        public event EventHandler<PackageEventArgs>? SendToStoragePackageEvent;

        public event EventHandler<PackageEventArgs>? AddToKittingAreaPackageEvent;

        public event EventHandler<PackageEventArgs>? AddToPackingAreaEvent;

        public event EventHandler<PackageEventArgs>? SendPackageToTerminalEvent;

        public event EventHandler<PackageEventArgs>? SendPackageOutTerminalEvent;

        public event EventHandler<PackageEventArgs>? PlacePackageOntoPalletEvent;

        public event EventHandler<PackageEventArgs>? SendPalletToPalletStorageEvent;

        public event EventHandler<PackageEventArgs>? SendPalletToPalletToTerminalEvent;

        public event EventHandler<PackageEventArgs>? BoxCreatedInKittingAreaEvent;

        public event EventHandler<PackageEventArgs>? PackagedAddedInBoxEvent;

        public event EventHandler<PackageEventArgs>? SendBoxToTerminalEvent;

        private bool CanRunSimulation()
        {
            if (packages.Count == 0 || storages.Count == 0 || terminals.Count == 0 || receivingDepartments.Count == 0)
            {
                return false;
            }
            return true;
        }

        private void ReceivePackage()
        {
            for (int i = 0; i < receivingDepartments.Count; i++)
            {
                if (packages.Count == 0)
                {
                    return;
                }
                //Console.WriteLine("" + receivingDepartments[i].Name + " received: " + packages[0].PackageId);
                receivingDepartments[i].AddPackage(packages[0]);
                RaiseReceivedPackageEvent(packages[0], receivingDepartments[i]);
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
                                RaisedSendToStoragePackageEvent(p, storage);
                                //Console.WriteLine(p.PackageId + " was sent to: " + storage.GetPackagePlacement(p));
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
                    if (area.GetPackagesGoingToKittingArea().Count == area.GetPackagesInKittingArea().Count)
                    {
                        continue;
                    }
                    for (int i = 0; i < area.GetPackagesGoingToKittingArea().Count; i++)
                    {
                        foreach (Package item in packagesTmp)
                        {
                            if (area.GetPackagesGoingToKittingArea()[i] == item)
                            {
                                foreach (Storage storage in storages)
                                {
                                    if (storage.GetPackage(item.PackageId) == item)
                                    {
                                        try
                                        {
                                            area.AddPackageToKittingArea(storage.MovePackage(item));
                                            RaisedAddToKittingAreaPackageEvent(item,area);
                                            //Console.WriteLine($"{item.PackageId} {item.Name} was moved to {area.KittingName}");
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
                foreach (PackingArea packingArea in packingAreas)
                {
                    if (packingArea.GetScheduledPackagesForPackingArea().Count == packingArea.GetPackagesInPackingArea().Count)
                    {
                        continue;
                    }
                    for (int i = 0; i < packingArea.GetScheduledPackagesForPackingArea().Count; i++)
                    {
                        foreach (Package item in packagesTmp)
                        {
                            if (packingArea.GetScheduledPackagesForPackingArea()[i] == item)
                            {
                                foreach (Storage storage in storages)
                                {
                                    if (storage.GetPackage(item.PackageId) == item)
                                    {
                                        try
                                        {
                                            packingArea.SendPackageToPackingArea(storage.MovePackage(item));
                                            RaisedAddToPackingAreaEvent(item, packingArea);
                                            //Console.WriteLine($"{item.PackageId} {item.Name} was moved to {packingArea.AreaName}");
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

            foreach (Terminal terminal in terminals)
            {
                foreach (Storage storage in storages)
                {
                    foreach (Package item in packagesTmp)
                    {
                        if (storage.GetPackage(item.PackageId) == item)
                        {
                            try
                            {
                                Package curPackage = storage.MovePackage(item);
                                if (curPackage == item)
                                {
                                    terminal.AddPackage(curPackage);
                                    RaisedSendPackageToTerminalEvent(curPackage,terminal);
                                    //Console.WriteLine($"{curPackage.PackageId} {curPackage.Name} was sent to {terminal.Name}");
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
                if (terminal.GetPackagesInTerminal().Count != 0)
                {
                    //Console.WriteLine($"{terminal.GetPackagesInTerminal()[0].PackageId} {terminal.GetPackagesInTerminal()[0].Name} was sent out of the {terminal.Name}");
                    Package packageToSendOut = terminal.SendPackage(terminal.GetPackagesInTerminal()[0]);
                    RaisedSendPackageOutTerminalEventRaised(packageToSendOut,terminal);
                }
            }
        }

        private void PackingAreaPalletCreation()
        {
            int scheduledpackages = 0;
            if (pallets.Count == 0)
            {
                return;
            }
            foreach (Pallet pallet in pallets)
            {
                if (pallet.GetScheduledPackages().Count != 0)
                {
                    scheduledpackages++;
                }
                if (pallet.GetScheduledPackages().Count == 0)
                {
                    continue;
                }
            }
            if (scheduledpackages == 0)
            {
                return;
            }

            foreach (PackingArea packingArea in packingAreas)
            {
                foreach (Pallet pallet in pallets)
                {
                    foreach (Package scheduled in pallet.GetScheduledPackages())
                    {
                        for (int i = 0; i < packingArea.GetPackagesInPackingArea().Count; i++)
                        {
                            if (scheduled == packingArea.GetPackagesInPackingArea()[i])
                            {
                                packingArea.AddPackageOnPallet(scheduled, pallet);
                                RaisedPlacePackageOntoPalletEvent(scheduled, pallet);
                                //Console.WriteLine($"{scheduled.PackageId} {scheduled.Name} was placed in {pallet.PalletName}");
                                return;
                            }
                        }
                    }
                }
            }
        }

        private void PersonUseForkliftPlacePallet()
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
                            foreach (Pallet pallet in pallets)
                            {
                                if (pallet.GetScheduledPackages().Count == pallet.GetPackagesOnPallet().Count)
                                {
                                    palletStorage.PlacePalletAutomatic(pallet);
                                    RaisedSendPalletToPalletStorageEvent(pallet, palletStorage, person, equipment);

                                    //Console.WriteLine($"{pallet.PalletName} was placed in {palletStorage.StorageName} by {person.Name} using {equipment.Name}");
                                    palletsTmp.Add(pallet);
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
                            foreach (Pallet pallet in palletsTmp)
                            {
                                foreach (Terminal terminal in terminals)
                                {
                                    if (pallet == palletStorage.GetPallet(pallet))
                                    {
                                        //Console.WriteLine(pallet.PalletName + " was sent to " + terminal.Name + " by " + person.Name + " using " + equipment.Name);
                                        palletStorage.SendPalletToTerminal(pallet, terminal);
                                        RaisedSendPalletFromPalletStorageToTerminalEvent(pallet, terminal);
                                    }
                                    return;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void KittingAreaToTerminal()
        {
            foreach (KittingArea kit in kittingAreas)
            {
                foreach (Package p in kit.GetPackagesInKittingArea())
                {
                    if (kit.GetPackagesGoingToKittingArea().Count != kit.GetPackagesInKittingArea().Count)
                    {
                        if (kit.GetKittingBoxesInKittingArea().Count == 0)
                        {
                            kit.CreateKittingBox("Kitting Box ", "box", 50, 50);
                            RaisedBoxCreatedInKittingAreaEvent(kit,kit.GetKittingBoxesInKittingArea()[0]) ;
                            //Console.WriteLine("A new box (" + kit.GetKittingBoxesInKittingArea()[0].PackageId + ") was created in " + kit.KittingName);
                            //Console.WriteLine(p.Name + " was put in a box (" + kit.GetKittingBoxesInKittingArea()[0].PackageId + ")");
                            kit.AddPackageToKittingBox(kit.GetKittingBoxesInKittingArea()[0], p);
                            RaisedPackagedAddedInBoxEvent(kit.GetKittingBoxesInKittingArea()[0], p);
                            return;
                        }
                        //Console.WriteLine(p.Name + " was put in a box (" + kit.GetKittingBoxesInKittingArea()[0].PackageId + ")");
                        kit.AddPackageToKittingBox(kit.GetKittingBoxesInKittingArea()[0], p);
                        RaisedPackagedAddedInBoxEvent(kit.GetKittingBoxesInKittingArea()[0], p);

                    }
                    
                    foreach (Terminal terminal in terminals)
                    {
                        if (kit.GetKittingBoxesInKittingArea().Count == 0)
                        {
                            return;
                        }
                        //Console.WriteLine(kit.GetKittingBoxesInKittingArea()[0].PackageId + " was sent to " + terminal.Name);
                        if(kit.GetKittingBoxesInKittingArea()[0] != null)
                        {
                            RaisedSendBoxToTerminalEvent(terminal, kit.GetKittingBoxesInKittingArea()[0]);
                            terminal.AddPackage(kit.SendBox(kit.GetKittingBoxesInKittingArea()[0]));
                            
                        }

                        return;
                    }
                }
            }
        }

        private void RaiseReceivedPackageEvent(Package package, ReceivingDepartment receivingDepartment)
        {
            ReceivedPackageEvent?.Invoke(this, new PackageEventArgs(package, receivingDepartment));
        }

        private void RaisedSendToStoragePackageEvent(Package package, Storage storage)
        {
            SendToStoragePackageEvent?.Invoke(this, new PackageEventArgs(package, storage));
        }

        private void RaisedAddToKittingAreaPackageEvent(Package package, KittingArea kittinarea)
        {
            AddToKittingAreaPackageEvent?.Invoke(this, new PackageEventArgs(package, kittinarea));
        }

        private void RaisedAddToPackingAreaEvent(Package package, PackingArea packingArea)
        {
            AddToPackingAreaEvent?.Invoke(this, new PackageEventArgs(package, packingArea));
        }
        private void RaisedSendPackageToTerminalEvent(Package package, Terminal terminal)
        {
            SendPackageToTerminalEvent?.Invoke(this, new PackageEventArgs(package, terminal));
        }

        private void RaisedSendPackageOutTerminalEventRaised(Package package,Terminal terminal)
        {
            SendPackageOutTerminalEvent?.Invoke(this, new PackageEventArgs(package, terminal));
        }

        private void RaisedPlacePackageOntoPalletEvent(Package package, Pallet pallet)
        {
            PlacePackageOntoPalletEvent?.Invoke(this, new PackageEventArgs(package, pallet));
        }

        private void RaisedSendPalletToPalletStorageEvent(Pallet pallet, PalletStorage palletStorage, Person person, Equipment equipment)
        {
            SendPalletToPalletStorageEvent?.Invoke(this, new PackageEventArgs(pallet, palletStorage, person, equipment));
        }

        private void RaisedSendPalletFromPalletStorageToTerminalEvent(Pallet pallet, Terminal terminal)
        {
            SendPalletToPalletToTerminalEvent?.Invoke(this, new PackageEventArgs(pallet, terminal));
        }

        private void RaisedBoxCreatedInKittingAreaEvent(KittingArea kittingArea, KittingBox box)
        {
            BoxCreatedInKittingAreaEvent?.Invoke(this, new PackageEventArgs(box, kittingArea));
        }

        private void RaisedPackagedAddedInBoxEvent(KittingBox box, Package package)
        {
            PackagedAddedInBoxEvent?.Invoke(this, new PackageEventArgs(package,box ));
        }

        private void RaisedSendBoxToTerminalEvent(Terminal terminal, Package box)
        {
            SendBoxToTerminalEvent?.Invoke(this, new PackageEventArgs(box,terminal ));
        }

    }
}
