using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ware.Storages;
using Ware.ReceivingDepartments;
using Ware.Terminals;

namespace Ware.Packages
{
    /// <summary>
    /// e
    /// </summary>
    public class PackageEventArgs : EventArgs
    {
        public Package Package { get; private set; }
        public ReceivingDepartment ReceivingDepartment { get; private set; }
        public Storage Storage { get; private set; }
        public Terminal Terminal { get; private set; }
        public string Text { get; private set; }
        public string StorageUniqueId { get; private set; }

        public PackageEventArgs()
        {
        }

        public PackageEventArgs(Package package, string storageUniqueId)
        {
            Package = package;
            StorageUniqueId = storageUniqueId;
        }
        public PackageEventArgs(string text)
        {
            Text = text;
        }

        public PackageEventArgs(Storage storage)
        {
            Storage = storage;
        }

        public PackageEventArgs(Package package)
        {
            Package = package;
        }

        public PackageEventArgs(Package package, ReceivingDepartment receivingDepartment)
        {
            Package = package;
            ReceivingDepartment = receivingDepartment;
        }

        public PackageEventArgs(Package package, Storage storage)
        {
            Package = package;
            Storage = storage;
        }

        public PackageEventArgs(Package package, Terminal terminal)
        {
            Package = package;
            Terminal = terminal;
        }
    }
}
