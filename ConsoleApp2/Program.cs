﻿// See https://aka.ms/new-console-template for more information
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

Assignment assignment = new("Garbage");
assignment.AddDescription("Take the garbage out");
assignment.AssignmentPrint();

Person person = new("Ole", 32, CrewList.AccessLevel.PACKER);
Person person1 = new("Oink", 56, CrewList.AccessLevel.EMPLOYEE);
Person person2 = new("Vegg", 44, CrewList.AccessLevel.MANAGER);

crew.AddCrewMember(person);
crew.AddCrewMember(person1);
crew.AddCrewMember(person2);
crew.CrewListPrint();

person.AddAssignment(assignment);
person.GetAssignmentPrint();


EquipmentList equipmentList = new();
Equipment equipment = new("Scanner", 5);
equipment.AddAccessLevel(CrewList.AccessLevel.TECHNICAL);
equipment.AddAccessLevel(CrewList.AccessLevel.CEO);


Equipment equipment1 = new("Lift", 8);
equipment1.AddAccessLevel(CrewList.AccessLevel.SUPPORT);
equipment1.AddAccessLevel(CrewList.AccessLevel.ADMINISTRATOR);
equipment1.AddAccessLevel(CrewList.AccessLevel.AMBASSADOR);

Equipment equipment2 = new("Truck", 7);
equipment2.AddAccessLevel(CrewList.AccessLevel.EXTRA1);
equipment2.AddAccessLevel(CrewList.AccessLevel.EMPLOYEE);

equipmentList.AddEquipment(equipment);
equipmentList.AddEquipment(equipment1);
equipmentList.AddEquipment(equipment2);
equipmentList.EquipmentListPrint();



