using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ware.Terminals;

namespace Ware
{
    /// <summary>
    /// Manages the storage of pallets.ee
    /// </summary>
    public class PalletStorage : IPalletStorage
    {
        private readonly Dictionary<string, (List<Pallet>, string, bool)> palletStorageDict = new();
        private readonly List<ShelvesConfig> shelvesConfigs = new();
        private bool northAccess = true;
        private bool eastAccess = true;
        private bool southAccess = true;
        private bool westAccess = true;
        private string storageName;

        /// <summary>
        /// Initializes a new instance of the PalletStorage class.
        /// </summary>
        /// <param name="name">The name of the storage.</param>
        public PalletStorage(string name)
        {
            storageName = name;
        }

        /// <summary>
        /// Gets all pallets stored in the storage.
        /// </summary>
        /// <returns>A dictionary containing all pallets and their data.</returns>
        public Dictionary<string, (List<Pallet>, string, bool)> GetAllPalletsInStorage()
        {
            return palletStorageDict;
        }

        /// <summary>
        /// Builds the storage layout based on the configured shelves.
        /// </summary>
        public void BuildStorage()
        {
            int shelfNumber = 1;
            foreach (PalletStorage.ShelvesConfig shelf in shelvesConfigs)
            {
                string shelfId = $"{storageName} : Shelf {shelfNumber.ToString("D3")}";
                palletStorageDict.Add(shelfId, (new List<Pallet>(new Pallet[1]), shelf.SizeName, false));
                shelfNumber++;
            }
        }

        /// <summary>
        /// Places a pallet onto a shelf in the storage.
        /// </summary>
        /// <param name="pallet">The pallet to be placed.</param>
        /// <param name="shelfId">Id of the shelf, used like this PalletStorage_1 : Shelf 001 where X is the shelf.</param>
        /// <exception cref="InvalidOperationException">Thrown when the shelf is already occupied.</exception>
        /// <exception cref="KeyNotFoundException">Thrown when the shelf id does not exist.</exception>
        public void PlacePallet(Pallet pallet, string shelfId)
        {
            if (palletStorageDict.TryGetValue(shelfId, out (List<Pallet>, string, bool) shelfInfo))
            {
                (List<Pallet> shelfContent, string sizeName, bool isOccupied) = shelfInfo;

                if (shelfContent[0] == null)
                {
                    shelfContent[0] = pallet;
                    palletStorageDict[shelfId] = (shelfContent, sizeName, true);
                }
                else
                {
                    throw new InvalidOperationException("The shelf is already occupied.");
                }
            }
            else
            {
                throw new KeyNotFoundException($"The shelf ID '{shelfId}' does not exist.");
            }
        }

        /// <summary>
        /// Places the pallets automatic in an empty spot.
        /// </summary>
        /// <param name="pallet">The Pallet being placed.</param>
        /// <exception cref="InvalidOperationException">Thrown when there isn't any space for the pallet.</exception>
        public void PlacePalletAutomatic(Pallet pallet)
        {
            foreach (KeyValuePair<string, (List<Pallet>, string, bool)> entry in palletStorageDict)
            {
                (List < Pallet > shelfContent, string sizeName, bool isOccupied) = entry.Value;
                if (!isOccupied)
                {
                    int index = shelfContent.FindIndex(p => p == null);
                    if (index != -1)
                    {
                        shelfContent[index] = pallet;
                        palletStorageDict[entry.Key] = (shelfContent, sizeName, true);
                        return;
                    }
                }
            }
            throw new InvalidOperationException("No empty space available for the pallet.");
        }

        /// <summary>
        /// Removes a pallet from the shelf.
        /// </summary>
        /// <param name="pallet">The pallet being removed from the shelf.</param>
        /// <exception cref="InvalidOperationException">Thrown when the pallet was not found.</exception>
        public void RemovePallet(Pallet pallet)
        {
            foreach (KeyValuePair<string, (List<Pallet>, string, bool)> entry in palletStorageDict)
            {
                (List<Pallet> shelfContent, string sizeName, bool isOccupied) = entry.Value;
                int index = shelfContent.IndexOf(pallet);
                if (index != -1)
                {
                    shelfContent[index] = null;
                    isOccupied = shelfContent.Any(p => p != null);
                    palletStorageDict[entry.Key] = (shelfContent, sizeName, isOccupied);
                    return;
                }
            }
            throw new InvalidOperationException("Pallet not found.");
        }

        /// <summary>
        /// Prints all information about the pallet storage.
        /// </summary>
        public void PrintAllPalletStorageInformation()
        {
            foreach (KeyValuePair<string, (List<Pallet>, string, bool)> entry in palletStorageDict)
            {
                (List<Pallet> shelfContent, string sizeName, _) = entry.Value;
                string packages = shelfContent[0] != null
                    ? $"Packages: {string.Join(", ", shelfContent[0].GetPackagesOnPallet().Select(pkg => pkg.Name))}"
                    : "Empty";

                Console.WriteLine($"[{entry.Key} | {packages} | Size: {sizeName}]");
            }
        }

        /// <summary>
        /// Sends all pallets to the terminal.
        /// </summary>
        /// <param name="terminal">The terminal the pallets are being sent to.</param>
        public void SendsAllPalletsToTerminal(Terminal terminal)
        {
            foreach (string entry in palletStorageDict.Keys.ToList())
            {
                (List<Pallet> shelfContent, string sizeName, bool isOccupied) = palletStorageDict[entry];

                if (isOccupied && shelfContent[0] != null)
                {
                    terminal.AddPallet(shelfContent[0]);
                    shelfContent[0] = null;
                    palletStorageDict[entry] = (shelfContent, sizeName, false);
                }
            }
        }

        /// <summary>
        /// Sends specified pallet to the terminal.
        /// </summary>
        /// <param name="pallet">The pallet being sent to terminal.</param>
        /// <param name="terminal">The terminal receiving the pallet.</param>
        /// <exception cref="InvalidOperationException">Thrown when the pallet was not found in the storage.</exception>
        public void SendPalletToTerminal(Pallet pallet, Terminal terminal)
        {
            bool palletFound = false;

            foreach (KeyValuePair<string, (List<Pallet>, string, bool)> entry in palletStorageDict)
            {
                (List<Pallet>  shelfContent, string sizeName, bool isOccupied) = entry.Value;

                if (isOccupied && shelfContent[0] == pallet)
                {
                    terminal.AddPallet(pallet);
                    shelfContent[0] = null;
                    palletStorageDict[entry.Key] = (shelfContent, sizeName, false);
                    palletFound = true;
                    break;
                }
            }

            if (!palletFound)
            {
                throw new InvalidOperationException("Pallet not found in storage.");
            }
        }

        /// <summary>
        /// Used to check specified pallet in storage.
        /// </summary>
        /// <param name="pallet">Specified pallet being checked.</param>
        /// <returns>returns null.</returns>
        public Pallet? GetPallet(Pallet pallet)
        {
            foreach (KeyValuePair<string, (List<Pallet>, string, bool)> entry in palletStorageDict)
            {
                (List<Pallet> shelfContent, string sizename, bool isOccupied) = entry.Value;

                if (isOccupied && shelfContent.Contains(pallet))
                {
                    return pallet;
                }
            }

            return null;
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
        /// The name of the PalletStorage
        /// </summary>
        public string StorageName
        {
            get { return storageName;  }
            set { storageName = value; }
        }


        /// <summary>
        /// Configuration of a shelf within the pallet storage system.
        /// </summary>
        /// <param name="sizeName">The name representing the size of the shelf.</param>
        /// <param name="shelfSpace">The total number of shelfs available on the shelf row.</param>
        private class ShelvesConfig
        {
            public string SizeName { get; set; }
            public int ShelfSpace { get; set; }
        }

        /// <summary>
        /// Adds a new shelf configuration to the storage system.
        /// </summary>
        /// <param name="sizeName">The name representing the size of the shelf. </param>
        /// <param name="shelfSpace">The amount of shelfs added to the row. </param>
        public void AddShelf(string sizeName, int shelfSpace)
        {
            for (int i = 0; i < shelfSpace; i++)
            {
                shelvesConfigs.Add(new ShelvesConfig { SizeName = sizeName, ShelfSpace = 1 });
            }
        }
    }
}