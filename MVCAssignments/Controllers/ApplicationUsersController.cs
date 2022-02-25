using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVCAssignments.Models;
using MVCAssignments.Services;
using MVCAssignments.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVCAssignments.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ApplicationUsersController : Controller
    {
        private readonly IApplicationUsersService applicationUsersService;
        private readonly RoleManager<IdentityRole> applicationRoleManager;
        private readonly UpdateApplicationUserViewModel updateApplicationUserViewModel;

        public ApplicationUsersController(IApplicationUsersService applicationUsersService, RoleManager<IdentityRole> applicationRoleManager)
        {
            this.applicationUsersService = applicationUsersService;
            this.applicationRoleManager = applicationRoleManager;

            this.updateApplicationUserViewModel = new UpdateApplicationUserViewModel();
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(this.applicationUsersService.Read());
        }

        [HttpGet]
        public async Task<IActionResult> UpdateUser(string userId)
        {
            ApplicationUser applicationUser = await this.applicationUsersService.FindApplicationUserAsync(userId);

            if (applicationUser != null)
            {
                this.updateApplicationUserViewModel.UserId = applicationUser.Id;
                this.updateApplicationUserViewModel.UserName = applicationUser.UserName;
                this.updateApplicationUserViewModel.Email = applicationUser.Email;
                this.updateApplicationUserViewModel.UserRoles =
                    await this.applicationUsersService.FindApplicationUserRolesAsync(applicationUser);

                return View(this.updateApplicationUserViewModel);
            }
            return RedirectToAction(nameof(Index), "ApplicationUsers");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUser(UpdateApplicationUserViewModel updateApplicationUserViewModel)
        {
            ApplicationUser applicationUser = await this.applicationUsersService.FindApplicationUserAsync(updateApplicationUserViewModel.UserId);

            if (ModelState.IsValid)
            {
                if (applicationUser != null)
                {
                    applicationUser.Email = updateApplicationUserViewModel.Email;
                    applicationUser.NormalizedEmail = updateApplicationUserViewModel.Email.ToUpper();
                    Dictionary<string, bool> applicationUserRoles = updateApplicationUserViewModel.UserRoles;

                    if (applicationUser.UserName.Equals("admin.A1@admin.com"))
                    {
                        applicationUserRoles["Admin"] = true;
                    }

                    await this.applicationUsersService.UpdateApplicationUserAsync(applicationUser, applicationUserRoles);
                }
            }
            else
            {
                if (applicationUser != null)
                {
                    this.updateApplicationUserViewModel.UserId = applicationUser.Id;
                    this.updateApplicationUserViewModel.UserName = applicationUser.UserName;
                    this.updateApplicationUserViewModel.Email = applicationUser.Email;
                    this.updateApplicationUserViewModel.UserRoles =
                        await this.applicationUsersService.FindApplicationUserRolesAsync(applicationUser);

                    return View(this.updateApplicationUserViewModel);
                }
            }

            return RedirectToAction(nameof(Index), "ApplicationUsers");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            ApplicationUser applicationUser = await this.applicationUsersService.FindApplicationUserAsync(userId);

            if (applicationUser != null)
            {
                if (!applicationUser.UserName.Equals("admin.A1@admin.com"))
                {
                    await this.applicationUsersService.DeleteApplicationUserAsync(applicationUser);
                }
            }

            return RedirectToAction(nameof(Index), "ApplicationUsers");
        }
    }
}
