using MVCAssignments.Models;
using System.Collections.Generic;

namespace MVCAssignments.Services
{
    public interface ICitiesService
    {
        public List<City> Read();

        public List<City> FindCities(string searchString, bool caseSensitive);

        public City FindCity(int id);

        public void CreateCity(City city);

        public void DeleteCity(int id);
    }
}
