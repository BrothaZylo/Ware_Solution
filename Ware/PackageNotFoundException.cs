using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Ware
{
    public class PackageNotFoundException : Exception
    {
        public PackageNotFoundException() { }

        public PackageNotFoundException(string message)
        : base(message) { }


    }
}
