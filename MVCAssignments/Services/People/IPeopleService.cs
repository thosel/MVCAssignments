using MVCAssignments.Models;
using System.Collections.Generic;

namespace MVCAssignments.Services
{
    public interface IPeopleService
    {
        public int CreatePerson(Person person);

        public void CreatePersonLanguage(int personId, int languageId);

        public List<Person> Read();

        public List<Person> FindPeople(string searchString, bool caseSensitive);

        public Person FindPerson(int personId);

        public Person FindPerson(string personName);

        public PersonLanguage FindPersonLanguage(int personId, int languageId);

        public void UpdatePerson(Person person);

        public void DeletePerson(int personId);

        public void DeletePersonLanguage(int personId, int languageId);
    }
}
