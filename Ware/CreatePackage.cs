using System;
using System.Runtime.InteropServices;

namespace Ware
{
    public class CreatePackage : IPackage
    {
        public string packageid;
        public string name;

        public CreatePackage(string name)
        {
            name = name;
            packageid = GenerateId();
        }

        public string GenerateId()
        {
            var selection = "abcdefghijklmnopqrstuvwxyz123456789";
            var newid = new char[15];
            for (int i = 0; i < 15; i++)
            {
                Random rand = new Random();
                int num = rand.Next(selection.Length);
                int randomchar = selection.ElementAt(num);
                newid[i] = selection[rand.Next(selection.Length)];
            }
            var trueid = new String(newid);
            return trueid;
        }


    }
}