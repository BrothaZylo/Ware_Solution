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
        private readonly Dictionary<string, AccessLevel> dictionary = [];

        /// <summary>
        /// Adds a member to the crew.
        /// </summary>
        /// <param name="person">Person object</param>
        public void AddCrewMember(Person person)
        {
            dictionary.Add(person.Name, person.AccessLevel);
        }

        /// <summary>
        /// Removes a crewmember
        /// </summary>
        /// <param name="person">Person object you want removed</param>
        public void RemoveCrewMember(Person person)
        {
            dictionary.Remove(person.Name);
        }

        /// <summary>
        /// Console writes the intire crew.
        /// </summary>
        public void CrewListPrint()
        {
            foreach(KeyValuePair<string, AccessLevel> item in dictionary)
            {
                Console.WriteLine(item.Key + " - " + item.Value);
            }
        }

        /// <summary>
        /// Gets all the memebers of the crew.
        /// </summary>
        /// <returns>A dict containing the crew members</returns>
        public Dictionary<string, AccessLevel> GetCrewListDictionary()
        {
            return dictionary;
        }
    }
}
