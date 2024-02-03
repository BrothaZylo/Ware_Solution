using System;
using System.Runtime.InteropServices;

namespace Ware
{
    /// <summary>
    /// Representerer en pakke med dens egenskaper og funksjonalitet for håndtering.
    /// </summary>
    /// <param name="packagename">Navn på pakken.</param>
    /// <param name="goodstype">Typen gods som er i pakken.</param>
    /// <param name="speedofdelivery">Farten på leveringen.</param>
    /// <param name="packageheightCM">Høyden på pakken i cm.</param>
    /// <param name="packagewidthCM">Bredden på pakken i cm.</param>
    public class CreatePackage(string packagename, string goodstype, string speedofdelivery, double packageheightCM, double packagewidthCM) : IPackage
    {
        public string packageid = GenerateId(), name = packagename, goods = goodstype, speed = speedofdelivery;
        public double height = packageheightCM, width = packagewidthCM;

        /// <summary>
        /// En Id genereres til pakken.
        /// </summary>
        /// <returns>Returnerer en unik Id til pakken.</returns>
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


        public static CreatePackage SendToWareHouse(CreatePackage packet)
        {
            return packet;
        }

        public static List<CreatePackage> SendMultipleToWareHouse(List<CreatePackage> packetlist)
        {
            return packetlist;
        }


    }
}