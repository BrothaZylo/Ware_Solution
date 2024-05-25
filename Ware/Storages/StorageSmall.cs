using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ware.Storages
{
    /// <summary>
    /// A smaller version of a storage unit
    /// </summary>
    /// <param name="goodsType">type of goods the unit will carry</param>
    /// <param name="uniqueStorageId">unique identifier of storage</param>
    public class StorageSmall(string goodsType, string uniqueStorageId) : Storage(goodsType, uniqueStorageId)
    {
    }
}
