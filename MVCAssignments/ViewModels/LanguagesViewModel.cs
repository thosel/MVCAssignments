using MVCAssignments.Models;
using System.Collections.Generic;

namespace MVCAssignments.ViewModels
{
    public class LanguagesViewModel
    {
        public CreateLanguageViewModel CreateLanguageViewModel { get; set; }

        public List<Language> Languages { get; set; }
    }
}
