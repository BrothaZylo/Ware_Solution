using System;
using System.Collections.Generic;

namespace Ware
{
    public class ReceivingDepartment
    {
        private List<CreatePackage> receivedPackages;
        private StorageConfiguration warehouse;

        public ReceivingDepartment(StorageConfiguration warehouse)
        {
            this.warehouse = warehouse;
            receivedPackages = new List<CreatePackage>();
        }

        public void ReceivePackage(CreatePackage package)
        {
            receivedPackages.Add(package);
        }

        public List<string> SendPackagesToWarehouse()
        {
            List<string> results = new List<string>();
            foreach (var package in receivedPackages)
            {
                string result = warehouse.PlacePackage(package);
                int transferTime = GetTransferTime(package.goods);
                result += $" Overføringstid: {transferTime} time(s).";
                results.Add(result);
            }

            receivedPackages.Clear();
            return results;
        }

        public int GetTransferTime(string goodsType)
        {
            int transferTime = 0;
            switch (goodsType.ToLower())
            {
                case "frysevarer":
                    transferTime = 2;
                    break;
                default:
                    transferTime = 1;
                    break;
            }
            return transferTime;
        }
    }
}