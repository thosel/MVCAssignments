using System.ComponentModel.DataAnnotations;

namespace MVCAssignments.ViewModels
{
    public class CreatePersonViewModel
    {
        public int Id { get; }

        [Display(Name = "Name:")]
        [Required(ErrorMessage = "A name is required.")]
        public string Name { get; set; }

        [Display(Name = "Phone number:")]
        [Required(ErrorMessage = "A phone number is required.")]
        public string Phone { get; set; }

        [Display(Name = "City:")]
        [Required(ErrorMessage = "A city is required.")]
        public string City { get; set; }
    }
}
