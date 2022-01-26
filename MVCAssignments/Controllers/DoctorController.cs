using Microsoft.AspNetCore.Mvc;
using MVCAssignments.Models;

namespace MVCAssignments.Controllers
{
    public class DoctorController : Controller
    {
        public IActionResult FeverCheck()
        {
            return View();
        }

        [HttpPost]
        public IActionResult FeverCheck(string temperature, string temperatureUnit)
        {
            float validTemperature;

            if (!IsTemperatureUnitValid(temperatureUnit))
            {
                ViewBag.Message = "The selected temperature unit was not a valid one! Please try again!";
            }
            else if (string.IsNullOrEmpty(temperature))
            {
                ViewBag.Message = "";
            }
            else if (!float.TryParse(temperature, out validTemperature))
            {
                ViewBag.Message = "The entered temperature was not a valid one! Please try again!";
            }
            else
            {
                ViewBag.Message = Doctor.FeverCheck(validTemperature, temperatureUnit);
            }


            return View();
        }

        private bool IsTemperatureUnitValid(string temperatureUnit)
        {
            bool isTemperatureUnitValid = false;

            switch (temperatureUnit)
            {
                case "celsius":
                case "fahrenheit":
                    isTemperatureUnitValid = true;
                    break;
                default:
                    break;
            }

            return isTemperatureUnitValid;
        }
    }
}
