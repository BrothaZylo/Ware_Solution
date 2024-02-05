using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ware
{
    internal interface IPackageHistory
    {
        public string AddPackageLog(string packageID, string action);
        public void GetPackageLog();


    }
}
