using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ware.PalletStorages;
using Ware.Storages;
using static Ware.TimeEstimates.TimeEstimate;

namespace Ware.TimeEstimates
{
    /// <summary>
    /// Set timers for "events"
    /// </summary>
    public class TimeEstimate
    {
        private readonly Dictionary<Storage, List<SubTimerCollection>> storageTime = [];
        private readonly Dictionary<PalletStorage, List<SubTimerCollection>> palletTime = [];

        /// <summary>
        /// Sets the time for how long it takes to get a package from the shelves
        /// </summary>
        /// <param name="storage">Unit you want to set timers to get packagees</param>
        /// <param name="placeOrGetBox">PlaceOrGetBox enum</param>
        /// <param name="fromShelf">What floor to start</param>
        /// <param name="toShelf">What florr to stop</param>
        /// <param name="timeSeconds">Amount of time it takes to get something from fromshelf-toshelf</param>
        public void SetTimeStorageGetPackage(Storage storage, PlaceOrGetBox placeOrGetBox, int fromShelf, int toShelf, int timeSeconds)
        {
            if (!storageTime.TryGetValue(storage, out List<SubTimerCollection>? value))
            {
                value = new List<SubTimerCollection>();
                storageTime[storage] = value;
            }
            value.Add(new SubTimerCollection { PlaceOrGet = placeOrGetBox, FromShelf = fromShelf, ToShelf = toShelf, TimeSeconds = timeSeconds });
        }

        /// <summary>
        /// Sets the time for how long it takes to get a package from the shelves
        /// </summary>
        /// <param name="palletStorage">Unit you want to set timers to get pallets</param>
        /// <param name="fromShelf">What floor to start</param>
        /// <param name="toShelf">What florr to stop</param>
        /// <param name="timeSeconds">Amount of time it takes to get something from fromshelf-toshelf</param>
        public void SetTimeStorageGetPallet(PalletStorage palletStorage, int fromShelf, int toShelf, int timeSeconds)
        {
            if (!palletTime.TryGetValue(palletStorage, out List<SubTimerCollection>? value))
            {
                value = new List<SubTimerCollection>();
                palletTime[palletStorage] = value;
            }

            value.Add(new SubTimerCollection { FromShelf = fromShelf, ToShelf = toShelf, TimeSeconds = timeSeconds });
        }

        /// <summary>
        /// Gets time it takes to get something from the storage selected
        /// </summary>
        /// <param name="storage">Storage you want info from</param>
        /// <returns>a dict containing storage timers</returns>
        public Dictionary<Storage, List<SubTimerCollection>> GetStorageTimeToGetPackageDictionary(Storage storage)
        {
            Dictionary<Storage, List<SubTimerCollection>> tmp = [];
            foreach (KeyValuePair<Storage, List<SubTimerCollection>> item in storageTime)
            {
                if (item.Key == storage)
                {
                    tmp.Add(item.Key, item.Value);
                }
            }
            return tmp;
        }

        /// <summary>
        /// Gets time it takes to get something from the palletstorage selected
        /// </summary>
        /// <param name="palletStorage">pallet you want info from</param>
        /// <returns>a dict containing palletstorage timers</returns>
        public Dictionary<PalletStorage, List<SubTimerCollection>> GetStorageTimeToGetPalletDictionary(PalletStorage palletStorage)
        {
            Dictionary<PalletStorage, List<SubTimerCollection>> tmp = [];
            foreach (KeyValuePair<PalletStorage, List<SubTimerCollection>> item in palletTime)
            {
                if (item.Key == palletStorage)
                {
                    tmp.Add(item.Key, item.Value);
                }
            }
            return tmp;
        }

        /// <summary>
        /// Console prints time for storage
        /// </summary>
        public void GetStorageTimeToGetPackagePrint()
        {
            foreach (KeyValuePair<Storage, List<SubTimerCollection>> item in storageTime)
            {
                Console.WriteLine(item.Key.UniqueId + " Timers:");
                foreach (SubTimerCollection subTimer in item.Value)
                {
                    Console.WriteLine($"[{subTimer.PlaceOrGet} | From Shelf: {subTimer.FromShelf} To Shelf: {subTimer.ToShelf} | Time Estimate: {subTimer.TimeSeconds} seconds]");
                }
            }
        }

        /// <summary>
        /// Console prints time for PalletStorage
        /// </summary>
        public void GetStorageTimeToGetPalletPrint()
        {
            foreach (KeyValuePair<PalletStorage, List<SubTimerCollection>> item in palletTime)
            {
                Console.WriteLine(item.Key + " Timers:");
                foreach (SubTimerCollection subTimer in item.Value)
                {
                    Console.WriteLine($"[{subTimer.PlaceOrGet} | From Shelf: {subTimer.FromShelf} To Shelf: {subTimer.ToShelf} | Time Estimate: {subTimer.TimeSeconds} seconds]");
                }
            }
        }

        /// <summary>
        /// Decides if you want to place or get a packagee/pallt
        /// </summary>
        public enum PlaceOrGetBox
        {
            /// <summary>
            /// Get a pallet/packet
            /// </summary>
            PLACE,
            /// <summary>
            /// Places a pallet/packet
            /// </summary>
            GET,
        }
    }
}