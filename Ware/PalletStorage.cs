using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ware
{
    /// <summary>
    /// Manages the storage of pallets.
    /// </summary>
    public class PalletStorage : IPalletStorage
    {
        private readonly Dictionary<string, (Pallet?,string, bool)> palletStorageDict = new();
        private readonly List<ShelvesConfig> shelvesConfigs = new();

        /// <summary>
        /// Builds the storage layout based on the configured shelves.
        /// </summary>
        public void BuildStorage()
        {
            int shelfNumber = 1;
            foreach (PalletStorage.ShelvesConfig shelf in shelvesConfigs)
            {
                for (int i = 0; i < shelf.TotalUnitsAvailable; i++)
                {
                    string shelfId = "Shelf-" + shelfNumber;
                    palletStorageDict.Add(shelfId, (null, shelf.SizeName, false));
                    shelfNumber++;
                }
            }
        }

        /// <summary>
        /// Places a pallet onto a shelf in the storage.
        /// </summary>
        /// <param name="pallet">The pallet to be placed.</param>
        /// <param name="shelfId">The id for the shelf where the pallet should be placed.</param>
        /// <exception cref="InvalidOperationException">Thrown when the shelf is already occupied.</exception>
        /// <exception cref="KeyNotFoundException">Thrown when the shelf id does not exist.</exception>

        public void PlacePallet(Pallet pallet, string shelfId)
        {
            bool shelfNotFound = true;

            if (palletStorageDict.ContainsKey(shelfId))
            {
                shelfNotFound = false;
                (Pallet?, string, bool) shelf = palletStorageDict[shelfId];
                if (shelf.Item3 == false)
                {
                    palletStorageDict[shelfId] = (pallet, shelf.Item2, true);
                }
                else if (shelf.Item3 == true)
                {
                    throw new InvalidOperationException("Shelf is already occupied.");
                }
            }

            if (shelfNotFound)
            {
                throw new KeyNotFoundException("Shelf ID does not exist.");
            }
        }

        /// <summary>
        /// Removes a pallet from a shelf.
        /// </summary>
        /// <param name="shelfId">The id of the shelf from which the pallet should be removed.</param>
        /// <exception cref="InvalidOperationException">Thrown when the pallet is not found or the shelf is already empty.</exception>
        public void RemovePallet(string shelfId)
        {
            if (palletStorageDict.ContainsKey(shelfId) && palletStorageDict[shelfId].Item3)
            {
                palletStorageDict[shelfId] = (null, palletStorageDict[shelfId].Item2, false);
            }
            else
            {
                throw new InvalidOperationException("Pallet not found or shelf is already empty.");
            }
        }

        /// <summary>
        /// Prints all information about the pallet storage.
        /// </summary>
        public void PrintAllPalletStorageInformation()
        {
            foreach (KeyValuePair<string, (Pallet?, string, bool)> item in palletStorageDict)
            {
                if (item.Value.Item1 == null)
                {
                    Console.WriteLine($"[{item.Key} | (Pallet: Empty) | (Size: {item.Value.Item2})]");
                }
                else
                {
                    int packageCount = item.Value.Item1.packagesOnPallet.Count;
                    Console.WriteLine($"[{item.Key} | (Pallet: {packageCount} packages) | (Size: {item.Value.Item2})]");
                }
            }
        }

        /// <summary>
        /// Configures a new shelf within the pallet storage.
        /// </summary>
        /// <param name="sizeName">The name representing the size of the shelf.</param>
        /// <param name="totalUnitsAvailable">The total number of units available on the shelf.</param>
        private class ShelvesConfig
        {
            public string SizeName { get; set; }
            public int TotalUnitsAvailable { get; set; }
        }

        /// <summary>
        /// Adds a new shelf configuration to the storage system.
        /// </summary>
        /// <param name="sizeName">The name representing the size of the shelf.</param>
        /// <param name="totalUnitsAvailable">The total number of units that the shelf has space for.</param>
        public void AddShelf(string sizeName, int totalUnitsAvailable)
        {
            shelvesConfigs.Add(new ShelvesConfig { SizeName = sizeName, TotalUnitsAvailable = totalUnitsAvailable });
        }
    }
}
