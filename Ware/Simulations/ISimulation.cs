using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ware.Equipments;
using Ware.KittingAreas;
using Ware.Packages;
using Ware.PackingAreas;
using Ware.Pallets;
using Ware.PalletStorages;
using Ware.Persons;
using Ware.ReceivingDepartments;
using Ware.Schedules;
using Ware.Storages;
using Ware.Terminals;

namespace Ware.Simulations
{
    internal interface ISimulation
    {
        void AddPackageToSimulation(Package package);
        void AddStorageToSimulation(Storage storage);
        void AddPalletToSimulation(Pallet pallet);
        void AddPalletStorageToSimulation(PalletStorage palletStorage);
        void AddPersonToSimulation(Person person);
        void AddTerminalToSimulation(Terminal terminal);
        void AddEquipmentToSimulation(Equipment equipment);
        void AddKittingAreaToSimulation(KittingArea kittingArea);
        void AddPackingAreaToSimulation(PackingArea packingArea);
        void AddReceivingDepartmentToSimulation(ReceivingDepartment receivingDepartment);
        void AddScheduleToSimulation(Schedule schedule);
        void Run();
    }
}
