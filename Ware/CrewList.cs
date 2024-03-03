using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Ware
{
    public class CrewList()
    {
        Dictionary<string, AccessLevel> list = [];


        public enum AccessLevel
        {
            CEO,
            HR,
            ADMINISTRATOR,
            DAILYLEADER,
            MANAGER,
            EMPLOYEE,
            JANITOR,
            VISITOR,
            OTHERS,
            EXTRA,
            OPERATOR,
            STOWER,
            PICKER,
            PACKER,
            CONTROLLER,
            AMBASSADOR,
            TECHNICAL,
        }

        /// <summary>
        /// Adds a member to the crew.
        /// </summary>
        /// <param name="name">Name of person</param>
        /// <param name="accessLevel">Job level / Access</param>
        public void AddCrewMember(string name, AccessLevel accessLevel)
        {
            list.Add(name, accessLevel);
        }

        /// <summary>
        /// Console writes the intire crew.
        /// </summary>
        public void CrewListPrint()
        {
            foreach(var item in list)
            {
                Console.WriteLine(item.Key + " " + item.Value);
            }
        }

        /// <summary>
        /// Gets all the memebers of the crew.
        /// </summary>
        /// <returns>A dict containing the crew members</returns>
        public Dictionary<string, AccessLevel> GetCrewListDictionary()
        {
            return list;
        }


    }
}
