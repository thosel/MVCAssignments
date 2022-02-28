using System.ComponentModel.DataAnnotations;

namespace MVCAssignments.ViewModels
{
    public class CreateLanguageViewModel
    {
        public int Id { get; }

        [Display(Name = "Language name:")]
        [Required(ErrorMessage = "A language name is required.")]
        public string Name { get; set; }
    }
}
