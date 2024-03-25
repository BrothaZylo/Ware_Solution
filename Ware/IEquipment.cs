using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ware
{
    internal interface IEquipment
    {
        void UseEquipment(Person person);
        void StopUsingEquipment(Person person);
        List<string> UsageHistory();
        void AccessLevelPrint();
        void AddAccessLevel(AccessLevel accessLevel);
        void DeleteAccessLevel(AccessLevel accessLevel);
        AccessLevel? GetAccessLevelPrint(string sname);
    }
}
