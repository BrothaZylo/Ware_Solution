using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ware.PackageLogs
{
    internal interface IPackageLogging
    {
        public string AddPackageLog(string packageID, string action);
        public void LogsPrint();
        public List<string> TrackPackage(string id);
        public Dictionary<string, List<(string, DateTime)>> GetAllPackageLog();
    }
}
