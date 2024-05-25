using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ware.Persons
{
    internal interface IPerson
    {
        void AddAssignment(Assignment assignment);
        void RemoveAssignment(Assignment assignment);
        List<Assignment> GetAssignmentsList();
        void GetAssignmentPrint();
    }
}
