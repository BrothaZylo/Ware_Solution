using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ware
{
    /// <summary>
    /// Exceptions for package errors.
    /// </summary>
    public class NegativeNumberException : Exception
    {
        /// <summary>
        /// Exceptions for package errors.
        /// </summary>
        public NegativeNumberException() { }

        /// <summary>
        /// Exceptions for package errors with custom catch message.
        /// </summary>
        /// <param packageName="message">Custom message for exception interception</param>
        public NegativeNumberException(string message)
        : base(message) { }
    }
}
