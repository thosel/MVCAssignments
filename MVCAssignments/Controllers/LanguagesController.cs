using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVCAssignments.Models;
using MVCAssignments.Services;
using MVCAssignments.ViewModels;

namespace MVCAssignments.Controllers
{
    [Authorize(Roles = "Admin")]
    public class LanguagesController : Controller
    {
        private readonly ILanguagesService languagesService;

        public LanguagesController(ILanguagesService languagesService)
        {
            this.languagesService = languagesService;
        }

        #region Create

        [HttpGet]
        public IActionResult CreateLanguage()
        {
            CreateLanguageViewModel createLanguageViewModel = new CreateLanguageViewModel();
            return View(createLanguageViewModel);
        }

        [HttpPost]
        public IActionResult CreateLanguage(CreateLanguageViewModel createLanguageViewModel)
        {
            if (ModelState.IsValid)
            {
                Language language = new Language(createLanguageViewModel.Name);

                if (this.languagesService.FindLanguage(language.Name) == null)
                {
                    this.languagesService.CreateLanguage(language);
                    TempData["create-language"] = "success";
                }
                else
                {
                    TempData["create-language-language-existed"] = "failure";
                    return View(createLanguageViewModel);
                }
            }
            else
            {
                TempData["create-language"] = "failure";
                return View(createLanguageViewModel);
            }

            return RedirectToAction(nameof(Index), "Languages");
        }

        #endregion

        #region Read

        [HttpGet]
        public IActionResult Index()
        {
            LanguagesViewModel languagesViewModel = new LanguagesViewModel
            {
                Languages = this.languagesService.Read()
            };

            return View(languagesViewModel);
        }

        [HttpPost]
        public IActionResult Index(string searchString, bool caseSensitive = false)
        {
            LanguagesViewModel languagesViewModel = new LanguagesViewModel();

            if (!string.IsNullOrEmpty(searchString))
            {
                languagesViewModel.Languages = this.languagesService.FindLanguages(searchString, caseSensitive);
            }
            else
            {
                return RedirectToAction(nameof(Index), "Languages");
            }

            return View(languagesViewModel);
        }

        #endregion

        #region Update

        [HttpGet]
        public IActionResult UpdateLanguage(int languageId)
        {
            UpdateLanguageViewModel updateLanguageViewModel = new UpdateLanguageViewModel();
            Language language = this.languagesService.FindLanguage(languageId);

            if (language != null)
            {
                updateLanguageViewModel.Id = language.Id;
                updateLanguageViewModel.Name = language.Name;
            }
            else
            {
                TempData["update-language-language-non-existing"] = "failure";
                return RedirectToAction(nameof(Index), "Languages");
            }

            return View(updateLanguageViewModel);
        }

        [HttpPost]
        public IActionResult UpdateLanguage(UpdateLanguageViewModel updateLanguageViewModel)
        {
            if (ModelState.IsValid)
            {
                Language language = this.languagesService.FindLanguage(updateLanguageViewModel.Id);

                if (language != null)
                {
                    language.Id = updateLanguageViewModel.Id;
                    language.Name = updateLanguageViewModel.Name;
                    this.languagesService.UpdateLanguage(language);
                    TempData["update-language"] = "success";
                }
                else
                {
                    TempData["update-language-language-non-existing"] = "failure";
                    return RedirectToAction(nameof(Index), "Languages");
                }
            }
            else
            {
                TempData["update-language"] = "failure";
                return View(updateLanguageViewModel);
            }

            return RedirectToAction(nameof(Index), "Languages");
        }

        #endregion

        #region Delete

        public IActionResult DeleteLanguage(int languageId)
        {
            if (this.languagesService.FindLanguage(languageId) != null)
            {
                this.languagesService.DeleteLanguage(languageId);
                TempData["delete-language"] = "success";
            }
            else
            {
                TempData["delete-language-language-non-existing"] = "failure";
            }

            return RedirectToAction(nameof(Index), "Languages");
        }

        #endregion
    }
}
