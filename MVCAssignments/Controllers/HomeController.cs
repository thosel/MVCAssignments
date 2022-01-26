using Microsoft.AspNetCore.Mvc;
using MVCAssignments.Models;
using System.Collections.Generic;

namespace MVCAssignments.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            ViewBag.FirstName = "Thosel";
            ViewBag.LastName = "Thoselsson";
            ViewBag.Street = "Thoselgatan";
            ViewBag.StreetNumber = "37";
            ViewBag.ZipCode = "458 73";
            ViewBag.City = "Thosellanda";
            ViewBag.Phone = "+46 (0)70 - xxx xx xx";

            return View();
        }

        public IActionResult Projects()
        {
            List<Project> projects = new List<Project>();

            #region load-projects

            projects.Add(new Project(
                "Programming", 
                "Calculator", 
                "A basic console based calculator that handles basic mathematical operations:", 
                "https://github.com/thosel/Calculator")
                );

            projects.Add(new Project(
                "Programming",
                "Hangman",
                "A simple guessing game:",
                "https://github.com/thosel/Hangman")
                );

            projects.Add(new Project(
                "Advanced programming",
                "Calculator - xUnit",
                "xUnit tests for the basic console based calculator mentioned above:",
                "https://github.com/thosel/Calculator/tree/optional-tasks")
                );

            projects.Add(new Project(
                "Advanced programming",
                "Vending machine",
                "A simple vending machine:",
                "https://github.com/thosel/VendingMachine")
                );

            projects.Add(new Project(
                "Advanced programming",
                "Todo it",
                "Group assignment, 3 collaborators:",
                "https://github.com/thosel/TodoIt")
                );

            projects.Add(new Project(
                "Frontend",
                "Frontend fundamentals",
                "Assignment to build two basic HTML structured pages:",
                "https://github.com/thosel/FrontendFundamentals/tree/optional-tasks")
                );

            projects.Add(new Project(
                "Frontend",
                "Introduction to Bootstrap",
                "An assignment that is meant as an introduction to the use of Twitter Bootstrap:",
                "https://github.com/thosel/IntroductionToBootstrap")
                );

            projects.Add(new Project(
                "Frontend",
                "Sokoban",
                "A simple JavaScript game:",
                "https://github.com/thosel/Sokoban")
                );

            #endregion

            ViewBag.Projects = projects;
            return View();
        }
    }
}
