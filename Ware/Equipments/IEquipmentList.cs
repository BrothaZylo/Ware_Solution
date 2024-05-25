using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ware.Equipments
{
    internal interface IEquipmentList
    {
        void AddEquipment(Equipment equipment);
        void RemoveEquipment(Equipment equipment);
        void EquipmentListPrint();
        Dictionary<string, (int, Equipment)> EquipmentListDictonary();
    }
}
