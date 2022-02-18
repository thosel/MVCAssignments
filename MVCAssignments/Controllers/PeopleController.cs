using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVCAssignments.Models;
using MVCAssignments.Services;
using MVCAssignments.ViewModels;
using System.Collections.Generic;

namespace MVCAssignments.Controllers
{
    public class PeopleController : Controller
    {
        private readonly IPeopleService peopleService;
        private readonly ICitiesService citiesService;
        private readonly ILanguagesService languagesService;
        private readonly PeopleViewModel peopleViewModel;

        public PeopleController(IPeopleService peopleService, ICitiesService citiesService, ILanguagesService languagesService)
        {
            this.peopleService = peopleService;
            this.citiesService = citiesService;
            this.languagesService = languagesService;

            this.peopleViewModel = new PeopleViewModel
            {
                CreatePersonViewModel = new CreatePersonViewModel()
            };
            this.peopleViewModel.CreatePersonViewModel.Cities = new SelectList(this.citiesService.Read(), "Id", "Name");

            this.peopleViewModel.CreateLanguageViewModel = new CreateLanguageViewModel
            {
                Languages = new SelectList(this.languagesService.Read(), "Id", "Name")
            };

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
            if (ModelState.IsValid && int.TryParse(createPersonViewModel.City, out int id))
            {
                Person person = new Person(
                    createPersonViewModel.Name,
                    createPersonViewModel.Phone,
                    this.citiesService.FindCity(id)
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
                this.peopleViewModel.People = new List<Person>
                {
                    this.peopleService.FindPerson(id)
                };
                TempData["get-person-details"] = "success";
            }

            return View("/Views/People/People.cshtml", this.peopleViewModel);
        }
    }
}
