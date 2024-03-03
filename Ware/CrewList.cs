using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Ware
{
    /// <summary>
    /// Creates a crewlist for everyone working there. Can also make positions for outsiders.
    /// </summary>
    public class CrewList() : ICrewList
    {
        private readonly Dictionary<string, AccessLevel> list = [];

        /// <summary>
        /// Job / access / position
        /// </summary>
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
            OPERATOR,
            STOWER,
            PICKER,
            PACKER,
            SENDER,
            CONTROLLER,
            AMBASSADOR,
            TECHNICAL,
            SUPPORT,
            OTHERS,
            EXTRA1,
            EXTRA2,
            EXTRA3,
            EXTRA4,
            EXTRA5,
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
        /// Removes a crewmember
        /// </summary>
        /// <param name="name">Name of the person you want removed</param>
        public void RemoveCrewMember(string name)
        {
            list.Remove(name);
        }

        /// <summary>
        /// Console writes the intire crew.
        /// </summary>
        public void CrewListPrint()
        {
            foreach(var item in list)
            {
                Console.WriteLine("| " + item.Key + " | " + item.Value + " | ");
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
