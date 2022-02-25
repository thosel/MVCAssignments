using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVCAssignments.ViewModels
{
    public class UpdateApplicationUserViewModel
    {
        public string UserId { get; set; }

        public string UserName { get; set; }

        [Display(Name = "User email:")]
        [Required(ErrorMessage = "An email address is required!")]
        [EmailAddress(ErrorMessage = "A valid email address is required!")]
        public string Email { get; set; }

        [Display(Name = "User roles:")]
        public Dictionary<string, bool> UserRoles { get; set; }
    }
}
