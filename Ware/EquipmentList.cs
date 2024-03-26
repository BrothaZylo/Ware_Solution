using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ware
{
    /// <summary>
    /// Storage for work equipment or machinery.
    /// </summary>
    public class EquipmentList : IEquipmentList
    {
        private readonly List<Equipment> allEquipmentList = [];
        private readonly Dictionary<string, (int, Equipment)> allEquipment = [];

        /// <summary>
        /// Adds equipment from Equipment class to a collective place.
        /// </summary>
        /// <param name="equipment">name of equipment object</param>
        public void AddEquipment(Equipment equipment)
        {
            allEquipmentList.Add(equipment);
            allEquipment.Add(equipment.Name, (equipment.Quantity, equipment));
        }

        /// <summary>
        /// Removes equipment
        /// </summary>
        /// <param name="equipment">equipment class</param>
        public void RemoveEquipment(Equipment equipment)
        {
            allEquipment.Remove(equipment.Name);
        }

        /// <summary>
        /// Console write all the equipment
        /// </summary>
        public void EquipmentListPrint()
        {
            foreach (KeyValuePair<string, (int, Equipment)> item in allEquipment)
            {
                Console.WriteLine("------------");
                Console.WriteLine(item.Key+"\n");
                Console.WriteLine(item.Value.Item2.GetAccessLevelPrint(item.Key));
            }
            Console.WriteLine("------------");
        }

        /// <summary>
        /// Gets the dictionay containing all the equipment
        /// </summary>
        /// <returns>A dictionary containing all the equipment</returns>
        public Dictionary<string, (int, Equipment)> EquipmentListDictonary()
        {
            return allEquipment;
        }
    }
}
