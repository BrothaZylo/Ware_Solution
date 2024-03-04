using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ware
{
    /// <summary>
    /// Creates an equipment or machinery.
    /// </summary>
    /// <param packageName="name"></param>
    /// <param packageName="quantity"></param>
    public class Equipment(string name = "Undefined", int quantity = 0) : IEquipment
    {
        private readonly List<CrewList.AccessLevel> equipment = [];
        private string name = name;
        private int quantity = quantity;

        /// <summary>
        /// Adds permission locks behind set
        /// </summary>
        /// <param packageName="accessLevel">Enum access level</param>
        public void AddAccessLevel(CrewList.AccessLevel accessLevel)
        {
            equipment.Add(accessLevel);
        }

        /// <summary>
        /// Checks if a person has access to use equipment
        /// </summary>
        /// <param packageName="person">object person</param>
        /// <returns>True or false</returns>
        public bool HasAccess(Person person)
        {
            foreach(CrewList.AccessLevel item in equipment)
            {
                if(person.AccessLevel == item)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Itterative soluton for foreach :() console writes all the accesses.
        /// </summary>
        /// <param packageName="sname">packageName of the key of EquipmentList dict</param>
        /// <returns>null</returns>
        public CrewList.AccessLevel? GetAccessLevelPrint(string sname)
        {
            foreach (CrewList.AccessLevel item in equipment)
            {
                if (name == sname)
                {
                    Console.WriteLine(item);
                }
            }
            return null;
        }

        /// <summary>
        /// Deletes the equipment access for set enum.
        /// </summary>
        /// <param packageName="accessLevel">Enum CrewList.AccessLevel</param>
        public void DeleteAccessLevel(CrewList.AccessLevel accessLevel)
        {
            equipment.Remove(accessLevel);
        }

        /// <summary>
        /// Console writes the access level of selected equipment.
        /// </summary>
        public void AccessLevelPrint()
        {
            foreach (CrewList.AccessLevel item in equipment)
            {
                Console.WriteLine(item);
            }
        }

        /// <summary>
        /// Name of equipment.
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// Quantity of equipment.
        /// </summary>
        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }

        /// <summary>
        /// Eqipment obj print
        /// </summary>
        /// <returns>Name and quantity</returns>
        override
        public string ToString()
        {
            return "\n" + Name + "\n" + Quantity + "\n";
        }
    }
}
