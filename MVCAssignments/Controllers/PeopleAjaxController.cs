using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVCAssignments.Models;
using MVCAssignments.Services;
using MVCAssignments.ViewModels;
using System.Collections.Generic;

namespace MVCAssignments.Controllers
{
    public class PeopleAjaxController : Controller
    {
        private readonly IPeopleService peopleService;
        private readonly ICitiesService citiesService;
        private readonly ILanguagesService languagesService;
        private readonly PeopleViewModel peopleViewModel;

        public PeopleAjaxController(IPeopleService peopleService, ICitiesService citiesService, ILanguagesService languagesService)
        {
            this.peopleService = peopleService;
            this.citiesService = citiesService;
            this.languagesService = languagesService;

            this.peopleViewModel = new PeopleViewModel
            {
                CreatePersonViewModel = new CreatePersonViewModel
                {
                    Cities = new SelectList(this.citiesService.Read(), "Id", "Name")
                },

                AddPersonLanguageViewModel = new AddPersonLanguageViewModel
                {
                    Languages = new SelectList(this.languagesService.Read(), "Id", "Name")
                },

                People = this.peopleService.Read()
            };
        }

        [HttpPost]
        public IActionResult GetPeople(string searchString, bool caseSensitive = false)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                this.peopleViewModel.People = this.peopleService.FindPeople(searchString, caseSensitive);
            }

            return PartialView("_PeoplePartial", this.peopleViewModel);
        }

        [HttpPost]
        public IActionResult CreatePerson(string name, string phone, string city)
        {
            bool isCityIdValid = int.TryParse(city, out int cityId);

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(phone) || !isCityIdValid)
            {
                return Json(
                   new
                   {
                       nameValidationMessage = string.IsNullOrEmpty(name) ? "A name is required." : "",
                       phoneValidationMessage = string.IsNullOrEmpty(phone) ? "A phone number is required." : "",
                       cityValidationMessage = this.citiesService.FindCity(cityId) == null ? "A city is required." : "",
                       statusMessage = "400 Bad Request: One or more required parameters were missing."
                   }
                   );
            }

            Person person = new Person(name, phone, this.citiesService.FindCity(cityId));

            this.peopleService.CreatePerson(person);
            this.peopleViewModel.People = this.peopleService.Read();

            return PartialView("_PeoplePartial", this.peopleViewModel);
        }

        public IActionResult DeletePerson(int id)
        {
            if (this.peopleService.FindPerson(id) != null)
            {
                this.peopleService.DeletePerson(id);
                this.peopleViewModel.People = this.peopleService.Read();
                return StatusCode(200);
            }

            return StatusCode(400);
        }

        public IActionResult GetPersonDetails(int id)
        {
            if (this.peopleService.FindPerson(id) == null)
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
                this.peopleViewModel.People = new List<Person>
                {
                    this.peopleService.FindPerson(id)
                };
            }

            return PartialView("_PersonDetailsPartial", this.peopleViewModel);
        }

        public IActionResult AddPersonLanguage(int personId, int languageId)
        {
            if (this.peopleService.FindPerson(personId) == null ||
                this.languagesService.FindLanguage(languageId) == null ||
                this.peopleService.FindPersonLanguage(personId, languageId) != null)
            {
                return Json(
                   new
                   {
                       errorMessage = "400 Bad Request: One or more parameters are not valid."
                   }
                   );
            }

            this.peopleService.AddPersonLanguage(personId, languageId);

            this.peopleViewModel.People = new List<Person>
                {
                    this.peopleService.FindPerson(personId)
                };

            return PartialView("_PersonDetailsPartial", this.peopleViewModel);
        }

        public IActionResult DeletePersonLanguage(int personId, int languageId)
        {
            if (this.peopleService.FindPersonLanguage(personId, languageId) != null)
            {
                this.peopleService.DeletePersonLanguage(personId, languageId);
            }
            else
            {
                return Json(
                   new
                   {
                       errorMessage = "400 Bad Request: One or more parameters are not valid."
                   }
                   );
            }

            this.peopleViewModel.People = new List<Person>
                {
                    this.peopleService.FindPerson(personId)
                };


            return PartialView("_PersonDetailsPartial", this.peopleViewModel);
        }
    }
}
