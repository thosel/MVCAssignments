using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVCAssignments.ViewModels
{
    public class CreatePersonViewModel
    {
        [Display(Name = "Person name:")]
        [Required(ErrorMessage = "A person name is required.")]
        public string Name { get; set; }

        [Display(Name = "Phone number:")]
        [Required(ErrorMessage = "A phone number is required.")]
        public string Phone { get; set; }

        [Display(Name = "City:")]
        [Required(ErrorMessage = "A city is required.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "A city is required.")]
        public string CityId { get; set; }

        [Display(Name = "Country:")]
        [Required(ErrorMessage = "A country is required.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "A country is required.")]
        public string CountryId { get; set; }

        [Display(Name = "Languages:")]
        public List<string> LanguageIds { get; set; }

        public IEnumerable<SelectListItem> Cities { get; set; }

        public IEnumerable<SelectListItem> Countries { get; set; }

        public IEnumerable<SelectListItem> Languages { get; set; }
    }
}
