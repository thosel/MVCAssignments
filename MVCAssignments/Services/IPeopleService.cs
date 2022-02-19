using MVCAssignments.Models;
using System.Collections.Generic;

namespace MVCAssignments.Services
{
    public interface IPeopleService
    {
        public List<Person> Read();

        public List<Person> FindPeople(string searchString, bool caseSensitive);

        public Person FindPerson(int id);

        public void CreatePerson(Person person);

        public void DeletePerson(int id);

        public void AddPersonLanguage(int personId, int languageId);

        public PersonLanguage FindPersonLanguage(int personId, int languageId);

        public void DeletePersonLanguage(int personId, int languageId);
    }
}
