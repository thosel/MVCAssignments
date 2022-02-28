using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVCAssignments.Models;
using MVCAssignments.Services;
using MVCAssignments.ViewModels;

namespace MVCAssignments.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CitiesController : Controller
    {
        private readonly ICitiesService citiesService;
        private readonly ICountriesService countriesService;

        public CitiesController(ICitiesService citiesService, ICountriesService countriesService)
        {
            this.citiesService = citiesService;
            this.countriesService = countriesService;
        }

        #region Create

        [HttpGet]
        public IActionResult CreateCity()
        {
            CreateCityViewModel createCityViewModel = new CreateCityViewModel();
            createCityViewModel.Countries = new SelectList(this.countriesService.Read(), "Id", "Name");
            return View(createCityViewModel);
        }

        [HttpPost]
        public IActionResult CreateCity(CreateCityViewModel createCityViewModel)
        {
            if (
                ModelState.IsValid &&
                int.TryParse(createCityViewModel.CountryId, out int countryId))
            {
                Country country = this.countriesService.FindCountry(countryId);

                if (country != null)
                {
                    City city = new City(
                    createCityViewModel.Name,
                    country
                    );

                    this.citiesService.CreateCity(city);
                    TempData["create-city"] = "success";
                }
                else
                {
                    TempData["create-city-country-non-existing"] = "failure";
                    return View(createCityViewModel);
                }
            }
            else
            {
                TempData["create-city"] = "failure";
                createCityViewModel.Countries = new SelectList(this.countriesService.Read(), "Id", "Name");
                return View(createCityViewModel);
            }

            return RedirectToAction(nameof(Index), "Cities");
        }

        #endregion

        #region Read

        [HttpGet]
        public IActionResult Index()
        {
            CitiesViewModel citiesViewModel = new CitiesViewModel
            {
                Cities = this.citiesService.Read()
            };

            return View(citiesViewModel);
        }

        [HttpPost]
        public IActionResult Index(string searchString, bool caseSensitive = false)
        {
            CitiesViewModel citiesViewModel = new CitiesViewModel();

            if (!string.IsNullOrEmpty(searchString))
            {
                citiesViewModel.Cities = this.citiesService.FindCities(searchString, caseSensitive);
            }
            else
            {
                return RedirectToAction(nameof(Index), "Cities");
            }

            return View(citiesViewModel);
        }

        #endregion

        #region Update

        [HttpGet]
        public IActionResult UpdateCity(int cityId)
        {
            UpdateCityViewModel updateCityViewModel = new UpdateCityViewModel();
            City city = this.citiesService.FindCity(cityId);

            if (city != null)
            {
                updateCityViewModel.Id = city.Id;
                updateCityViewModel.Name = city.Name;
                updateCityViewModel.Countries = new SelectList(this.countriesService.Read(), "Id", "Name", city.Country.Id);
            }
            else
            {
                TempData["update-city-city-non-existing"] = "failure";
                return RedirectToAction(nameof(Index), "Cities");
            }

            return View(updateCityViewModel);
        }

        [HttpPost]
        public IActionResult UpdateCity(UpdateCityViewModel updateCityViewModel)
        {
            if (
                ModelState.IsValid &&
                int.TryParse(updateCityViewModel.CountryId, out int countryId))
            {
                Country country = this.countriesService.FindCountry(countryId);

                if (country != null)
                {
                    City city = this.citiesService.FindCity(updateCityViewModel.Id);

                    if (city != null)
                    {
                        city.Id = updateCityViewModel.Id;
                        city.Name = updateCityViewModel.Name;
                        city.Country = country;
                        this.citiesService.UpdateCity(city);
                        TempData["update-city"] = "success";
                    }
                    else
                    {
                        TempData["update-city-city-non-existing"] = "failure";
                        updateCityViewModel.Countries = new SelectList(this.countriesService.Read(), "Id", "Name");
                        return RedirectToAction(nameof(Index), "Cities");
                    }
                }
                else
                {
                    TempData["create-city-country-non-existing"] = "failure";
                    updateCityViewModel.Countries = new SelectList(this.countriesService.Read(), "Id", "Name");
                    return RedirectToAction(nameof(Index), "Cities");
                }
            }
            else
            {
                TempData["update-city"] = "failure";
                updateCityViewModel.Countries = new SelectList(this.countriesService.Read(), "Id", "Name");
                return View(updateCityViewModel);
            }

            return RedirectToAction(nameof(Index), "Cities");
        }

        #endregion

        #region Delete

        public IActionResult DeleteCity(int cityId)
        {
            if (this.citiesService.FindCity(cityId) != null)
            {
                this.citiesService.DeleteCity(cityId);
                TempData["delete-city"] = "success";
            }
            else
            {
                TempData["delete-city-city-non-existing"] = "failure";
            }

            return RedirectToAction(nameof(Index), "Cities");
        }

        #endregion
    }
}
