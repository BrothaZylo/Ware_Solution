using System;
using System.Dynamic;
using System.Runtime.InteropServices;
using System.Xml.Linq;

namespace Ware
{
    /// <summary>
    /// Create a package
    /// </summary>
    /// <param name="packageName">Name of the package</param>
    /// <param name="goodsType">Package goods type</param>
    /// <param name="packageHeightCm">height of the package in cm</param>
    /// <param name="packageWidthCm">width of the package i cm.</param>
    public class Package(string packageName = "Undefined", string goodsType = "Undefined", double packageHeightCm = 0, double packageWidthCm = 0) : IPackage
    {
        private string packageId = GenerateId(), name = packageName, goods = goodsType;
        private double height = packageHeightCm, width = packageWidthCm;

        /// <summary>
        /// Generates an ID for the package
        /// </summary>
        /// <returns>Returns the package id</returns>
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