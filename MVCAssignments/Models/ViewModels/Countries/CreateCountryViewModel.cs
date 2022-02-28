using System.ComponentModel.DataAnnotations;

namespace MVCAssignments.ViewModels
{
    public class CreateCountryViewModel
    {
        public int Id { get; }

        [Display(Name = "Country name:")]
        [Required(ErrorMessage = "A country name is required.")]
        public string Name { get; set; }
    }
}
