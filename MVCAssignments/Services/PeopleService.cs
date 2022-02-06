using MVCAssignments.Models;
using System.Collections.Generic;

namespace MVCAssignments.Services
{
    public class PeopleService
    {
        public static readonly List<Person> People = new List<Person>();
        public static int PersonCounter = 0;
    }
}
