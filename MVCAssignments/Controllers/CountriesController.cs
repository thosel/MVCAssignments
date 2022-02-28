using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVCAssignments.Models;
using MVCAssignments.Services;
using MVCAssignments.ViewModels;

namespace MVCAssignments.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CountriesController : Controller
    {
        private readonly ICountriesService countriesService;

        public CountriesController(ICountriesService countriesService)
        {
            this.countriesService = countriesService;
        }

        #region Create

        [HttpGet]
        public IActionResult CreateCountry()
        {
            CreateCountryViewModel createCountryViewModel = new CreateCountryViewModel();
            return View(createCountryViewModel);
        }

        [HttpPost]
        public IActionResult CreateCountry(CreateCountryViewModel createCountryViewModel)
        {
            if (ModelState.IsValid)
            {
                Country country = new Country(createCountryViewModel.Name);

                if (this.countriesService.FindCountry(country.Name) == null)
                {
                    this.countriesService.CreateCountry(country);
                    TempData["create-country"] = "success";
                }
                else
                {
                    TempData["create-country-country-existed"] = "failure";
                    return View(createCountryViewModel);
                }
            }
            else
            {
                TempData["create-country"] = "failure";
                return View(createCountryViewModel);
            }

            return RedirectToAction(nameof(Index), "Countries");
        }

        #endregion

        #region Read

        [HttpGet]
        public IActionResult Index()
        {
            CountriesViewModel countriesViewModel = new CountriesViewModel
            {
                Countries = this.countriesService.Read()
            };

            return View(countriesViewModel);
        }

        [HttpPost]
        public IActionResult Index(string searchString, bool caseSensitive = false)
        {
            CountriesViewModel countriesViewModel = new CountriesViewModel();

            if (!string.IsNullOrEmpty(searchString))
            {
                countriesViewModel.Countries = this.countriesService.FindCountries(searchString, caseSensitive);
            }
            else
            {
                return RedirectToAction(nameof(Index), "Countries");
            }

            return View(countriesViewModel);
        }

        #endregion

        #region Update

        [HttpGet]
        public IActionResult UpdateCountry(int countryId)
        {
            UpdateCountryViewModel updateCountryViewModel = new UpdateCountryViewModel();
            Country country = this.countriesService.FindCountry(countryId);

            if (country != null)
            {
                updateCountryViewModel.Id = country.Id;
                updateCountryViewModel.Name = country.Name;
            }
            else
            {
                TempData["update-country-country-non-existing"] = "failure";
                return RedirectToAction(nameof(Index), "Countries");
            }

            return View(updateCountryViewModel);
        }

        [HttpPost]
        public IActionResult UpdateCountry(UpdateCountryViewModel updateCountryViewModel)
        {
            if (ModelState.IsValid)
            {
                Country country = this.countriesService.FindCountry(updateCountryViewModel.Id);

                if (country != null)
                {
                    country.Id = updateCountryViewModel.Id;
                    country.Name = updateCountryViewModel.Name;
                    this.countriesService.UpdateCountry(country);
                    TempData["update-country"] = "success";
                }
                else
                {
                    TempData["update-country-country-non-existing"] = "failure";
                    return RedirectToAction(nameof(Index), "Countries");
                }
            }
            else
            {
                TempData["update-country"] = "failure";
                return View(updateCountryViewModel);
            }

            return RedirectToAction(nameof(Index), "Countries");
        }

        #endregion

        #region Delete

        public IActionResult DeleteCountry(int countryId)
        {
            if (this.countriesService.FindCountry(countryId) != null)
            {
                this.countriesService.DeleteCountry(countryId);
                TempData["delete-country"] = "success";
            }
            else
            {
                TempData["delete-country-country-non-existing"] = "failure";
            }

            return RedirectToAction(nameof(Index), "Countries");
        }

        #endregion
    }
}
