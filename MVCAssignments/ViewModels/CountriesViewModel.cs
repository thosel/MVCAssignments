using MVCAssignments.Models;
using System.Collections.Generic;

namespace MVCAssignments.ViewModels
{
    public class CountriesViewModel
    {
        public CreateCountryViewModel CreateCountryViewModel { get; set; }

        public List<Country> Countries { get; set; }
    }
}
