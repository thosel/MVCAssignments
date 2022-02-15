using Microsoft.AspNetCore.Mvc;
using MVCAssignments.Models;
using MVCAssignments.Services;
using MVCAssignments.ViewModels;
using System.Collections.Generic;

namespace MVCAssignments.Controllers
{
    public class PeopleAjaxController : Controller
    {
        private readonly IPeopleService peopleService;
        private readonly PeopleViewModel peopleViewModel;

        public PeopleAjaxController(IPeopleService peopleService)
        {
            this.peopleService = peopleService;
            this.peopleViewModel = new PeopleViewModel();
            this.peopleViewModel.People = this.peopleService.Read();
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
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(city))
            {
                return Json(
                   new
                   {
                       nameValidationMessage = string.IsNullOrEmpty(name) ? "A name is required." : "",
                       phoneValidationMessage = string.IsNullOrEmpty(phone) ? "A phone number is required." : "",
                       cityValidationMessage = string.IsNullOrEmpty(city) ? "A city is required." : "",
                       statusMessage = "400 Bad Request: One or more required parameters were missing."
                   }
                   );
            }

            Person person = new Person(name, phone, city);

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
                this.peopleViewModel.People = new List<Person>();
                this.peopleViewModel.People.Add(this.peopleService.FindPerson(id));
            }

            return PartialView("_PersonDetailsPartial", this.peopleViewModel);
        }
    }
}
