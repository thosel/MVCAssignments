using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVCAssignments.Models
{
    public class City
    {
        private City() { }

        public City(string name, Country country)
        {
            Name = name;
            Country = country;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public Country Country { get; set; }

        public List<Person> People { get; set; }
    }
}
