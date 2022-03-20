using Microsoft.AspNetCore.Mvc;
using MVCAssignments.Models;
using MVCAssignments.Models.ClientAppModels;
using MVCAssignments.Services;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace MVCAssignments.Controllers
{
    public class PeopleAPIController : Controller
    {
        private readonly IPeopleService peopleService;
        private readonly ICitiesService citiesService;
        private readonly ICountriesService countriesService;
        private readonly ILanguagesService languagesService;

        public PeopleAPIController(IPeopleService peopleService, ICountriesService countriesService, ICitiesService citiesService, ILanguagesService languagesService)
        {
            this.peopleService = peopleService;
            this.citiesService = citiesService;
            this.countriesService = countriesService;
            this.languagesService = languagesService;
        }

        [HttpGet]
        public string GetPeople()
        {
            List<Person> people = this.peopleService.Read();
            List<ClientAppPerson> clientAppPeople = new List<ClientAppPerson>();
            people.ForEach((person) =>
            {
                ClientAppPerson clientAppPerson = new ClientAppPerson();

                clientAppPerson.Id = person.Id;
                clientAppPerson.Name = person.Name;
                clientAppPerson.Phone = person.Phone;
                clientAppPerson.City = person.City.Name;
                clientAppPerson.Country = person.City.Country.Name;
                clientAppPerson.Languages = new List<string>();

                List<Language> languages = this.languagesService.Read();

                foreach (var language in languages)
                {
                    if (this.peopleService.FindPersonLanguage(person.Id, language.Id) != null)
                    {
                        clientAppPerson.Languages.Add(language.Name);
                    }
                }

                clientAppPeople.Add(clientAppPerson);
            });


            return JsonConvert.SerializeObject(clientAppPeople);
        }

        [HttpGet]
        public string GetCities()
        {
            List<City> cities = this.citiesService.Read();
            List<ClientAppCity> citiesToReturn = new List<ClientAppCity>();

            foreach (var city in cities)
            {
                ClientAppCity clientAppCity = new ClientAppCity();
                clientAppCity.CityId = city.Id;
                clientAppCity.CityName = city.Name;
                citiesToReturn.Add(clientAppCity);
            }

            return JsonConvert.SerializeObject(citiesToReturn);
        }

        [HttpGet]
        public string GetLanguages()
        {
            List<Language> languages = this.languagesService.Read();
            List<ClientAppLanguage> languagesToReturn = new List<ClientAppLanguage>();

            foreach (var language in languages)
            {
                ClientAppLanguage clientAppLanguage = new ClientAppLanguage();
                clientAppLanguage.LanguageId = language.Id;
                clientAppLanguage.LanguageName = language.Name;
                languagesToReturn.Add(clientAppLanguage);
            }

            return JsonConvert.SerializeObject(languagesToReturn);
        }

        [HttpPost]
        public IActionResult CreatePerson([FromBody] ClientAppCreatePerson clientAppCreatePerson)
        {
            if (string.IsNullOrEmpty(clientAppCreatePerson.Name))
            {
                return StatusCode(400);
            }
            if (clientAppCreatePerson.Phone == null)
            {
                return StatusCode(400);
            }
            if(this.citiesService.FindCity(clientAppCreatePerson.CityId) == null)
            {
                return StatusCode(400);
            }
            if (clientAppCreatePerson.LanguageIds == null)
            {
                return StatusCode(400);
            }
            else
            {
                foreach (var languageId in clientAppCreatePerson.LanguageIds)
                {
                    if (this.languagesService.FindLanguage(languageId) == null)
                    {
                        return StatusCode(400);
                    }
                }
            }

            Person person = new Person(
                clientAppCreatePerson.Name,
                clientAppCreatePerson.Phone,
                this.citiesService.FindCity(clientAppCreatePerson.CityId)
                );
            
            int personId = this.peopleService.CreatePerson(person);


            foreach (var languageId in clientAppCreatePerson.LanguageIds)
            {
                if (this.peopleService.FindPersonLanguage(personId, languageId) == null)
                {
                    this.peopleService.CreatePersonLanguage(personId, languageId);
                }
            }

            return StatusCode(201, personId);
        }

        [HttpGet]
        public IActionResult DeletePerson(int id)
        {
            if (this.peopleService.FindPerson(id) != null)
            {
                this.peopleService.DeletePerson(id);
                return StatusCode(204);
            }

            return StatusCode(400);
        }
    }
}
