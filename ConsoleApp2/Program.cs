// See https://aka.ms/new-console-template for more information
using Ware;

//----------------------------------------------------------//
//--------------------Default packages----------------------//
//----------------------------------------------------------//
Package package1 = new("Chips", "Dry", 15, 3);
Package package2 = new("Ost", "Dry", 14, 23);
Package package3 = new("Moose", "Dangerous", 84, 43);
Package package4 = new("Cream", "Refrigerated", 84, 43);
Package package5 = new("Ice", "Refrigerated", 18, 39);


//----------------------------------------------------------//
//-----------------------Simulation-------------------------//
//----------------------------------------------------------//
Simulation sim = new(60);

sim.AddPackage(package1);
sim.AddPackage(package2);
sim.AddPackage(package3);
sim.AddPackage(package4);
sim.AddPackage(package5);
sim.Run();

