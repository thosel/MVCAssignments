using System.ComponentModel.DataAnnotations;

namespace MVCAssignments.ViewModels
{
    public class UpdateCountryViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Country name:")]
        [Required(ErrorMessage = "A country name is required.")]
        public string Name { get; set; }
    }
}
