using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace MVCAssignments.Controllers
{
    public class GamesController : Controller
    {
        private Random random = new Random((int)DateTime.Now.Ticks);

        public IActionResult GuessingGame()
        {
            initNumber();
            initGuesses();

            return View();
        }

        [HttpPost]
        public IActionResult GuessingGame(string number)
        {
            int validNumber;

            if (!int.TryParse(number, out validNumber))
            {
                ViewBag.Message = "The entered number was not a valid one! Please try again!";
            }
            else if (validNumber < 1 || validNumber > 100)
            {
                ViewBag.Message = "The entered number was not in the interval! Please try again!";
            }
            else
            {
                incrementGuesses();
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
                    setHighScoresCookie();

                    ViewBag.Message = $"Guess {guesses}: The entered number {validNumber} was correct! Please play again!";

                    initNumber();
                    initGuesses();
                }
            }

            return View();
        }

        private void incrementGuesses()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("guesses")))
            {
                int guesses = int.Parse(HttpContext.Session.GetString("guesses"));
                guesses++;
                HttpContext.Session.SetString("guesses", $"{guesses}");
            }
            else
            {
                initGuesses();
            }
        }

        private void initGuesses()
        {
            HttpContext.Session.SetString("guesses", "0");
        }

        private void initNumber()
        {
            HttpContext.Session.SetString("number", $"{random.Next(1, 101)}");
        }

        private void setHighScoresCookie()
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
