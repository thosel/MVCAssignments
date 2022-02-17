﻿using System.ComponentModel.DataAnnotations;

namespace MVCAssignments.ViewModels
{
    public class CreateCountryViewModel
    {
        public int Id { get; }

        [Display(Name = "Name:")]
        [Required(ErrorMessage = "A name is required.")]
        public string Name { get; set; }
    }
}