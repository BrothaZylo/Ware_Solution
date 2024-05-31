using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Ware.Storages;

namespace Ware.PalletStorages
{
    /// <summary>
    /// A class for making an aisle for pallets
    /// </summary>
    public class PalletAisle : IPalletAisle
    {
        private string name;
        private List<PalletStorage> palletAisle;

        public event EventHandler<StorageEventArgs>? PalletStorageAddEvent;
        public event EventHandler<StorageEventArgs>? PalletStorageRemoveEvent;

        /// <summary>
        /// Constructor for making an aisle with a name and a empty list of PalletStorage
        /// </summary>
        /// <param name="aisleName">Name of the aisle</param>
        /// <exception cref="ArgumentException"></exception>
        public PalletAisle(string aisleName)
        {
            if (string.IsNullOrEmpty(aisleName))
            {
                throw new ArgumentException("Aisle name cannot be null or empty");
            }
            name = aisleName;
            palletAisle = new List<PalletStorage>();
        }

        /// <summary>
        /// Adds a PalletStorage to the PalletAisle
        /// </summary>
        /// <param name="palletStorage">A PalletStorage</param>
        public void AddPalletStorage(PalletStorage palletStorage)
        {
            palletAisle.Add(palletStorage);
            RaisePalletStorageAddEvent(palletStorage);
        }

        /// <summary>
        /// Removes a palletStorage from the PalletAisle
        /// </summary>
        /// <param name="palletStorage">A PalletAisle</param>
        public void RemovePalletStorage(PalletStorage palletStorage)
        {
            if (palletAisle.Contains(palletStorage))
            {
                palletAisle.Remove(palletStorage);
            }
        }

        /// <summary>
        /// Prints out the palletstorages
        /// </summary>
        public void GetAllPalletPrints()
        {
            foreach (PalletStorage palletStorage in palletAisle)
            {
                palletStorage.PrintAllPalletStorageInformation();
            }
        }

        private void RaisePalletStorageAddEvent(PalletStorage palletStorage)
        {
            PalletStorageAddEvent?.Invoke(this, new StorageEventArgs(palletStorage));
        }
        private void RaiseStorageRemoveEvent(PalletStorage palletStorage)
        {
            PalletStorageRemoveEvent?.Invoke(this, new StorageEventArgs(palletStorage));
        }
    }
}
