using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ware
{
    public class PalletStorage : IPalletStorage
    {
        private readonly Dictionary<string, (Pallet?,string, bool)> palletStorageDict = new();
        private readonly List<ShelvesConfig> shelvesConfigs = new();

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

        private class ShelvesConfig
        {
            public string SizeName { get; set; }
            public int TotalUnitsAvailable { get; set; }
        }
        public void AddShelf(string sizeName, int totalUnitsAvailable)
        {
            shelvesConfigs.Add(new ShelvesConfig { SizeName = sizeName, TotalUnitsAvailable = totalUnitsAvailable });
        }
    }
}
