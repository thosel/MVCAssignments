using Microsoft.AspNetCore.Mvc;
using MVCAssignments.Models;
using MVCAssignments.Services;
using System.Threading.Tasks;

namespace MVCAssignments.Controllers
{
    public class ApplicationUsersController : Controller
    {
        private readonly IApplicationUsersService applicationUsersService;

        public ApplicationUsersController(IApplicationUsersService applicationUsersService)
        {
            this.applicationUsersService = applicationUsersService;
        }

        public IActionResult Index()
        {
            return View(this.applicationUsersService.Read());
        }

        public async Task<IActionResult> DeleteApplicationUser(string applicationUserId)
        {
            ApplicationUser applicationUser = await this.applicationUsersService.FindApplicationUserAsync(applicationUserId);
            await this.applicationUsersService.DeleteApplicationUserAsync(applicationUser);

            return RedirectToAction(nameof(Index), "ApplicationUsers");
        }
    }
}
