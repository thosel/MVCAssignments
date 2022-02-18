using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVCAssignments.ViewModels
{
    public class CreateLanguageViewModel
    {
        public int Id { get; }

        [Display(Name = "Name:")]
        [Required(ErrorMessage = "A name is required.")]
        public string Name { get; set; }

        public IEnumerable<SelectListItem> Languages { get; set; }
    }
}
