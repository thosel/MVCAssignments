using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVCAssignments.Models;
using MVCAssignments.Services;
using MVCAssignments.ViewModels;
using System.Collections.Generic;

namespace MVCAssignments.Controllers
{
    [Authorize(Roles = "Admin")]
    public class LanguagesController : Controller
    {
        private readonly ILanguagesService languagesService;
        private readonly LanguagesViewModel languagesViewModel;

        public LanguagesController(ILanguagesService languagesService)
        {
            this.languagesService = languagesService;

            this.languagesViewModel = new LanguagesViewModel
            {
                CreateLanguageViewModel = new CreateLanguageViewModel(),

                Languages = this.languagesService.Read()
            };
        }

        public IActionResult GetLanguages()
        {
            return View("/Views/Languages/Languages.cshtml", this.languagesViewModel);
        }

        [HttpPost]
        public IActionResult GetLanguages(string searchString, bool caseSensitive = false)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                this.languagesViewModel.Languages = this.languagesService.FindLanguages(searchString, caseSensitive);
            }

            return View("/Views/Languages/Languages.cshtml", this.languagesViewModel);
        }

        [HttpPost]
        public IActionResult CreateLanguage(CreateLanguageViewModel createLanguageViewModel)
        {
            if (ModelState.IsValid)
            {
                Language language = new Language(createLanguageViewModel.Name);

                this.languagesService.CreateLanguage(language);

                TempData["create-language"] = "success";
            }
            else
            {
                TempData["create-language"] = "failure";
                this.languagesViewModel.Languages = this.languagesService.Read();
                return View("/Views/Languages/Languages.cshtml", this.languagesViewModel);
            }

            return RedirectToAction(nameof(GetLanguages), "Languages");
        }

        public IActionResult DeleteLanguage(int id)
        {
            if (this.languagesService.FindLanguage(id) != null)
            {
                this.languagesService.DeleteLanguage(id);
                TempData["delete-language"] = "success";
            }
            else
            {
                TempData["delete-language"] = "failure";
            }

            return RedirectToAction(nameof(GetLanguages), "Languages");
        }

        public IActionResult GetLanguageDetails(int id)
        {
            if (this.languagesService.FindLanguage(id) == null)
            {
                TempData["get-language-details"] = "failure";
                return View("/Views/Languages/Languages.cshtml", this.languagesViewModel);
            }
            else
            {
                this.languagesViewModel.Languages = new List<Language>
                {
                    this.languagesService.FindLanguage(id)
                };
                TempData["get-language-details"] = "success";
            }

            return View("/Views/Languages/Languages.cshtml", this.languagesViewModel);
        }
    }
}
