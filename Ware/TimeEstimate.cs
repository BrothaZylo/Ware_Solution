using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ware
{
    public class TimeEstimate
    {
        private int seconds = 0;
        private readonly Dictionary<Storage, List<SubTimer>> storageTime = [];

        public void SetTimeStorage(Storage storage, int fromShelf, int toShelf, int timeSeconds)
        {
            foreach (var item in storageTime.Keys)
            {
                if (!storageTime.ContainsKey(storage))
                {
                    List<SubTimer> subTimerList = new List<SubTimer> { };
                    subTimerList.Add(new SubTimer { FromShelf = fromShelf, ToShelf = toShelf, TimeSeconds = timeSeconds });
                    storageTime.Add(storage, subTimerList);
                }
            }
        }

        public void GetStorageTimeprint()
        {
            Console.WriteLine("eeeeeeeeeeee");

            foreach (var item in storageTime)
            {
                Console.WriteLine("eeeeeeeeeeee");
                foreach(var subTimer in item.Value)
                {
                    Console.WriteLine("[Shelfs:" + subTimer.ToShelf + "-" + subTimer.FromShelf + " | TimeToGetEstimate:" + subTimer.TimeSeconds + "]");
                    Console.WriteLine("eeeeeeeeeeeeeeeeeeeeeeeeeeeee");
                }
            }
        }



        public class SubTimer
        {
            public int FromShelf {  get; set; }
            public int ToShelf { get; set; }
            public int TimeSeconds { get; set; }
        }

    }
}
