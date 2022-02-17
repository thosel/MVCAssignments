using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVCAssignments.ViewModels
{
    public class CreateCityViewModel
    {
        public int Id { get; }

        [Display(Name = "Name:")]
        [Required(ErrorMessage = "A name is required.")]
        public string Name { get; set; }

        [Display(Name = "Country:")]
        [Required(ErrorMessage = "A country is required.")]
        public string Country { get; set; }

        public IEnumerable<SelectListItem> Countries { get; set; }
    }
}
