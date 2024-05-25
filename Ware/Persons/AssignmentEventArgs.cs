using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ware.Persons
{
    /// <summary>
    /// e
    /// </summary>
    public class AssignmentEventArgs : EventArgs
    {
        public Person Person { get; private set; }
        public Assignment Assignment { get; private set; }
        public string PersonName { get; private set; }

        public AssignmentEventArgs(Assignment assignment)
        {
            Assignment = assignment;
        }

        public AssignmentEventArgs(Assignment assignment, string personName)
        {
            Assignment = assignment;
            PersonName = personName;
        }
    }
}