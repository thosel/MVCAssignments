﻿using Microsoft.AspNetCore.Mvc;
using MVCAssignments.Models;
using MVCAssignments.Services;
using MVCAssignments.ViewModels;
using System.Collections.Generic;

namespace MVCAssignments.Controllers
{
    public class PeopleController : Controller
    {
        private readonly PeopleViewModel peopleViewModel;

        public PeopleController()
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

            return View("/Views/People/People.cshtml", this.peopleViewModel);
        }

        [HttpPost]
        public IActionResult CreatePerson(CreatePersonViewModel createPersonViewModel)
        {
            if (ModelState.IsValid)
            {
                Person person = new Person(
                    ++PeopleService.PersonCounter,
                    createPersonViewModel.Name,
                    createPersonViewModel.Phone,
                    createPersonViewModel.City
                    );

                PeopleService.People.Add(person);

                TempData["create-person"] = "success";
            }
            else
            {
                TempData["create-person"] = "failure";
                this.peopleViewModel.People = PeopleService.People;
                return View("/Views/People/People.cshtml", this.peopleViewModel);
            }

            return RedirectToAction(nameof(GetPeople), "People");
        }

        public IActionResult DeletePerson(int id)
        {
            if (PeopleService.People.Find(person => person.Id == id) != null)
            {
                PeopleService.People.RemoveAll(person => person.Id == id);
                TempData["delete-person"] = "success";
            }
            else
            {
                TempData["delete-person"] = "failure";
            }

            return RedirectToAction(nameof(GetPeople), "People");
        }
        public IActionResult GetPersonDetails(int id)
        {
            if (PeopleService.People.Find(person => person.Id == id) == null)
            {
                TempData["get-person-details"] = "failure";
                return View("/Views/People/People.cshtml", this.peopleViewModel);
            }
            else
            {
                this.peopleViewModel.People = new List<Person>();
                this.peopleViewModel.People.Add(PeopleService.People.Find(person => person.Id == id));
                TempData["get-person-details"] = "success";
            }

            return View("/Views/People/People.cshtml", this.peopleViewModel);
        }
    }
}
