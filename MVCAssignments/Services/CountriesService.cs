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

        public Country FindCountry(int id)
        {
            return db.Countries.Find(id);
        }

        public void CreateCountry(Country country)
        {
            db.Countries.Add(country);
            db.SaveChanges();
        }

        public void DeleteCountry(int id)
        {
            db.Countries.Remove(FindCountry(id));
            db.SaveChanges();
        }
    }
}
