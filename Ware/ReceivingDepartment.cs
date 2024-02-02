using System;
using System.Collections.Generic;

namespace Ware
{
    /// <summary>
    /// Her håndteres mottak av pakkene og tidene det tar.
    /// </summary>
    public class ReceivingDepartment
    {
        private List<CreatePackage> receivedPackages;
        private StorageConfiguration storageConfiguration;

        public ReceivingDepartment(StorageConfiguration warehouse)
        {
            storageConfiguration = warehouse;
            receivedPackages = new List<CreatePackage>();
        }

        /// <summary>
        /// Pakke mottas og legges in i listen til mottate pakker.
        /// </summary>
        /// <param name="package">Navnet på pakken som mottas.</param>
        public void ReceivePackage(CreatePackage package)
        {
            receivedPackages.Add(package);
        }

        /// <summary>
        /// Pakkene sendes til lagerhuset og returnerer en liste til pakkenes resultater.
        /// </summary>
        /// <returns>Liste med strenger som hviser resultatet til plassering av hver pakke i lagerhuset.</returns>
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