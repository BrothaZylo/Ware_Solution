// See https://aka.ms/new-console-template for more information
using Ware;

CreatePackage u = new CreatePackage("eeee");
u.name = "package name";

Console.WriteLine(u.name);
Console.WriteLine(u.packageid);

Shelf o = new("egg", 2, "3");
Console.WriteLine(o.space1);