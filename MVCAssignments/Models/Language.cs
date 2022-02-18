using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCAssignments.Models
{
    public class Language
    {
        private Language() { }

        public Language(string name)
        {
            Name = name;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public List<PersonLanguage> PersonLanguages { get; set; }
    }
}
