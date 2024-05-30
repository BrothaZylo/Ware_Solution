using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ware.Storages;
using Ware.ReceivingDepartments;
using Ware.Terminals;
using Ware.Persons;
using Ware.Equipments;

namespace Ware.Packages
{
    /// <summary>
    /// e
    /// </summary>
    public class PackageEventArgs : EventArgs
    {

        public Package package {  get;  set; }
        public ReceivingDepartment receivingDepartment { get;  set; }
        public Storage storage { get;  set; }
        public Terminal terminal { get;  set; }
        public string text { get;  set; }
        public string storageUniqueId { get;  set; }
        public Pallet pallet { get;  set; }
        public PalletStorage palletStorage { get;  set; }
        public Package package1 { get;  set; }
        public KittingBox box { get;  set; }
        public KittingArea kittingArea{ get;  set;}
        public PackingArea packingArea { get;  set; }
        public Equipment equipment { get;  set; }
        public Person person { get;  set; }

        public PackageEventArgs(Package packageO)
        {
            package = packageO;
        }
        public PackageEventArgs(Package packageO, string storageUniqueIdO)
        {
            package = packageO;
            storageUniqueId = storageUniqueIdO;
        }
        public PackageEventArgs(string textO)
        {
            text = textO;
        }

        public PackageEventArgs(Storage storageO)
        {
            storage = storageO;
        }
        public PackageEventArgs(Package packageO, PackingArea packingAreaO)
        {
            package = packageO;
            packingArea = packingAreaO;
        }
        public PackageEventArgs(Pallet palletO, PalletStorage palletStorageO, Person personO, Equipment equipmentO)
        {
            pallet = palletO;
            palletStorage = palletStorageO;
            person = personO;
            equipment = equipmentO;
        }

        public PackageEventArgs(Package packageO, Pallet palletO)
        {
            package = packageO;
            pallet = palletO;
        }

        public PackageEventArgs(Package packageO, ReceivingDepartment receivingDepartmentO)
        {
            package = packageO;
            receivingDepartment = receivingDepartmentO;
        }

        public PackageEventArgs(Package packageO, Storage storageO)
        {
            package = packageO;
            storage = storageO;
        }

        public PackageEventArgs(Package packageO, Terminal terminalO)
        {
            package = packageO;
            terminal = terminalO;
        }

        public PackageEventArgs(Pallet palletO, PalletStorage palletStorageO)
        {
            pallet = palletO;
            palletStorage = palletStorageO;
        }

        public PackageEventArgs(Pallet palletO, Terminal terminalO)
        {
            pallet = palletO;
            terminal = terminalO;
        }
        public PackageEventArgs(Package packageO, KittingArea kittingAreaO)
        {
            package = packageO;
            kittingArea = kittingAreaO;
        }

        public PackageEventArgs(Terminal terminalO)
        {
            terminal = terminalO;
        }
        public PackageEventArgs(Package packageO, KittingBox boxO)
        {
            package = packageO;
            box = boxO;
        }
        public PackageEventArgs(KittingBox boxO , KittingArea kittingAreaO)
        {
            box = boxO;
            kittingArea = kittingAreaO;
        }


    }
}
