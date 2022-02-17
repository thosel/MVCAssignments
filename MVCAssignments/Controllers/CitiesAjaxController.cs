using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVCAssignments.Models;
using MVCAssignments.Services;
using MVCAssignments.ViewModels;
using System.Collections.Generic;

namespace MVCAssignments.Controllers
{
    public class CitiesAjaxController : Controller
    {
        private readonly ICitiesService citiesService;
        private readonly ICountriesService countriesService;
        private readonly CitiesViewModel citiesViewModel;

        public CitiesAjaxController(ICitiesService citiesService, ICountriesService countriesService)
        {
            this.citiesService = citiesService;
            this.countriesService = countriesService;

            this.citiesViewModel = new CitiesViewModel();

            this.citiesViewModel.CreateCityViewModel = new CreateCityViewModel();
            this.citiesViewModel.CreateCityViewModel.Countries = new SelectList(this.countriesService.Read(), "Id", "Name");

            this.citiesViewModel.Cities = this.citiesService.Read();
        }

        [HttpPost]
        public IActionResult GetCities(string searchString, bool caseSensitive = false)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                this.citiesViewModel.Cities = this.citiesService.FindCities(searchString, caseSensitive);
            }

            return PartialView("_CitiesPartial", this.citiesViewModel);
        }

        [HttpPost]
        public IActionResult CreateCity(string name, string country)
        {
            bool isCountryIdValid = int.TryParse(country, out int countryId);

            if (string.IsNullOrEmpty(name) || !isCountryIdValid)
            {
                return Json(
                   new
                   {
                       nameValidationMessage = string.IsNullOrEmpty(name) ? "A name is required." : "",
                       countryValidationMessage = this.countriesService.FindCountry(countryId) == null ? "A country is required." : "",
                       statusMessage = "400 Bad Request: One or more required parameters were missing."
                   }
                   );
            }

            City city = new City(name, this.countriesService.FindCountry(countryId));

            this.citiesService.CreateCity(city);
            this.citiesViewModel.Cities = this.citiesService.Read();

            return PartialView("_CitiesPartial", this.citiesViewModel);
        }

        public IActionResult DeleteCity(int id)
        {
            if (this.citiesService.FindCity(id) != null)
            {
                this.citiesService.DeleteCity(id);
                this.citiesViewModel.Cities = this.citiesService.Read();
                return StatusCode(200);
            }

            return StatusCode(400);
        }

        public IActionResult GetCityDetails(int id)
        {
            if (this.citiesService.FindCity(id) == null)
            {
                return Json(
                   new
                   {
                       errorMessage = "400 Bad Request: Id parameter not valid."
                   }
                   );
            }
            else
            {
                this.citiesViewModel.Cities = new List<City>();
                this.citiesViewModel.Cities.Add(this.citiesService.FindCity(id));
            }

            return PartialView("_CityDetailsPartial", this.citiesViewModel);
        }
    }
}
