﻿using Microsoft.AspNetCore.Mvc;
using MVCAssignments.Models;
using MVCAssignments.Services;
using MVCAssignments.ViewModels;
using System.Collections.Generic;

namespace MVCAssignments.Controllers
{
    public class PeopleController : Controller
    {
        private readonly IPeopleService peopleService;
        private readonly PeopleViewModel peopleViewModel;

        public PeopleController(IPeopleService peopleService)
        {
            this.peopleService = peopleService;
            this.peopleViewModel = new PeopleViewModel();
            this.peopleViewModel.People = this.peopleService.Read();
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
                this.peopleViewModel.People = this.peopleService.FindPeople(searchString, caseSensitive);
            }

            return View("/Views/People/People.cshtml", this.peopleViewModel);
        }

        [HttpPost]
        public IActionResult CreatePerson(CreatePersonViewModel createPersonViewModel)
        {
            if (ModelState.IsValid)
            {
                Person person = new Person(createPersonViewModel.Name,
                    createPersonViewModel.Phone,
                    createPersonViewModel.City
                    );

                this.peopleService.CreatePerson(person);

                TempData["create-person"] = "success";
            }
            else
            {
                TempData["create-person"] = "failure";
                this.peopleViewModel.People = this.peopleService.Read();
                return View("/Views/People/People.cshtml", this.peopleViewModel);
            }

            return RedirectToAction(nameof(GetPeople), "People");
        }

        public IActionResult DeletePerson(int id)
        {
            if (this.peopleService.FindPerson(id) != null)
            {
                this.peopleService.DeletePerson(id);
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
            if (this.peopleService.FindPerson(id) == null)
            {
                TempData["get-person-details"] = "failure";
                return View("/Views/People/People.cshtml", this.peopleViewModel);
            }
            else
            {
                this.peopleViewModel.People = new List<Person>();
                this.peopleViewModel.People.Add(this.peopleService.FindPerson(id));
                TempData["get-person-details"] = "success";
            }

            return View("/Views/People/People.cshtml", this.peopleViewModel);
        }
    }
}
