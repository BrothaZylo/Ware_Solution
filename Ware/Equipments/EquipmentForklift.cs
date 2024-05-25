using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ware.Equipments
{
    /// <summary>
    /// Creates a door
    /// </summary>
    /// <param name="name">name of door</param>
    /// <param name="quantity">how many doors</param>
    public class EquipmentForklift(string name, int quantity) : Equipment(name, quantity)
    {
    }
}
