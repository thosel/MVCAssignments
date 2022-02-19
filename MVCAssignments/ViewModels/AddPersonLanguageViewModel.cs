using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVCAssignments.ViewModels
{
    public class AddPersonLanguageViewModel
    {
        [Required]
        public int PersonId { get; set; }

        [Required]
        public int LanguageId { get; set; }

        public IEnumerable<SelectListItem> Languages { get; set; }
    }
}
