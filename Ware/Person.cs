using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Ware
{
    /// <summary>
    /// Creates a person
    /// </summary>
    /// <param packageName="name">Name of person</param>
    /// <param packageName="age">Age of person</param>
    /// <param packageName="accessLevel">Position / permissions of the person</param>
    public class Person(string name = "Unknown", int age = 0, CrewList.AccessLevel accessLevel = CrewList.AccessLevel.OTHERS) : IPerson
    {
        private string name = name;
        private int age = age;
        private readonly List<Assignment> assignments = [];
        private CrewList.AccessLevel accessLevel = accessLevel;

        /// <summary>
        /// Adds an object Assignment to the person.
        /// </summary>
        /// <param packageName="assignment">object assignment</param>
        public void AddAssignment(Assignment assignment)
        {
            assignments.Add(assignment);
        }

        /// <summary>
        /// Removes an assignment from a person.
        /// </summary>
        /// <param packageName="assignment">object assignment</param>
        public void RemoveAssignment(Assignment assignment)
        {
            assignments.Remove(assignment);
        }

        /// <summary>
        /// Gets the dictionary over all the tasks a person has received.
        /// </summary>
        /// <returns>A List</returns>
        public List<Assignment> GetAssignmentsList()
        {
            return assignments;
        }

        /// <summary>
        /// Console writes Current assignments
        /// </summary>
        public void GetAssignmentPrint()
        {
            foreach (Assignment assignment in assignments)
            {
                Console.WriteLine(assignment);
            }
        }

        /// <summary>
        /// Getter setter for packageName.
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// Getter setter for age.
        /// </summary>
        public int Age
        {
            get { return age; }
            set { age = value; }
        }

        /// <summary>
        /// Getter setter accesslevel of a person
        /// </summary>
        public CrewList.AccessLevel AccessLevel
        {
            get { return accessLevel; }
            set { accessLevel = value; }
        }
    }
}
