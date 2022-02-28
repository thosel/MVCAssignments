using MVCAssignments.Models;
using System.Collections.Generic;

namespace MVCAssignments.Services
{
    public interface ICitiesService
    {
        public void CreateCity(City city);

        public List<City> Read();

        public List<City> FindCities(string searchString, bool caseSensitive);

        public City FindCity(int cityId);

        public City FindCity(string cityName);

        public List<int> GetCountryCityIds(int countryId);

        public void UpdateCity(City city);

        public void DeleteCity(int cityId);
    }
}
