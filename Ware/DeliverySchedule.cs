using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ware
{
    public class DeliverySchedule(List<DeliverySchedule.DeliveryList> ListOfDeliveries)
    {
        private Dictionary<string, List<DeliveryList>> Calender = [];
        private List<DeliveryList> YourDeliveryList = ListOfDeliveries;
        private List<DeliveryList> PackageList = new();

        public void CreateSchedule()
        {
            int count = 1;
            foreach (var i in ListOfDeliveries)
            {
                Calender.Add(i.Day,(PackageList));
                count++;
                
            }
        }

        public void SchedulePrint()
        {
            foreach(var i in Calender)
            {
                Console.WriteLine(i.Key);
                foreach (var j in PackageList)
                {
                    Console.WriteLine(j.Day);
                    if (i.Key == j.Day)
                    {
                        Console.WriteLine($"Id: {j.Packages.packageid} Name: {j.Packages.name} Type of goods: {j.Packages.goods}");

                    }
                }
            }
        }


        public class DeliveryList 
        {
            public string Day;
            public CreatePackage Packages;
            public DateTime DeliveryTime;
            public int DeliveryType;
        }
    }
}
