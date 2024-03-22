using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ware
{
    /// <summary>
    /// Creates an equipment or machinery.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="quantity"></param>
    public class Equipment(string name = "Undefined", int quantity = 0) : IEquipment
    {
        private readonly List<AccessLevel> equipment = [];
        private string name = name;
        private int quantity = quantity;
        private readonly List<string> usages = [];

        /// <summary>
        /// Use the equipment, this does not check if the user has the correct accesslevel
        /// </summary>
        /// <param name="person">Person that is going to use it</param>
        public void UseEquipment(Person person)
        {
            person.IsUsingEquipment = name;
            usages.Add(""+person.Name+" started using "+name +" "+DateTime.Now);
        }

        /// <summary>
        /// When a person stops using an equipment
        /// </summary>
        /// <param name="person">Person using the equipment</param>
        public void StopUsingEquipment(Person person)
        {
            person.IsUsingEquipment = "none";
            usages.Add("" + person.Name + " stopped using " + name + " " + DateTime.Now);
        }

        /// <summary>
        /// Gets the local history of set equipment usage
        /// </summary>
        /// <returns>History of equipment usage in a list</returns>
        public List<string> UsageHistory()
        {
            return usages;
        }

        /// <summary>
        /// Adds permission locks behind set
        /// </summary>
        /// <param name="accessLevel">Enum access level</param>
        public void AddAccessLevel(AccessLevel accessLevel)
        {
            equipment.Add(accessLevel);
        }

        /// <summary>
        /// Deletes the equipment access for set enum.
        /// </summary>
        /// <param name="accessLevel">Enum AccessLevel</param>
        public void DeleteAccessLevel(AccessLevel accessLevel)
        {
            equipment.Remove(accessLevel);
        }

        /// <summary>
        /// Checks if a person has access to use equipment
        /// </summary>
        /// <param name="person">object person</param>
        /// <returns>True or false</returns>
        public bool HasAccess(Person person)
        {
            foreach(AccessLevel item in equipment)
            {
                if(person.AccessLevel == item)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Itterative soluton for foreach () console writes all the accesses.
        /// </summary>
        /// <param name="personName">name of the key of EquipmentList dict</param>
        /// <returns>null</returns>
        public AccessLevel? GetAccessLevelPrint(string personName)
        {
            foreach (AccessLevel item in equipment)
            {
                if (name == personName)
                {
                    Console.WriteLine(item);
                }
            }
            return null;
        }

        /// <summary>
        /// Console writes the access level of selected equipment.
        /// </summary>
        public void AccessLevelPrint()
        {
            foreach (AccessLevel item in equipment)
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
