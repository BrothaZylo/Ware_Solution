using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ware
{
    /// <summary>
    /// Creates an assignment
    /// </summary>
    /// <param name="assignment">Name of the assignment</param>
    public class Assignment(string assignment = "Undefined")  : IAssignment
    {
        private string assignment = assignment;
        private string description = "";
        private DateTime completionTime = new(2024, 03, 3, 0, 0, 0);

        /// <summary>
        /// Adds a description to the assignment if needed.
        /// </summary>
        /// <param name="adddescription">Describes the assignment</param>
        public void AddDescription(string adddescription)
        {
            Description = adddescription;
        }

        /// <summary>
        /// Prints the assignment
        /// </summary>
        public void AssignmentPrint()
        {
            Console.WriteLine(Task + "\n" + Description);
        }

        /// <summary>
        /// Getter setter for description
        /// </summary>
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        /// <summary>
        /// Getter setter for when the assignment is to be done.
        /// </summary>
        public DateTime CompletionTime
        {
            get { return completionTime; }
            set { completionTime = value; }
        }

        /// <summary>
        /// Getter setter for assignment
        /// </summary>
        public string Task
        {
            get { return assignment; }
            set { assignment = value; }
        }

        /// <summary>
        /// object info
        /// </summary>
        /// <returns>Task and Description</returns>
        override
        public string ToString()
        {
            return "\n" + Task + "\n" + Description + "\n" + CompletionTime;
        }
    }
}
