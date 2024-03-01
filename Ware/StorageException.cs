using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ware
{
    public class StorageException : Exception
    {
        public StorageException() { }

        public StorageException(string message)
        : base(message) { }
    }
}
