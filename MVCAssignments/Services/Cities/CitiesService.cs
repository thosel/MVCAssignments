using Microsoft.EntityFrameworkCore;
using MVCAssignments.Models;
using MVCAssignments.Models.Data;
using System;
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

        public void CreateCity(City city)
        {
            db.Cities.Add(city);
            db.SaveChanges();
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

        public City FindCity(int cityId)
        {
            try
            {
                return db.Cities.Where(city => city.Id == cityId)
                .Include(city => city.Country)
                .ToList()[0];
            }
            catch (ArgumentNullException)
            {
                return null;
            }
        }

        public City FindCity(string cityName)
        {
            return db.Cities.FirstOrDefault(city => city.Name.Trim().ToLower() == cityName.Trim().ToLower());
        }

        public List<int> GetCountryCityIds(int countryId)
        {
            List<int> cityIds = new List<int>();

            List<City> cities = db.Cities.Where(city => city.Country.Id == countryId).ToList();

            foreach (var city in cities)
            {
                cityIds.Add(city.Id);
            }

            return cityIds;
        }

        public void UpdateCity(City city)
        {
            db.Cities.Update(FindCity(city.Id));
            db.SaveChanges();
        }

        public void DeleteCity(int cityId)
        {
            db.Cities.Remove(FindCity(cityId));
            db.SaveChanges();
        }
    }
}
