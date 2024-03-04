using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ware
{
    internal interface IEquipment
    {
        void AccessLevelPrint();
        void AddAccessLevel(CrewList.AccessLevel accessLevel);
        void DeleteAccessLevel(CrewList.AccessLevel accessLevel);
        CrewList.AccessLevel? GetAccessLevelPrint(string sname);


    }
}
