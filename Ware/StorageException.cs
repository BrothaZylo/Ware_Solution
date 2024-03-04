using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ware
{
    /// <summary>
    /// Exception for storage errors.
    /// </summary>
    public class StorageException : Exception
    {
        /// <summary>
        /// Exception for storage errors.
        /// </summary>
        public StorageException() { }

        /// <summary>
        /// Exception for storage errors.
        /// </summary>
        /// <param packageName="message"></param>
        public StorageException(string message)
        : base(message) { }
    }
}
