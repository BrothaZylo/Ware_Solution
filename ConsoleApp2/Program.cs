// See https://aka.ms/new-console-template for more information
using Ware;

//----------------------------------------------------------//
//--------------------Default packages----------------------//
//----------------------------------------------------------//
Package package1 = new("Chips", "Dry", 15, 3);
Package package2 = new("Ost", "Dry", 14, 23);
Package package3 = new("Moose", "Dangerous", 84, 43);
Package package6 = new("ebb", "Dangerous", 84, 43);
Package package7 = new("eee", "Dangerous", 84, 43);
Package package4 = new("Cream", "Refrigerated", 84, 43);
Package package5 = new("Ice", "Refrigerated", 18, 39);

Package package8 = new("Ice boog", "Refrigerated", 18, 39);

ReceivingDepartment receivingDepartment = new ReceivingDepartment();
receivingDepartment.AddPackage(package1);
receivingDepartment.AddPackage(package2);

//----------------------------------------------------------//
//-----------------------Simulation-------------------------//
//----------------------------------------------------------//
/*
Simulation sim = new(30);

sim.AddPackage(package1);
sim.AddPackage(package2);
sim.AddPackage(package3);
sim.AddPackage(package4);
sim.AddPackage(package5);
sim.AddPackage(package6);
sim.AddPackage(package7);
sim.AddPackage(package8);
sim.Run();
*/
CrewList crew = new();

crew.AddCrewMember("Ola", CrewList.AccessLevel.HR);
crew.AddCrewMember("Big", CrewList.AccessLevel.ADMINISTRATOR);

crew.CrewListPrint();


