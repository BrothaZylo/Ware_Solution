using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Ware.TimeEstimate;

namespace Ware
{
    public class TimeEstimate
    {
        private int seconds = 0;
        private readonly Dictionary<Storage, List<SubTimer>> storageTime = [];

        public void SetTimeStorage(Storage storage, int fromShelf, int toShelf, int timeSeconds)
        {
            if (!storageTime.ContainsKey(storage))
            {
                storageTime[storage] = new List<SubTimer>();
            }
            storageTime[storage].Add(new SubTimer { FromShelf = fromShelf, ToShelf = toShelf, TimeSeconds = timeSeconds });
        }

        public void GetStorageTimeprint()
        {
            foreach (KeyValuePair<Storage, List<SubTimer>> item in storageTime)
            {
                foreach (SubTimer subTimer in item.Value)
                {
                    Console.WriteLine($"[From Shelf: {subTimer.FromShelf} To Shelf: {subTimer.ToShelf} | Time Estimate: {subTimer.TimeSeconds} seconds]");
                    Console.WriteLine("eeeeeeeeeeeeeeeeeeeeeeeeeeeee");
                }
            }
        }

        public class SubTimer
        {
            public int FromShelf { get; set; }
            public int ToShelf { get; set; }
            public int TimeSeconds { get; set; }
        }
    }
}