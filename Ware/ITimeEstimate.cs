using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Ware.TimeEstimate;

namespace Ware
{
    internal interface ITimeEstimate
    {
        void SetTimeStorageGetPackage(Storage storage, int fromShelf, int toShelf, int timeSeconds);
        void SetTimeStorageGetPallet(PalletStorage palletStorage, int fromShelf, int toShelf, int timeSeconds);
        Dictionary<Storage, List<SubTimerCollection>> GetStorageTimeToGetPackageDictionary(Storage storage);
        Dictionary<PalletStorage, List<SubTimerCollection>> GetStorageTimeToGetPalletDictionary(PalletStorage palletStorage);
        void GetStorageTimeToGetPackagePrint();
        void GetStorageTimeToGetPalletPrint();

    }
}
