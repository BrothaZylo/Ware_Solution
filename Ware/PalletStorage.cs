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
        /// <param name="shelfId">Id of the shelf, used like this "Shelf-1" where X is the shelf.</param>
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
        /// Places the pallets automatic in an empty spot.
        /// </summary>
        /// <param name="pallet">The pallet that's going to be placed automatic</param>
        /// <exception cref="InvalidOperationException">Thrown when there's no space left in the storage.</exception>
        public void PlacePalletAutomatic(Pallet pallet)
        {
            foreach (KeyValuePair<string, (List<Pallet>, string, bool)> shelfEntry in palletStorageDict)
            {
                (List<Pallet> palletsOnShelf, string sizeName, bool isOccupied) = shelfEntry.Value;
                for (int floor = 0; floor < palletsOnShelf.Count; floor++)
                {
                    if (palletsOnShelf[floor] == null)
                    {
                        palletsOnShelf[floor] = pallet;
                        palletStorageDict[shelfEntry.Key] = (palletsOnShelf, shelfEntry.Value.Item2, true);
                        return;
                    }
                }
            }

            throw new InvalidOperationException("No empty space available for the pallet.");
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
            foreach (KeyValuePair<string, (List<Pallet>, string, bool)> shelfEntry in palletStorageDict)
            {
                int shelfNumber = int.Parse(shelfEntry.Key.Split('-')[1]) - 1;
                char shelfLetter = (char)('A' + shelfNumber);

                for (int i = 0; i < shelfEntry.Value.Item1.Count; i++)
                {
                    string floorLabel = $"{shelfLetter}{i + 1}";
                    string floorStatus = shelfEntry.Value.Item1[i] != null ? $"{shelfEntry.Value.Item1[i].packagesOnPallet.Count} packages" : "Empty";
                    Console.WriteLine($"[Shelf-{shelfNumber + 1} | Floor : {floorLabel} | {floorStatus} | Size: {shelfEntry.Value.Item2}]");
                }
            }
        }

        /// <summary>
        /// Sends all packages from a specified pallet to the terminal and clears the pallet.
        /// </summary>
        /// <param name="shelfId">The shelf id where the pallet is.</param>
        /// <param name="floor">The floor number on the shelf where the pallet is.</param>
        /// <param name="terminal">The terminal to which the pallets are being sent to.</param>
        /// <exception cref="KeyNotFoundException">Used when the specified shelf id does not exist.</exception>
        /// <exception cref="InvalidOperationException">If there's no pallet on the specified floor or if the floor number is invalid.</exception>
        public void SendPalletToTerminal(string shelfId, int floor, Terminal terminal)
        {
            if (!palletStorageDict.ContainsKey(shelfId))
            {
                throw new KeyNotFoundException("Shelf ID does not exist.");
            }

            (List<Pallet> pallets, string sizeName, bool isOccupied) = palletStorageDict[shelfId];

            if (floor < 0 || floor >= pallets.Count)
            {
                throw new InvalidOperationException("Invalid floor number.");
            }

            Pallet pallet = pallets[floor];
            if (pallet == null)
            {
                throw new InvalidOperationException("No pallet found on the specified floor.");
            }
            terminal.AddPallet(pallet);
            pallets[floor] = null;
            palletStorageDict[shelfId] = (pallets, sizeName, pallets.Any(p => p != null));
        }

        /// <summary>
        /// Sends pallets to the terminal automatic.
        /// </summary>
        /// <param name="pallet">The pallet that is being sent to the terminal.</param>
        /// <param name="terminal">The terminal which where the pallet is being sent to.</param>
        /// <exception cref="InvalidOperationException">Thrown when the pallet is not found in the storage.</exception>
        public void SendPalletToTerminalAutomatic(Pallet pallet, Terminal terminal)
        {
            bool palletFound = false;

            foreach (KeyValuePair<string, (List<Pallet>, string, bool)> shelfEntry in palletStorageDict)
            {
                List<Pallet> palletsOnShelf = shelfEntry.Value.Item1;
                string sizeName = shelfEntry.Value.Item2;
                bool isOccupied = false;

                for (int floor = 0; floor < palletsOnShelf.Count; floor++)
                {
                    if (palletsOnShelf[floor] == pallet)
                    {
                        terminal.AddPallet(pallet);
                        palletsOnShelf[floor] = null;
                        palletFound = true;
                    }
                }

                foreach (Pallet remainingPallet in palletsOnShelf)
                {
                    if (remainingPallet != null)
                    {
                        isOccupied = true;
                        break;
                    }
                }
                
                if (palletFound)
                {
                    palletStorageDict[shelfEntry.Key] = (palletsOnShelf, sizeName, isOccupied);
                    break;
                }
            }

            if (!palletFound)
            {
                throw new InvalidOperationException("Pallet not found in storage.");
            }
        }


        /// <summary>
        /// Sets the direction of access points to the storage.
        /// </summary>
        /// <param name="north">If set to true, access from the north side is allowed.</param>
        /// <param name="east">If set to true, access from the east side is allowed.</param>
        /// <param name="south">If set to true, access from the south side is allowed.</param>
        /// <param name="west">If set to true, access from the west side is allowed.</param>
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
        /// Configuration of a shelf within the pallet storage system.
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
