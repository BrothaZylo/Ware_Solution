using System;
using System.Dynamic;
using System.Runtime.InteropServices;
using System.Xml.Linq;

namespace Ware
{
    /// <summary>
    /// Creating packages
    /// </summary>
    public class Package : IPackage
    {
        private string name, goods;
        private double heightCm, widthCm;
        string packageId = GenerateId();
        /// <summary>
        /// Create a package
        /// </summary>
        /// <param name="packageName">Name of the package</param>
        /// <param name="goodsType">Package goods type</param>
        /// <param name="packageHeightCm">height of the package in cm</param>
        /// <param name="packageWidthCm">width of the package in cm.</param>
        public Package(string packageName = "Undefined", string goodsType = "Undefined", double packageHeightCm = 1, double packageWidthCm = 1)
        {
            name = packageName;
            goods = goodsType;
            if(packageHeightCm <= 0 || packageWidthCm <= 0)
            {
                throw new NegativeNumberException("Package height or width cannot be 0 or lower");
            }
            heightCm = packageHeightCm;
            widthCm = packageWidthCm;
        }

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
            get { return widthCm; }
            set { widthCm = value; }
        }

        /// <summary>
        /// Gets the package height
        /// </summary>
        /// <returns>height</returns>
        public double Height
        {
            get { return heightCm; }
            set { heightCm = value; }
        }

    }
}
