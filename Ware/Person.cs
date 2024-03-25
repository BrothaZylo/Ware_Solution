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
    /// <param name="name">Name of person</param>
    /// <param name="age">Age of person</param>
    /// <param name="accessLevel">Position / permissions of the person</param>
    public class Person(string name = "Unknown", int age = 0, AccessLevel accessLevel = AccessLevel.OTHERS) : IPerson
    {
        private string name = name;
        private int age = age;
        private readonly List<Assignment> assignments = [];
        private AccessLevel accessLevel = accessLevel;
        private string isUsingEquipment = "";

        /// <summary>
        /// Adds an object Assignment to the person.
        /// </summary>
        /// <param name="assignment">object assignment</param>
        public void AddAssignment(Assignment assignment)
        {
            assignments.Add(assignment);
        }

        /// <summary>
        /// Removes an assignment from a person.
        /// </summary>
        /// <param name="assignment">object assignment</param>
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
        /// Checks if the user is using an equipment
        /// </summary>
        public string IsUsingEquipment
        {
            get { return isUsingEquipment; }
            set { isUsingEquipment = value; }
        }

        /// <summary>
        /// Getter setter for name.
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
        public AccessLevel AccessLevel
        {
            get { return accessLevel; }
            set { accessLevel = value; }
        }
    }
}
