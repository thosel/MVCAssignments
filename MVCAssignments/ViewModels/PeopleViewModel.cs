using MVCAssignments.Models;
using System.Collections.Generic;

namespace MVCAssignments.ViewModels
{
    public class PeopleViewModel
    {
        public CreatePersonViewModel CreatePersonViewModel { get; set; }

        public AddPersonLanguageViewModel AddPersonLanguageViewModel { get; set; }

        public List<Person> People { get; set; }
    }
}
