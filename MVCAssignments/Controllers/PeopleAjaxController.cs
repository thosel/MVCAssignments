using Microsoft.AspNetCore.Mvc;
using MVCAssignments.Models;
using MVCAssignments.Services;
using MVCAssignments.ViewModels;
using System.Collections.Generic;

namespace MVCAssignments.Controllers
{
    public class PeopleAjaxController : Controller
    {
        private readonly PeopleViewModel peopleViewModel;

        public PeopleAjaxController()
        {
            this.peopleViewModel = new PeopleViewModel();
            this.peopleViewModel.People = PeopleService.People;
        }

        public IActionResult GetPeople()
        {
            return View("/Views/People/People.cshtml", this.peopleViewModel);
        }

        [HttpPost]
        public IActionResult GetPeople(string searchString, bool caseSensitive = false)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                if (caseSensitive)
                {
                    this.peopleViewModel.People = PeopleService.People.FindAll(person => person.Name.Contains(searchString) || person.City.Contains(searchString));
                }
                else if (!caseSensitive)
                {
                    this.peopleViewModel.People = new List<Person>();

                    foreach (var person in PeopleService.People)
                    {
                        if (person.Name.ToLower().Contains(searchString.ToLower()) || person.City.ToLower().Contains(searchString.ToLower()))
                        {
                            this.peopleViewModel.People.Add(person);
                        }
                    }
                }
            }

            return PartialView("_PeoplePartial", this.peopleViewModel);
        }

        [HttpPost]
        public IActionResult CreatePerson(string name, string phone, string city)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(city))
            {
                string nameValidationMessage = "";
                string phoneValidationMessage = "";
                string cityValidationMessage = "";

                if (string.IsNullOrEmpty(name))
                {
                    nameValidationMessage = "A name is required.";
                }

                if (string.IsNullOrEmpty(phone))
                {
                    phoneValidationMessage = "A phone number is required.";
                }

                if (string.IsNullOrEmpty(city))
                {
                    cityValidationMessage = "A city is required.";
                }

                return Json(
                   new
                   {
                       nameValidationMessage = nameValidationMessage,
                       phoneValidationMessage = phoneValidationMessage,
                       cityValidationMessage = cityValidationMessage,
                       errorMessage = "400 Bad Request: One or more required parameters were missing."
                   }
                   );
            }

            Person person = new Person(
                    ++PeopleService.PersonCounter,
                    name,
                    phone,
                    city
                    );

            PeopleService.People.Add(person);
            this.peopleViewModel.People = PeopleService.People;

            return PartialView("_PeoplePartial", this.peopleViewModel);
        }

        public IActionResult DeletePerson(int id)
        {
            if (PeopleService.People.Find(person => person.Id == id) != null)
            {
                PeopleService.People.RemoveAll(person => person.Id == id);
                this.peopleViewModel.People = PeopleService.People;
                return StatusCode(200);
            }

            return StatusCode(400);
        }

        public IActionResult GetPersonDetails(int id)
        {
            if (PeopleService.People.Find(person => person.Id == id) == null)
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
                this.peopleViewModel.People.Add(PeopleService.People.Find(person => person.Id == id));
            }

            return PartialView("_PersonDetailsPartial", this.peopleViewModel);
        }
    }
}
