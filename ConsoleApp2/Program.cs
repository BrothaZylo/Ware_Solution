// See https://aka.ms/new-console-template for more information
using Ware;

CreatePackage u = new("Hestesko", "kjølevare", "fast", 82.5, 43.4);
CreatePackage uu = new("Pæreboks", "kulvare", "treg", 91.3, 15.7);

Console.WriteLine(u.name);
Console.WriteLine(u.packageid);
Console.WriteLine(u.goods);
Console.WriteLine(u.speed);
Console.WriteLine(u.height);
Console.WriteLine(u.width);

CreatePackage xd = CreatePackage.SendToWareHouse(u);
Console.WriteLine(xd.name+" "+xd.packageid);
List<CreatePackage> e = [u, uu];
Console.WriteLine(e);
for(int i = 0; i < e.Count; i++)
{
    Console.WriteLine(e[i].name);
}
