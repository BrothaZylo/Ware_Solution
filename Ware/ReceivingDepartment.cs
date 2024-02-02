using System;
using System.Collections.Generic;

namespace Ware
{
    public class ReceivingDepartment
    {
        private List<CreatePackage> receivedPackages;
        private StorageConfiguration storageConfiguration;

        public ReceivingDepartment(StorageConfiguration warehouse)
        {
            storageConfiguration = warehouse;
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
                string result = storageConfiguration.PlacePackage(package);
                int timeDeliveryToStorage = storageConfiguration.GetTimeDeliveryToStorage();
                int timeStorageToTerminal = storageConfiguration.GetTimeStorageToTerminal();
                result += $" Tid fra mottak til lager: {timeDeliveryToStorage} minutter.";
                result += $" Tid fra lager til terminal: {timeStorageToTerminal} minutter.";
                results.Add(result);
            }

            receivedPackages.Clear();
            return results;
        }
    }
}