using Microsoft.EntityFrameworkCore;
using MVCAssignments.Models;
using MVCAssignments.Models.Data;
using System;
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

        public int CreatePerson(Person person)
        {
            db.People.Add(person);
            db.SaveChanges();
            db.Entry(person).GetDatabaseValues();
            return person.Id;
        }

        public void CreatePersonLanguage(int personId, int languageId)
        {
            db.PersonLanguages.Add(new PersonLanguage(personId, languageId));
            db.SaveChanges();
        }

        public List<Person> Read()
        {
            return db.People
                .Include(person => person.City)
                .ThenInclude(person => person.Country)
                .Include(person => person.PersonLanguages)
                .ThenInclude(person => person.Language)
                .ToList();
        }

        public List<Person> FindPeople(string searchString, bool caseSensitive)
        {
            List<Person> peopleToReturn = new List<Person>();
            List<Person> people = db.People.Where(person =>
                person.Name.Contains(searchString) ||
                person.City.Name.Contains(searchString)
                )
                .Include(person => person.City)
                .ThenInclude(person => person.Country)
                .Include(person => person.PersonLanguages)
                .ThenInclude(person => person.Language)
                .ToList();

            if (caseSensitive)
            {
                foreach (var person in people)
                {
                    if (person.Name.Contains(searchString) || person.City.Name.Contains(searchString))
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

        public Person FindPerson(int personId)
        {
            try
            {
                return db.People.Where(person => person.Id == personId)
                    .Include(person => person.City)
                    .ThenInclude(city => city.Country)
                    .Include(person => person.PersonLanguages)
                .ToList()[0];
            }
            catch (ArgumentNullException)
            {
                return null;
            }
        }

        public Person FindPerson(string personName)
        {
            return db.People.FirstOrDefault(person => person.Name.Trim().ToLower() == personName.Trim().ToLower());
        }

        public PersonLanguage FindPersonLanguage(int personId, int languageId)
        {
            return db.PersonLanguages.Find(personId, languageId);
        }

        public void UpdatePerson(Person person)
        {
            Person personToUpdate = FindPerson(person.Id);
            if (personToUpdate != null)
            {
                personToUpdate.Name = person.Name;
                personToUpdate.Phone = person.Phone;
                personToUpdate.City = person.City;

                db.People.Update(personToUpdate);
                db.SaveChanges();
            }
        }

        public void DeletePerson(int personId)
        {
            db.People.Remove(FindPerson(personId));
            db.SaveChanges();
        }

        public void DeletePersonLanguage(int personId, int languageId)
        {
            db.PersonLanguages.Remove(FindPersonLanguage(personId, languageId));
            db.SaveChanges();
        }
    }
}
