using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ware.Packages;
using Ware.PalletStorages;

namespace Ware.Storages
{
    public class StorageEventArgs : EventArgs
    {
        public Storage Storage { get; private set; }
        public Package Package { get; private set; }
        public PalletStorage PalletStorage { get; private set; }
        StorageEventArgs()
        {

        }

        public StorageEventArgs(Storage storage)
        {
            Storage = storage;
        }
        public StorageEventArgs(Storage storage, Package package)
        {
            Storage = storage;
            Package = package;
        }

        public StorageEventArgs(PalletStorage palletStorage)
        {
            PalletStorage = palletStorage;
        }
    }
}
