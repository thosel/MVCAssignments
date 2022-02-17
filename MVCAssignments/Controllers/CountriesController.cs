using Microsoft.AspNetCore.Mvc;
using MVCAssignments.Models;
using MVCAssignments.Services;
using MVCAssignments.ViewModels;
using System.Collections.Generic;

namespace MVCAssignments.Controllers
{
    public class CountriesController : Controller
    {
        private readonly ICountriesService countriesService;
        private readonly CountriesViewModel countriesViewModel;

        public CountriesController(ICountriesService countriesService)
        {
            this.countriesService = countriesService;

            this.countriesViewModel = new CountriesViewModel
            {
                CreateCountryViewModel = new CreateCountryViewModel(),

                Countries = this.countriesService.Read()
            };
        }

        public IActionResult GetCountries()
        {
            return View("/Views/Countries/Countries.cshtml", this.countriesViewModel);
        }

        [HttpPost]
        public IActionResult GetCountries(string searchString, bool caseSensitive = false)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                this.countriesViewModel.Countries = this.countriesService.FindCountries(searchString, caseSensitive);
            }

            return View("/Views/Countries/Countries.cshtml", this.countriesViewModel);
        }

        [HttpPost]
        public IActionResult CreateCountry(CreateCountryViewModel createCountryViewModel)
        {
            if (ModelState.IsValid)
            {
                Country country = new Country(createCountryViewModel.Name);

                this.countriesService.CreateCountry(country);

                TempData["create-country"] = "success";
            }
            else
            {
                TempData["create-country"] = "failure";
                this.countriesViewModel.Countries = this.countriesService.Read();
                return View("/Views/Countries/Countries.cshtml", this.countriesViewModel);
            }

            return RedirectToAction(nameof(GetCountries), "Countries");
        }

        public IActionResult DeleteCountry(int id)
        {
            if (this.countriesService.FindCountry(id) != null)
            {
                this.countriesService.DeleteCountry(id);
                TempData["delete-country"] = "success";
            }
            else
            {
                TempData["delete-country"] = "failure";
            }

            return RedirectToAction(nameof(GetCountries), "Countries");
        }

        public IActionResult GetCountryDetails(int id)
        {
            if (this.countriesService.FindCountry(id) == null)
            {
                TempData["get-country-details"] = "failure";
                return View("/Views/Countries/Countries.cshtml", this.countriesViewModel);
            }
            else
            {
                this.countriesViewModel.Countries = new List<Country>
                {
                    this.countriesService.FindCountry(id)
                };
                TempData["get-country-details"] = "success";
            }

            return View("/Views/Countries/Countries.cshtml", this.countriesViewModel);
        }
    }
}
