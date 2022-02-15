using MVCAssignments.Models;
using MVCAssignments.Models.Data;
using System.Collections.Generic;
using System.Linq;

namespace MVCAssignments.Services
{
    public class PeopleService : IPeopleService
    {
        private readonly MVCAssignmentsContext db;

        public PeopleService(MVCAssignmentsContext db)
        {
            this.db = db;
        }

        public static readonly List<Person> People = new List<Person>();

        public List<Person> Read()
        {
            return db.People.ToList();
        }

        public List<Person> FindPeople(string searchString, bool caseSensitive)
        {
            List<Person> peopleToReturn = new List<Person>();
            List<Person> people = db.People.Where(person =>
                person.Name.Contains(searchString) ||
                person.City.Contains(searchString)
                ).ToList();

            if (caseSensitive)
            {
                foreach (var person in people)
                {
                    if (person.Name.Contains(searchString) || person.City.Contains(searchString))
                    {
                        peopleToReturn.Add(person);
                    }
                }
            }
            else
            {
                peopleToReturn = people;
            }

            return peopleToReturn;
        }

        public Person FindPerson(int id)
        {
            return db.People.Find(id);
        }

        public void CreatePerson(Person person)
        {
            db.People.Add(person);
            db.SaveChanges();
        }

        public void DeletePerson(int id)
        {
            db.People.Remove(FindPerson(id));
            db.SaveChanges();
        }
    }
}
