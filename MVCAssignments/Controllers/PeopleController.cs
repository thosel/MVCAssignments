using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVCAssignments.Models;
using MVCAssignments.Services;
using MVCAssignments.ViewModels;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace MVCAssignments.Controllers
{
    [Authorize(Roles = "Admin, User")]
    public class PeopleController : Controller
    {
        private readonly IPeopleService peopleService;
        private readonly ICitiesService citiesService;
        private readonly ICountriesService countriesService;
        private readonly ILanguagesService languagesService;

        public PeopleController(IPeopleService peopleService, ICountriesService countriesService, ICitiesService citiesService, ILanguagesService languagesService)
        {
            this.peopleService = peopleService;
            this.citiesService = citiesService;
            this.countriesService = countriesService;
            this.languagesService = languagesService;
        }

        #region Create

        [HttpGet]
        public IActionResult CreatePerson()
        {
            CreatePersonViewModel createPeopleViewModel = new CreatePersonViewModel();
            createPeopleViewModel.Cities = new SelectList(this.citiesService.Read(), "Id", "Name");
            createPeopleViewModel.Countries = new SelectList(this.countriesService.Read(), "Id", "Name");
            createPeopleViewModel.Languages = new SelectList(this.languagesService.Read(), "Id", "Name");
            return View(createPeopleViewModel);
        }

        [HttpPost]
        public IActionResult CreatePerson(CreatePersonViewModel createPersonViewModel)
        {
            if (
                ModelState.IsValid &&
                int.TryParse(createPersonViewModel.CityId, out int cityId))
            {
                City city = this.citiesService.FindCity(cityId);

                if (city != null)
                {
                    Person person = new Person(
                                            createPersonViewModel.Name,
                                            createPersonViewModel.Phone,
                                            city
                                            );

                    int personId = this.peopleService.CreatePerson(person);
                    TempData["create-person"] = "success";

                    if (this.peopleService.FindPerson(personId) != null)
                    {
                        if (createPersonViewModel.LanguageIds != null)
                        {
                            foreach (var languageId in createPersonViewModel.LanguageIds)
                            {
                                if (int.TryParse(languageId, out int parsedLanguageId))
                                {
                                    if (this.languagesService.FindLanguage(parsedLanguageId) != null ||
                                    this.peopleService.FindPersonLanguage(personId, parsedLanguageId) == null)
                                    {
                                        this.peopleService.CreatePersonLanguage(personId, parsedLanguageId);
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    TempData["create-person-city-non-existing"] = "failure";
                    return RedirectToAction(nameof(Index), "People");
                }
            }
            else
            {
                TempData["create-person"] = "failure";
                createPersonViewModel.Cities = new SelectList(this.citiesService.Read(), "Id", "Name");
                createPersonViewModel.Countries = new SelectList(this.countriesService.Read(), "Id", "Name");
                createPersonViewModel.Languages = new SelectList(this.languagesService.Read(), "Id", "Name");
                return View(createPersonViewModel);
            }

            return RedirectToAction(nameof(Index), "People");
        }

        #endregion

        #region Read

        [HttpGet]
        public IActionResult Index()
        {
            PeopleViewModel peopleViewModel = new PeopleViewModel
            {
                People = this.peopleService.Read()
            };

            return View(peopleViewModel);
        }

        [HttpPost]
        public IActionResult Index(string searchString, bool caseSensitive = false)
        {
            PeopleViewModel peopleViewModel = new PeopleViewModel();

            if (!string.IsNullOrEmpty(searchString))
            {
                peopleViewModel.People = this.peopleService.FindPeople(searchString, caseSensitive);
            }
            else
            {
                return RedirectToAction(nameof(Index), "People");
            }

            return View(peopleViewModel);
        }

        [HttpPost]
        public IActionResult GetCountryCityIdsAjax(string countryId)
        {
            if (!string.IsNullOrEmpty(countryId) && int.TryParse(countryId, out int parsedCountryId))
            {
                return Json(JsonConvert.SerializeObject(this.citiesService.GetCountryCityIds(parsedCountryId)));
            }

            return StatusCode(400);
        }

        [HttpPost]
        public IActionResult GetCityCountryIdAjax(string cityId)
        {
            if (!string.IsNullOrEmpty(cityId) && int.TryParse(cityId, out int parsedCityId))
            {
                return Json(JsonConvert.SerializeObject(this.citiesService.FindCity(parsedCityId).Country.Id));
            }

            return StatusCode(400);
        }

        #endregion

        #region Update

        [HttpGet]
        public IActionResult UpdatePerson(int personId)
        {
            UpdatePersonViewModel updatePersonViewModel = new UpdatePersonViewModel();
            updatePersonViewModel.LanguageIds = new List<string>();
            Person person = this.peopleService.FindPerson(personId);

            if (person != null)
            {
                updatePersonViewModel.Id = person.Id;
                updatePersonViewModel.Name = person.Name;
                updatePersonViewModel.Phone = person.Phone;
                updatePersonViewModel.CityId = person.City.Id.ToString();
                updatePersonViewModel.CountryId = person.City.Country.Id.ToString();

                List<int> languagesSelectedValues = new List<int>();
                foreach (var personLanguage in person.PersonLanguages)
                {
                    languagesSelectedValues.Add(personLanguage.LanguageId);
                    updatePersonViewModel.LanguageIds.Add(personLanguage.LanguageId.ToString());
                }

                updatePersonViewModel.Cities = new SelectList(this.citiesService.Read(), "Id", "Name", person.City.Id);
                updatePersonViewModel.Countries = new SelectList(this.countriesService.Read(), "Id", "Name", person.City.Country.Id);
                updatePersonViewModel.Languages = new SelectList(this.languagesService.Read(), "Id", "Name", languagesSelectedValues);
            }
            else
            {
                TempData["update-person-person-non-existing"] = "failure";
                return RedirectToAction(nameof(Index), "Cities");
            }

            return View(updatePersonViewModel);
        }

        [HttpPost]
        public IActionResult UpdatePerson(UpdatePersonViewModel updatePersonViewModel)
        {
            if (
                ModelState.IsValid &&
                int.TryParse(updatePersonViewModel.CityId, out int cityId))
            {
                City city = this.citiesService.FindCity(cityId);

                if (city != null)
                {
                    Person person = new Person(
                                            updatePersonViewModel.Name,
                                            updatePersonViewModel.Phone,
                                            city
                                            );
                    person.Id = updatePersonViewModel.Id;

                    this.peopleService.UpdatePerson(person);
                    TempData["update-person"] = "success";

                    if (this.peopleService.FindPerson(person.Id) != null)
                    {
                        if (updatePersonViewModel.LanguageIds != null)
                        {
                            List<Language> allLanguages = this.languagesService.Read();
                            foreach (var language in allLanguages)
                            {
                                if (this.peopleService.FindPersonLanguage(person.Id, language.Id) != null)
                                {
                                    this.peopleService.DeletePersonLanguage(person.Id, language.Id);
                                }
                            }

                            foreach (var languageId in updatePersonViewModel.LanguageIds)
                            {
                                if (int.TryParse(languageId, out int parsedLanguageId))
                                {
                                    if (this.languagesService.FindLanguage(parsedLanguageId) != null ||
                                    this.peopleService.FindPersonLanguage(person.Id, parsedLanguageId) == null)
                                    {
                                        this.peopleService.CreatePersonLanguage(person.Id, parsedLanguageId);
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    TempData["update-person-city-non-existing"] = "failure";
                    return RedirectToAction(nameof(Index), "People");
                }
            }
            else
            {
                TempData["update-person"] = "failure";

                updatePersonViewModel.LanguageIds = new List<string>();
                Person person = this.peopleService.FindPerson(updatePersonViewModel.Id);

                if (person != null)
                {
                    updatePersonViewModel.Id = person.Id;
                    updatePersonViewModel.Name = person.Name;
                    updatePersonViewModel.Phone = person.Phone;
                    updatePersonViewModel.CityId = person.City.Id.ToString();
                    updatePersonViewModel.CountryId = person.City.Country.Id.ToString();

                    List<int> languagesSelectedValues = new List<int>();
                    foreach (var personLanguage in person.PersonLanguages)
                    {
                        languagesSelectedValues.Add(personLanguage.LanguageId);
                        updatePersonViewModel.LanguageIds.Add(personLanguage.LanguageId.ToString());
                    }

                    updatePersonViewModel.Cities = new SelectList(this.citiesService.Read(), "Id", "Name", person.City.Id);
                    updatePersonViewModel.Countries = new SelectList(this.countriesService.Read(), "Id", "Name", person.City.Country.Id);
                    updatePersonViewModel.Languages = new SelectList(this.languagesService.Read(), "Id", "Name", languagesSelectedValues);
                }
                else
                {
                    TempData["update-person-person-non-existing"] = "failure";
                    return RedirectToAction(nameof(Index), "Cities");
                }

                return View(updatePersonViewModel);
            }

            return RedirectToAction(nameof(Index), "People");
        }

        #endregion

        #region Delete

        public IActionResult DeletePerson(int personId)
        {
            if (this.peopleService.FindPerson(personId) != null)
            {
                this.peopleService.DeletePerson(personId);
                TempData["delete-person"] = "success";
            }
            else
            {
                TempData["delete-person-person-non-existing"] = "failure";
            }

            return RedirectToAction(nameof(Index), "People");
        }

        #endregion
    }
}
