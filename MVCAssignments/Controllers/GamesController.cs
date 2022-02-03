using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace MVCAssignments.Controllers
{
    public class GamesController : Controller
    {
        private readonly Random random = new Random((int)DateTime.Now.Ticks);

        public IActionResult GuessingGame()
        {
            InitNumber();
            InitGuesses();

            return View();
        }

        [HttpPost]
        public IActionResult GuessingGame(string number)
        {

            if (!int.TryParse(number, out int validNumber))
            {
                ViewBag.Message = "The entered number was not a valid one! Please try again!";
            }
            else if (validNumber < 1 || validNumber > 100)
            {
                ViewBag.Message = "The entered number was not in the interval! Please try again!";
            }
            else
            {
                IncrementGuesses();
                string guesses = HttpContext.Session.GetString("guesses");

                int sessionNumber = int.Parse(HttpContext.Session.GetString("number"));

                if (validNumber > sessionNumber)
                {
                    ViewBag.Message = $"Guess {guesses}: The entered number {validNumber} was to high! Please try again!";
                }
                else if (validNumber < sessionNumber)
                {
                    ViewBag.Message = $"Guess {guesses}: The entered number {validNumber} was to low! Please try again!";
                }
                else
                {
                    SetHighScoresCookie();

                    ViewBag.Message = $"Guess {guesses}: The entered number {validNumber} was correct! Please play again!";

                    InitNumber();
                    InitGuesses();
                }
            }

            return View();
        }

        private void IncrementGuesses()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("guesses")))
            {
                int guesses = int.Parse(HttpContext.Session.GetString("guesses"));
                guesses++;
                HttpContext.Session.SetString("guesses", $"{guesses}");
            }
            else
            {
                InitGuesses();
            }
        }

        private void InitGuesses()
        {
            HttpContext.Session.SetString("guesses", "0");
        }

        private void InitNumber()
        {
            HttpContext.Session.SetString("number", $"{random.Next(1, 101)}");
        }

        private void SetHighScoresCookie()
        {
            if (!HttpContext.Request.Cookies.ContainsKey("high-scores"))
            {
                HttpContext.Response.Cookies.Append("high-scores", HttpContext.Session.GetString("guesses"));
            }
            else
            {
                HttpContext.Response.Cookies.Append(
                    "high-scores",
                    HttpContext.Request.Cookies["high-scores"] +
                    "," +
                    HttpContext.Session.GetString("guesses")
                    );

            }
        }
    }
}
