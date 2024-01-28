// See https://aka.ms/new-console-template for more information
using System.Xml.Linq;
using Ware;

CreatePackage u = new("Hestesko", "kjølevare", "fast", 82.5, 43.4);

Console.WriteLine(u.name);
Console.WriteLine(u.packageid);
List<WareHouseSizeConfig> configlist =
[
    new() { Sizename = "Tiny", Totalunitsavailable = 10, Maxlengthcm = 10.5, Maxwidthcm = 5},
    new() { Sizename = "Large", Totalunitsavailable = 15, Maxlengthcm = 30, Maxwidthcm = 30 }
];

ShelvesUnits house = new("Frysevarer", 25, configlist);
