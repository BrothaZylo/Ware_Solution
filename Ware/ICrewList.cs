using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Ware.CrewList;

namespace Ware
{
    internal interface ICrewList
    {
        void AddCrewMember(Person person);

        void RemoveCrewMember(Person person);
        void CrewListPrint();
        Dictionary<string, AccessLevel> GetCrewListDictionary();
    }
}
