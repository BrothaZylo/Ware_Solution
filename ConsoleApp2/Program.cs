// See https://aka.ms/new-console-template for more information
using Ware;

CreatePackage u = new("Hestesko", "kjølevare", "fast", 82.5, 43.4);

Console.WriteLine(u.name);
Console.WriteLine(u.packageid);
Console.WriteLine(u.goods);
Console.WriteLine(u.speed);
Console.WriteLine(u.height);
Console.WriteLine(u.width);

CreatePackage xd = CreatePackage.SendToWareHouse(u);
Console.WriteLine(xd.name+" "+xd.packageid);