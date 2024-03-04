using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Ware
{
    /// <summary>
    /// Exceptions for package errors.
    /// </summary>
    public class PackageInvalidException : Exception
    {
        /// <summary>
        /// Exceptions for package errors.
        /// </summary>
        public PackageInvalidException() { }

        /// <summary>
        /// Exceptions for package errors with custom catch message.
        /// </summary>
        /// <param packageName="message">Custom message for exception interception</param>
        public PackageInvalidException(string message)
        : base(message) { }
    }
}
