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
        private readonly List<SubTimer> storageTime = [];

        public void SetTimeStorage(Storage storage, int fromShelf, int toShelf, int timeSeconds)
        {
            storageTime.Add(new() { FromShelf = fromShelf, ToShelf = toShelf, TimeSeconds = timeSeconds });
        }

        public void prnt()
        {
            foreach (var item in storageTime)
            {
                Console.WriteLine("[Shelfs:"+item.FromShelf +"-"+ item.ToShelf + " | TimeToGetEstimate:" + item.TimeSeconds+"]");
            }
        }

        private class SubTimer
        {
            public int FromShelf {  get; set; }
            public int ToShelf { get; set; }
            public int TimeSeconds { get; set; }


        }

    }
}
