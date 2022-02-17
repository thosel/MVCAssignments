using MVCAssignments.Models;
using System.Collections.Generic;

namespace MVCAssignments.ViewModels
{
    public class CitiesViewModel
    {
        public CreateCityViewModel CreateCityViewModel { get; set; }

        public List<City> Cities { get; set; }
    }
}
