using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVCAssignments.Models;
using MVCAssignments.Services;
using MVCAssignments.ViewModels;
using System.Collections.Generic;

namespace MVCAssignments.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CountriesAjaxController : Controller
    {
        private readonly ICountriesService countriesService;
        private readonly CountriesViewModel countriesViewModel;

        public CountriesAjaxController(ICountriesService countriesService)
        {
            this.countriesService = countriesService;

            this.countriesViewModel = new CountriesViewModel
            {
                CreateCountryViewModel = new CreateCountryViewModel(),

                Countries = this.countriesService.Read()
            };
        }

        [HttpPost]
        public IActionResult GetCountries(string searchString, bool caseSensitive = false)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                this.countriesViewModel.Countries = this.countriesService.FindCountries(searchString, caseSensitive);
            }

            return PartialView("_CountriesPartial", this.countriesViewModel);
        }

        [HttpPost]
        public IActionResult CreateCountry(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return Json(
                   new
                   {
                       nameValidationMessage = string.IsNullOrEmpty(name) ? "A name is required." : "",
                       statusMessage = "400 Bad Request: One or more required parameters were missing."
                   }
                   );
            }

            Country country = new Country(name);

            this.countriesService.CreateCountry(country);
            this.countriesViewModel.Countries = this.countriesService.Read();

            return PartialView("_CountriesPartial", this.countriesViewModel);
        }

        public IActionResult DeleteCountry(int id)
        {
            if (this.countriesService.FindCountry(id) != null)
            {
                this.countriesService.DeleteCountry(id);
                this.countriesViewModel.Countries = this.countriesService.Read();
                return StatusCode(200);
            }

            return StatusCode(400);
        }

        public IActionResult GetCountryDetails(int id)
        {
            if (this.countriesService.FindCountry(id) == null)
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
                this.countriesViewModel.Countries = new List<Country>
                {
                    this.countriesService.FindCountry(id)
                };
            }

            return PartialView("_CountryDetailsPartial", this.countriesViewModel);
        }
    }
}
