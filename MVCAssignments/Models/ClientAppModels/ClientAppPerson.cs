using System.Collections.Generic;

namespace MVCAssignments.Models.ClientAppModels
{
    public class ClientAppPerson
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public List<string> Languages { get; set; }
    }
}
