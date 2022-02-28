using MVCAssignments.Models;
using MVCAssignments.Models.Data;
using System.Collections.Generic;
using System.Linq;

namespace MVCAssignments.Services
{
    public class CountriesService : ICountriesService
    {
        private readonly MVCAssignmentsContext db;

        public CountriesService(MVCAssignmentsContext db)
        {
            this.db = db;
        }

        public void CreateCountry(Country country)
        {
            db.Countries.Add(country);
            db.SaveChanges();
        }

        public List<Country> Read()
        {
            return db.Countries.ToList();
        }

        public List<Country> FindCountries(string searchString, bool caseSensitive)
        {
            List<Country> countriesToReturn = new List<Country>();
            List<Country> countries = db.Countries.Where(country =>
                country.Name.Contains(searchString)
                ).ToList();

            if (caseSensitive)
            {
                foreach (var country in countries)
                {
                    if (country.Name.Contains(searchString))
                    {
                        countriesToReturn.Add(country);
                    }
                }
            }
            else
            {
                countriesToReturn = countries;
            }

            return countriesToReturn;
        }

        public Country FindCountry(int countryId)
        {
            return db.Countries.Find(countryId);
        }

        public Country FindCountry(string countryName)
        {
            return db.Countries.FirstOrDefault(country => country.Name.Trim().ToLower() == countryName.Trim().ToLower());
        }

        public void UpdateCountry(Country country)
        {
            db.Countries.Update(FindCountry(country.Id));
            db.SaveChanges();
        }

        public void DeleteCountry(int countryId)
        {
            db.Countries.Remove(FindCountry(countryId));
            db.SaveChanges();
        }
    }
}
