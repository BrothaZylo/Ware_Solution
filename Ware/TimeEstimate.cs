using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Ware.TimeEstimate;

namespace Ware
{
    /// <summary>
    /// Set timers for "events"
    /// </summary>
    public class TimeEstimate
    {
        private readonly Dictionary<Storage, List<SubTimer>> storageTime = [];

        /// <summary>
        /// Sets the time for how long it takes to get a package from the shelves
        /// </summary>
        /// <param name="storage">Unit you want to set timers to get packagees</param>
        /// <param name="fromShelf">What floor to start</param>
        /// <param name="toShelf">What florr to stop</param>
        /// <param name="timeSeconds">Amount of time it takes to get something from fromshelf-toshelf</param>
        public void SetTimeStorage(Storage storage, int fromShelf, int toShelf, int timeSeconds)
        {
            if (!storageTime.TryGetValue(storage, out List<SubTimer>? value))
            {
                value = new List<SubTimer>();
                storageTime[storage] = value;
            }

            value.Add(new SubTimer { FromShelf = fromShelf, ToShelf = toShelf, TimeSeconds = timeSeconds });
        }

        /// <summary>
        /// Gets time it takes to get something from the storrage selected
        /// </summary>
        /// <param name="storage">Storage you want info from</param>
        /// <returns>a dict containing storage timers</returns>
        public Dictionary<Storage, List<SubTimer>> GetStorageTimeShelvesDictionary(Storage storage)
        {
            Dictionary<Storage, List<SubTimer>> tmp = [];
            foreach(KeyValuePair<Storage, List<SubTimer>> item in storageTime)
            {
                if (item.Key == storage)
                {
                    tmp.Add(item.Key, item.Value);
                }
            }
            return tmp;
        }

        /// <summary>
        /// Console prints time for storage
        /// </summary>
        public void GetStorageTimeprint()
        {
            foreach (KeyValuePair<Storage, List<SubTimer>> item in storageTime)
            {
                Console.WriteLine(item.Key.UniqueId+" Timers:");
                foreach (SubTimer subTimer in item.Value)
                {
                    Console.WriteLine($"[From Shelf: {subTimer.FromShelf} To Shelf: {subTimer.ToShelf} | Time Estimate: {subTimer.TimeSeconds} seconds]");
                }
            }
        }

        /// <summary>
        /// Subclass for list inputs
        /// </summary>
        public class SubTimer
        {
            private int fromShelf;
            private int toShelf;
            private int timeSeconds;

            /// <summary>
            /// Shelf number
            /// </summary>
            public int FromShelf
            {
                get { return fromShelf; }
                set { fromShelf = value; }
            }
            /// <summary>
            /// Shelf number
            /// </summary>
            public int ToShelf
            {
                get { return toShelf; }
                set { toShelf = value; }
            }
            /// <summary>
            /// Time it takes to get a package
            /// </summary>
            public int TimeSeconds
            {
                get { return timeSeconds; }
                set { timeSeconds = value; }
            }
        }
    }
}