using System;
using System.Dynamic;
using System.Runtime.InteropServices;
using System.Xml.Linq;

namespace Ware
{
    /// <summary>
    /// Representerer en pakke med dens egenskaper og funksjonalitet for håndtering.
    /// </summary>
    /// <param name="packageName">Navn på pakken.</param>
    /// <param name="goodsType">Typen gods som er i pakken.</param>
    /// <param name="packageHeightCm">Høyden på pakken i cm.</param>
    /// <param name="packageWidthCm">Bredden på pakken i cm.</param>
    public class Package(string packageName, string goodsType,double packageHeightCm, double packageWidthCm) : IPackage
    {
        private string packageId = GenerateId(), name = packageName, goods = goodsType;
        private double height = packageHeightCm, width = packageWidthCm;

        /// <summary>
        /// Generates an ID for the package
        /// </summary>
        /// <returns>Returns a a uni.</returns>
        private static string GenerateId()
        {
            string selection = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz123456789";
            char[] newid = new char[15];
            for (int i = 0; i < 15; i++)
            {
                Random rand = new();
                newid[i] = selection[rand.Next(selection.Length)];
            }
            string generatedid = new(newid);
            return generatedid;
        }

        /// <summary>
        /// Gets the package id
        /// </summary>
        /// <returns>packageid</returns>
        public string PackageId
        {
            get { return packageId; }
            set { packageId = value; }
        }

        /// <summary>
        /// Gets the package name
        /// </summary>
        /// <returns>name</returns>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// Gets the package goods type
        /// </summary>
        /// <returns>goods</returns>
        public string Goods
        {
            get { return goods; }
            set { goods = value; }
        }

        /// <summary>
        /// Gets the package width
        /// </summary>
        /// <returns>width</returns>
        public double Width
        {
            get { return width; }
            set { width = value; }
        }

        /// <summary>
        /// Gets the package height
        /// </summary>
        /// <returns>height</returns>
        public double Height
        {
            get { return height; }
            set { height = value; }
        }

    }

}