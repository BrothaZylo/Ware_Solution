using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ware
{
    internal interface ICreatePackage
    {
        string GetPackageId();

        string GetPackageName();

        string GetPackageGoodsType();

        string GetPackageSpeedofdelivery();

        double GetPackageWidth();

        double GetPackageHeight();
    }
}
