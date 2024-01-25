using System;

namespace Ware
{
    public class CreatePackages
    {
        public string packageid;
        public string name;

        public CreatePackages(string packageid, string name)
        {
            packageid = packageid;
            name = name;
        }
        
        public string li()
        {
            return "Name: "+name+"\nPackage id: "+ packageid;
        }

    }
}