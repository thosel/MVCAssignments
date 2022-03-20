using System.Collections.Generic;

namespace MVCAssignments.Models.ClientAppModels
{
    public class ClientAppCreatePerson
    {
        public string Name { get; set; }

        public string Phone { get; set; }

        public int CityId { get; set; }

        public List<int> LanguageIds { get; set; }
    }
}
