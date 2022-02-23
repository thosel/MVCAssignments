using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVCAssignments.Models;
using MVCAssignments.Services;
using MVCAssignments.ViewModels;
using System.Collections.Generic;

namespace MVCAssignments.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CitiesController : Controller
    {
        private readonly ICitiesService citiesService;
        private readonly ICountriesService countriesService;
        private readonly CitiesViewModel citiesViewModel;

        public CitiesController(ICitiesService citiesService, ICountriesService countriesService)
        {
            this.citiesService = citiesService;
            this.countriesService = countriesService;

            this.citiesViewModel = new CitiesViewModel();

            this.citiesViewModel.CreateCityViewModel = new CreateCityViewModel();
            this.citiesViewModel.CreateCityViewModel.Countries = new SelectList(this.countriesService.Read(), "Id", "Name");

            this.citiesViewModel.Cities = this.citiesService.Read();
        }

        public IActionResult GetCities()
        {
            return View("/Views/Cities/Cities.cshtml", this.citiesViewModel);
        }

        [HttpPost]
        public IActionResult GetCities(string searchString, bool caseSensitive = false)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                this.citiesViewModel.Cities = this.citiesService.FindCities(searchString, caseSensitive);
            }

            return View("/Views/Cities/Cities.cshtml", this.citiesViewModel);
        }

        [HttpPost]
        public IActionResult CreateCity(CreateCityViewModel createCityViewModel)
        {
            if (ModelState.IsValid && int.TryParse(createCityViewModel.Country, out int id))
            {
                City city = new City(
                    createCityViewModel.Name,
                    this.countriesService.FindCountry(id)
                    );

                this.citiesService.CreateCity(city);

                TempData["create-city"] = "success";
            }
            else
            {
                TempData["create-city"] = "failure";
                this.citiesViewModel.Cities = this.citiesService.Read();
                return View("/Views/Cities/Cities.cshtml", this.citiesViewModel);
            }

            return RedirectToAction(nameof(GetCities), "Cities");
        }

        public IActionResult DeleteCity(int id)
        {
            if (this.citiesService.FindCity(id) != null)
            {
                this.citiesService.DeleteCity(id);
                TempData["delete-city"] = "success";
            }
            else
            {
                TempData["delete-city"] = "failure";
            }

            return RedirectToAction(nameof(GetCities), "Cities");
        }

        public IActionResult GetCityDetails(int id)
        {
            if (this.citiesService.FindCity(id) == null)
            {
                TempData["get-city-details"] = "failure";
                return View("/Views/Cities/Cities.cshtml", this.citiesViewModel);
            }
            else
            {
                this.citiesViewModel.Cities = new List<City>();
                this.citiesViewModel.Cities.Add(this.citiesService.FindCity(id));
                TempData["get-city-details"] = "success";
            }

            return View("/Views/Cities/Cities.cshtml", this.citiesViewModel);
        }
    }
}
