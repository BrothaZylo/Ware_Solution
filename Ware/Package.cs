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
    /// <param name="goodsType">Package goodsType type</param>
    /// <param name="packageHeightCm">packageHeightCm of the package in cm</param>
    /// <param name="packageWidthCm">packageWidthCm of the package i cm.</param>
    public class Package(string packageName = "Undefined", string goodsType = "Undefined", double packageHeightCm = 0, double packageWidthCm = 0) : IPackage
    {
        private string packageId = GenerateId(), packageName = packageName, goodsType = goodsType;
        private double packageHeightCm = packageHeightCm, packageWidthCm = packageWidthCm;

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
        /// Gets the package packageName
        /// </summary>
        /// <returns>packageName</returns>
        public string Name
        {
            get { return packageName; }
            set { packageName = value; }
        }

        /// <summary>
        /// Gets the package goodsType type
        /// </summary>
        /// <returns>goodsType</returns>
        public string Goods
        {
            get { return goodsType; }
            set { goodsType = value; }
        }

        /// <summary>
        /// Gets the package packageWidthCm
        /// </summary>
        /// <returns>packageWidthCm</returns>
        public double Width
        {
            get { return packageWidthCm; }
            set { packageWidthCm = value; }
        }

        /// <summary>
        /// Gets the package packageHeightCm
        /// </summary>
        /// <returns>packageHeightCm</returns>
        public double Height
        {
            get { return packageHeightCm; }
            set { packageHeightCm = value; }
        }

    }

}