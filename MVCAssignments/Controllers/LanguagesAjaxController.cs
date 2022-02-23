using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVCAssignments.Models;
using MVCAssignments.Services;
using MVCAssignments.ViewModels;
using System.Collections.Generic;

namespace MVCAssignments.Controllers
{
    [Authorize(Roles = "Admin")]
    public class LanguagesAjaxController : Controller
    {
        private readonly ILanguagesService languagesService;
        private readonly LanguagesViewModel languagesViewModel;

        public LanguagesAjaxController(ILanguagesService languagesService)
        {
            this.languagesService = languagesService;

            this.languagesViewModel = new LanguagesViewModel
            {
                CreateLanguageViewModel = new CreateLanguageViewModel(),

                Languages = this.languagesService.Read()
            };
        }

        [HttpPost]
        public IActionResult GetLanguages(string searchString, bool caseSensitive = false)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                this.languagesViewModel.Languages = this.languagesService.FindLanguages(searchString, caseSensitive);
            }

            return PartialView("_LanguagesPartial", this.languagesViewModel);
        }

        [HttpPost]
        public IActionResult CreateLanguage(string name)
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

            Language language = new Language(name);

            this.languagesService.CreateLanguage(language);
            this.languagesViewModel.Languages = this.languagesService.Read();

            return PartialView("_LanguagesPartial", this.languagesViewModel);
        }

        public IActionResult DeleteLanguage(int id)
        {
            if (this.languagesService.FindLanguage(id) != null)
            {
                this.languagesService.DeleteLanguage(id);
                this.languagesViewModel.Languages = this.languagesService.Read();
                return StatusCode(200);
            }

            return StatusCode(400);
        }

        public IActionResult GetLanguageDetails(int id)
        {
            if (this.languagesService.FindLanguage(id) == null)
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
                this.languagesViewModel.Languages = new List<Language>
                {
                    this.languagesService.FindLanguage(id)
                };
            }

            return PartialView("_LanguageDetailsPartial", this.languagesViewModel);
        }
    }
}
