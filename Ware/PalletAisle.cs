using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Ware
{
    public class PalletAisle : IPalletAisle
    {
        private string name;
        private List<PalletStorage> palletAisle;

        public event EventHandler<StorageEventArgs>? PalletStorageAddEvent;
        public event EventHandler<StorageEventArgs>? PalletStorageRemoveEvent;


        private void RaisePalletStorageAddEvent(PalletStorage palletStorage)
        {
            PalletStorageAddEvent?.Invoke(this, new StorageEventArgs(palletStorage));
        }
        private void RaiseStorageRemoveEvent(PalletStorage palletStorage)
        {
            PalletStorageRemoveEvent?.Invoke(this, new StorageEventArgs(palletStorage));
        }




        public PalletAisle(string aisleName)
        {
            if (string.IsNullOrEmpty(aisleName))
            {
                throw new ArgumentException("Aisle name cannot be null or empty");
            }
            name = aisleName;
            palletAisle = new List<PalletStorage>();
        }
        public void AddPalletStorage(PalletStorage palletStorage)
        {
            palletAisle.Add(palletStorage);
            RaisePalletStorageAddEvent(palletStorage);
        }
        public void RemovePalletStorage(PalletStorage palletStorage)
        {
            if (palletAisle.Contains(palletStorage))
            {
                palletAisle.Remove(palletStorage);
            }
        }
        public void GetAllPalletPrints()
        {
            foreach(PalletStorage palletStorage in palletAisle)
            {
                palletStorage.PrintAllPalletStorageInformation();
            }
        }


    }
}
