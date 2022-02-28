using System.ComponentModel.DataAnnotations;

namespace MVCAssignments.ViewModels
{
    public class UpdateLanguageViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Language name:")]
        [Required(ErrorMessage = "A language name is required.")]
        public string Name { get; set; }
    }
}
