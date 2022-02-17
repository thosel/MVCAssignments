using MVCAssignments.Models;
using System.Collections.Generic;

namespace MVCAssignments.Services
{
    public interface ICountriesService
    {
        public List<Country> Read();

        public List<Country> FindCountries(string searchString, bool caseSensitive);

        public Country FindCountry(int id);

        public void CreateCountry(Country country);

        public void DeleteCountry(int id);
    }
}
