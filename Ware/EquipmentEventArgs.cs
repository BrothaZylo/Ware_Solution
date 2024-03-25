using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ware
{
    /// <summary>
    /// Events for equipment
    /// </summary>
    public class EquipmentEventArgs : EventArgs
    {
        /// <summary>
        /// Equipment class
        /// </summary>
        public Equipment? Equipment { get; private set; }

        /// <summary>
        /// Person class
        /// </summary>
        public Person? Person { get; private set; }

        /// <summary>
        /// Accesslevel enums
        /// </summary>
        public AccessLevel? AccessLevel { get; private set; }

        /// <summary>
        /// message value
        /// </summary>
        public string? Text { get; set; }

        /// <summary>
        /// Empty events args
        /// </summary>
        public EquipmentEventArgs()
        {
        }

        /// <summary>
        /// Equipment event containting Equipment object
        /// </summary>
        /// <param name="equipment">Equipment object</param>
        public EquipmentEventArgs(Equipment? equipment)
        {
            Equipment = equipment;
        }

        /// <summary>
        /// Equipment event containing Equipment and Person objects
        /// </summary>
        /// <param name="equipment">Equipment object</param>
        /// <param name="person">Person object</param>
        public EquipmentEventArgs(Equipment? equipment, Person? person)
        {
            Equipment = equipment;
            Person = person;
        }

        /// <summary>
        /// Equipment event containing Equipment and Accesslevel objects
        /// </summary>
        /// <param name="equipment"></param>
        /// <param name="accessLevel"></param>
        public EquipmentEventArgs(Equipment equipment, AccessLevel? accessLevel)
        {
            Equipment = equipment;
            AccessLevel = accessLevel;
        }

        /// <summary>
        /// Equipment event containing Equipment and Person objects
        /// </summary>
        /// <param name="equipment">Equipment object</param>
        /// <param name="person">Person object</param>
        /// <param name="accessLevel">Person object</param>
        public EquipmentEventArgs(Equipment? equipment, Person? person, AccessLevel? accessLevel)
        {
            Equipment = equipment;
            Person = person;
            AccessLevel = accessLevel;
        }

        /// <summary>
        /// Equipment event containing person objects
        /// </summary>
        /// <param name="person">Person object</param>
        public EquipmentEventArgs(Person? person)
        {
            Person = person;
        }

        /// <summary>
        /// Equipement event containing person and accesslevel objects
        /// </summary>
        /// <param name="person"></param>
        /// <param name="accessLevel"></param>
        public EquipmentEventArgs(Person person, AccessLevel? accessLevel)
        {
            Person = person;
            AccessLevel = accessLevel;
        }
        
    }
}
