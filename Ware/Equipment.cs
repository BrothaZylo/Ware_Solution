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
    /// <param name="name"></param>
    /// <param name="quantity"></param>
    public class Equipment(string name, int quantity)
    {
        private readonly List<CrewList.AccessLevel> equipment = [];
        private string name = name;
        private int quantity = quantity;

        /// <summary>
        /// Adds permission locks behind set
        /// </summary>
        /// <param name="accessLevel">Enum access level</param>
        public void AddAccessLevel(CrewList.AccessLevel accessLevel)
        {
            equipment.Add(accessLevel);
        }

        public CrewList.AccessLevel? GetAccessLevel(string sname)
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
        /// <param name="accessLevel">Enum CrewList.AccessLevel</param>
        public void DeleteAccessLevel(CrewList.AccessLevel accessLevel)
        {
            equipment.Remove(accessLevel);
        }

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
    }
}
