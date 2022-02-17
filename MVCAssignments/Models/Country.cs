using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVCAssignments.Models
{
    public class Country
    {
        private Country() { }

        public Country(string name)
        {
            Name = name;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public List<City> Cities { get; set; }
    }
}
