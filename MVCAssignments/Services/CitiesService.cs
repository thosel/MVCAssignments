using Microsoft.EntityFrameworkCore;
using MVCAssignments.Models;
using MVCAssignments.Models.Data;
using System.Collections.Generic;
using System.Linq;

namespace MVCAssignments.Services
{
    public class CitiesService : ICitiesService
    {
        private readonly MVCAssignmentsContext db;

        public CitiesService(MVCAssignmentsContext db)
        {
            this.db = db;
        }

        public List<City> Read()
        {
            return db.Cities.Include(city => city.Country).ToList();
        }

        public List<City> FindCities(string searchString, bool caseSensitive)
        {
            List<City> citiesToReturn = new List<City>();
            List<City> cities = db.Cities.Where(city =>
                city.Name.Contains(searchString) ||
                city.Country.Name.Contains(searchString)
                ).ToList();

            if (caseSensitive)
            {
                foreach (var city in cities)
                {
                    if (city.Name.Contains(searchString) || city.Country.Name.Contains(searchString))
                    {
                        citiesToReturn.Add(city);
                    }
                }
            }
            else
            {
                citiesToReturn = cities;
            }

            return citiesToReturn;
        }

        public City FindCity(int id)
        {
            return db.Cities.Find(id);
        }

        public void CreateCity(City city)
        {
            db.Cities.Add(city);
            db.SaveChanges();
        }

        public void DeleteCity(int id)
        {
            db.Cities.Remove(FindCity(id));
            db.SaveChanges();
        }
    }
}
