using System.ComponentModel.DataAnnotations;

namespace MVCAssignments.Models
{
    public class Person
    {
        public Person(string name, string phone, string city)
        {
            Name = name;
            Phone = phone;
            City = city;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string City { get; set; }
    }
}
