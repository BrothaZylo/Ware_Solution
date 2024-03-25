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
        private readonly Dictionary<string, (List<Pallet>, string, bool)> palletStorageDict = new();
        private readonly List<ShelvesConfig> shelvesConfigs = new();
        private bool northAccess = true;
        private bool eastAccess = true;
        private bool southAccess = true;
        private bool westAccess = true;

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
                    palletStorageDict.Add(shelfId, (new List<Pallet>(new Pallet[shelf.Floors]), shelf.SizeName, false));
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

        public void PlacePallet(Pallet pallet, string shelfId, int floor)
        {
            if (palletStorageDict.ContainsKey(shelfId))
            {
                (List<Pallet> pallets, string sizeName, bool isOccupied) = palletStorageDict[shelfId];

                if (floor < 0 || floor > pallets.Count - 1)
                {
                    throw new IndexOutOfRangeException("Invalid floor number.");
                }

                if (pallets[floor] == null)
                {
                    pallets[floor] = pallet;
                    palletStorageDict[shelfId] = (pallets, sizeName, pallets.Contains(null) ? false : true);
                }
                else
                {
                    throw new InvalidOperationException("The specified floor is already occupied.");
                }
            }
            else
            {
                throw new KeyNotFoundException("Shelf ID does not exist.");
            }
        }

        /// <summary>
        /// Removes a pallet from a shelf.
        /// </summary>
        /// <param name="shelfId">The id of the shelf from which the pallet should be removed.</param>
        /// <param name="floor">The floor number on the shelf from which the pallet should be removed.</param>
        /// <exception cref="InvalidOperationException">Thrown when the floor is empty.</exception>
        /// <exception cref="KeyNotFoundException">Used when the shelf ID does not exist in the storage.</exception>
        public void RemovePallet(string shelfId, int floor)
        {
            if (palletStorageDict.ContainsKey(shelfId))
            {
                (List<Pallet> pallets, string sizeName, bool _) = palletStorageDict[shelfId];

                if (floor < 0 || floor > pallets.Count - 1)
                {
                    throw new InvalidOperationException("Pallet not found or invalid floor specified.");
                }

                pallets[floor] = null;
                palletStorageDict[shelfId] = (pallets, sizeName, pallets.Any(p => p != null));
            }
            else
            {
                throw new KeyNotFoundException("Shelf ID does not exist.");
            }
        }

        /// <summary>
        /// Prints all information about the pallet storage.
        /// </summary>
        public void PrintAllPalletStorageInformation()
        {
            foreach (KeyValuePair<string, (List<Pallet>, string, bool)> item in palletStorageDict)
            {
                string shelfInfo = item.Key + " | (Size: " + item.Value.Item2 + ") | Floors: ";
                for (int i = 0; i < item.Value.Item1.Count; i++)
                {
                    shelfInfo += $"[{i}: " + (item.Value.Item1[i] != null ? $"{item.Value.Item1[i].packagesOnPallet.Count} packages" : "Empty") + "]";
                }
                Console.WriteLine(shelfInfo);
            }
        }
        /// <summary>
        /// Sets the directions of accesspoint to the storage collectivly. True if can access, else false.
        /// </summary>
        /// <param name="north">Bool</param>
        /// <param name="east">Bool</param>
        /// <param name="south">Bool</param>
        /// <param name="west">Bool</param>
        public void SetAccessDirection(bool north, bool east, bool south, bool west)
        {
            northAccess = north;
            southAccess = south;
            westAccess = west;
            eastAccess = east;
        }

        /// <summary>
        /// Check if you can access the storage from the north side.
        /// </summary>
        public bool NorthAccess
        {
            get { return northAccess; }
            set { northAccess = value; }
        }

        /// <summary>
        /// Check if you can access the storage from the south side.
        /// </summary>
        public bool SouthAccess
        {
            get { return southAccess; }
            set { southAccess = value; }
        }

        /// <summary>
        /// Check if you can access the storage from the east side
        /// </summary>
        public bool EastAccess
        {
            get { return eastAccess; }
            set { eastAccess = value; }
        }

        /// <summary>
        /// Check if you can access the storage from the west side
        /// </summary>
        public bool WestAccess
        {
            get { return westAccess; }
            set { westAccess = value; }
        }

        /// <summary>
        /// Configures a new shelf within the pallet storage.
        /// </summary>
        /// <param name="sizeName">The name representing the size of the shelf.</param>
        /// <param name="totalUnitsAvailable">The total number of units available on the shelf.</param>
        /// <param name="Floors">Number of floors per shelf.</param>
        private class ShelvesConfig
        {
            public string SizeName { get; set; }
            public int TotalUnitsAvailable { get; set; }
            public int Floors { get; set; }
        }

        /// <summary>
        /// Adds a new shelf configuration to the storage system.
        /// </summary>
        /// <param name="sizeName">The name representing the size of the shelf.</param>
        /// <param name="totalUnitsAvailable">The total number of units that the shelf has space for.</param>
        /// <param name="Floors">Number of floors per shelf.</param>
        public void AddShelf(string sizeName, int totalUnitsAvailable, int floors)
        {
            shelvesConfigs.Add(new ShelvesConfig { SizeName = sizeName, TotalUnitsAvailable = totalUnitsAvailable, Floors = floors });
        }

    }
}
