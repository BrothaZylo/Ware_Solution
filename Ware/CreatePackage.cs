using System;
using System.Runtime.InteropServices;

namespace Ware
{
    public class CreatePackage(string packagename, string goodstype, string speedofdelivery, double packageheightCM, double packagewidthCM) : IPackage
    {
        public string packageid = GenerateId(), name = packagename, goods = goodstype, speed = speedofdelivery;
        public double height = packageheightCM, width = packagewidthCM;

        public static string GenerateId()
        {
            var selection = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz123456789";
            var newid = new char[15];
            for (int i = 0; i < 15; i++)
            {
                Random rand = new();
                newid[i] = selection[rand.Next(selection.Length)];
            }
            var trueid = new String(newid);
            return trueid;
        }

        public static string Inte(string x)
        {
            return x;
        }

        public static CreatePackage SendToWareHouse(CreatePackage packet)
        {
            return packet;
        }

        public List<CreatePackage> SendMultipleToWareHouse(List<CreatePackage> packetlist)
        {
            return packetlist;
        }

        public void SendMultipleToWareHouse()
        {
            throw new NotImplementedException();
        }

        public void SendToWareHouse()
        {
            throw new NotImplementedException();
        }

    }
}