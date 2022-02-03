using Microsoft.AspNetCore.Mvc;
using MVCAssignments.Models;
using MVCAssignments.ViewModels;
using System.Collections.Generic;

namespace MVCAssignments.Controllers
{
    public class PeopleController : Controller
    {
        private readonly PeopleViewModel peopleViewModel;
        private static readonly List<Person> people = new List<Person>();
        private static int personCounter = 0;

        public PeopleController()
        {
            this.peopleViewModel = new PeopleViewModel();
            this.peopleViewModel.People = PeopleController.people;
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
                    this.peopleViewModel.People = PeopleController.people.FindAll(person => person.Name.Contains(searchString) || person.City.Contains(searchString));
                }
                else if (!caseSensitive)
                {
                    this.peopleViewModel.People = new List<Person>();

                    foreach (var person in PeopleController.people)
                    {
                        if (person.Name.ToLower().Contains(searchString.ToLower()) || person.City.ToLower().Contains(searchString.ToLower()))
                        {
                            this.peopleViewModel.People.Add(person);
                        }
                    }
                }
            }

            return View("/Views/People/People.cshtml", this.peopleViewModel);
        }

        [HttpPost]
        public IActionResult CreatePerson(CreatePersonViewModel createPersonViewModel)
        {
            if (ModelState.IsValid)
            {
                Person person = new Person(
                    ++PeopleController.personCounter,
                    createPersonViewModel.Name,
                    createPersonViewModel.Phone,
                    createPersonViewModel.City
                    );

                PeopleController.people.Add(person);
                this.peopleViewModel.People = PeopleController.people;

                TempData["create-person"] = "success";
            }
            else
            {
                TempData["create-person"] = "failure";
            }

            return View("/Views/People/People.cshtml", this.peopleViewModel);
        }

        public IActionResult DeletePerson(int id)
        {
            if (PeopleController.people.Find(person => person.Id == id) != null)
            {
                PeopleController.people.RemoveAll(person => person.Id == id);
                this.peopleViewModel.People = PeopleController.people;
                TempData["delete-person"] = "success";
            }
            else
            {
                TempData["delete-person"] = "failure";
            }

            return View("/Views/People/People.cshtml", this.peopleViewModel);
        }
    }
}
