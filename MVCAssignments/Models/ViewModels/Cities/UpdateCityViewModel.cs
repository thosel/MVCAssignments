using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVCAssignments.ViewModels
{
    public class UpdateCityViewModel
    {
        public int Id { get; set; }

        [Display(Name = "City name:")]
        [Required(ErrorMessage = "A city name is required.")]
        public string Name { get; set; }

        [Display(Name = "Country:")]
        [Required(ErrorMessage = "A country is required.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "A country is required.")]
        public string CountryId { get; set; }

        public IEnumerable<SelectListItem> Countries { get; set; }
    }
}
