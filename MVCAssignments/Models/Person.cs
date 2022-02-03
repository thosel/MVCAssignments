namespace MVCAssignments.Models
{
    public class Person
    {
        private readonly int id;
        public Person(int id, string name, string phone, string city)
        {
            this.id = id;
            Name = name;
            Phone = phone;
            City = city;
        }

        public int Id
        {
            get { return this.id; }
        }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string City { get; set; }
    }
}
