using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ware
{
    public class Warehouse : IWarehouse
    {
        public List<Package> packages = new List<Package>();
        public List<Storage> storages = new List<Storage>();

        public void AddPackage(Package package)
        {
            packages.Add(package);
        }

        public void AddStorage(Storage storage)
        {
            storages.Add(storage);
        }
        public List<Package> GetPackages()
        {
            return new List<Package>(packages);
        }

        public List<Storage> GetStorages()
        {
            return new List<Storage>(storages);
        }
    }
}
