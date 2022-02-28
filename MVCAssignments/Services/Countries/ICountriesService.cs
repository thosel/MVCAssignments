using MVCAssignments.Models;
using System.Collections.Generic;

namespace MVCAssignments.Services
{
    public interface ICountriesService
    {
        public void CreateCountry(Country country);

        public List<Country> Read();

        public List<Country> FindCountries(string searchString, bool caseSensitive);

        public Country FindCountry(int countryId);

        public Country FindCountry(string countryName);

        public void UpdateCountry(Country country);

        public void DeleteCountry(int countryId);
    }
}
