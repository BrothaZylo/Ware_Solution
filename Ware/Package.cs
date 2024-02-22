using System;
using System.Dynamic;
using System.Runtime.InteropServices;

namespace Ware
{
    /// <summary>
    /// Representerer en pakke med dens egenskaper og funksjonalitet for håndtering.
    /// </summary>
    /// <param name="packageName">Navn på pakken.</param>
    /// <param name="goodsType">Typen gods som er i pakken.</param>
    /// <param name="speedOfDelivery">Farten på leveringen.</param>
    /// <param name="packageHeightCm">Høyden på pakken i cm.</param>
    /// <param name="packageWidthCm">Bredden på pakken i cm.</param>
    public class Package(string packageName, string goodsType, string speedOfDelivery, double packageHeightCm, double packageWidthCm) : IPackage
    {
        public string PackageId = GenerateId(), Name = packageName, Goods = goodsType, SpeedOfDelivery = speedOfDelivery;
        public double Height = packageHeightCm, Width = packageWidthCm;

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
        public string GetPackageId()
        {
            return PackageId;
        }

        /// <summary>
        /// Gets the package name
        /// </summary>
        /// <returns>name</returns>
        public string GetPackageName()
        {
            return Name;
        }

        /// <summary>
        /// Gets the package goods type
        /// </summary>
        /// <returns>goods</returns>
        public string GetPackageGoodsType()
        {
            return Goods;
        }

        /// <summary>
        /// Gets the package speed
        /// </summary>
        /// <returns>speed</returns>
        public string GetPackageSpeedofdelivery()
        {
            return SpeedOfDelivery;
        }

        /// <summary>
        /// Gets the package Width
        /// </summary>
        /// <returns>width</returns>
        public double GetPackageWidth()
        {
            return Width;
        }

        /// <summary>
        /// Gets the package height
        /// </summary>
        /// <returns>height</returns>
        public double GetPackageHeight()
        {
            return Height;
        }
        /// <summary>
        /// Sets the package width
        /// </summary>
        /// <param name="newWidth">the package width</param>
        public void SetPackageWidth(double newWidth)
        {
            Width = newWidth;
        }
        /// <summary>
        /// Sets the pacakge height
        /// </summary>
        /// <param name="newHeight">package height</param>
        public void SetPackageHeight(double newHeight)
        {
            Height = newHeight;
        }
    }

}