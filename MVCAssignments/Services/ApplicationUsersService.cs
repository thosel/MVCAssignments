using Microsoft.AspNetCore.Identity;
using MVCAssignments.Models;
using MVCAssignments.Models.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCAssignments.Services
{
    public class ApplicationUsersService : IApplicationUsersService
    {
        private readonly MVCAssignmentsContext db;
        private readonly UserManager<ApplicationUser> applicationUserManager;
        private readonly RoleManager<IdentityRole> applicationRoleManager;

        public ApplicationUsersService(MVCAssignmentsContext db, UserManager<ApplicationUser> applicationUserManager, RoleManager<IdentityRole> applicationRoleManager)
        {
            this.db = db;
            this.applicationUserManager = applicationUserManager;
            this.applicationRoleManager = applicationRoleManager;
        }

        public IQueryable<ApplicationUser> Read()
        {
            return this.applicationUserManager.Users;
        }

        public async Task<ApplicationUser> FindApplicationUserAsync(string applicationUserId)
        {
            return await this.applicationUserManager.FindByIdAsync(applicationUserId);
        }

        public List<IdentityRole> ReadApplicationRoles()
        {
            return this.applicationRoleManager.Roles.ToList();
        }

        public async Task<Dictionary<string, bool>> FindApplicationUserRolesAsync(ApplicationUser applicationUser)
        {
            Dictionary<string, bool> rolesDictionary = new Dictionary<string, bool>();

            var roles = ReadApplicationRoles();

            foreach (var role in roles)
            {
                rolesDictionary.Add(role.ToString(), false);
            }

            var userRoles = await this.applicationUserManager.GetRolesAsync(applicationUser);

            foreach (string role in userRoles)
            {
                if (rolesDictionary.ContainsKey(role))
                {
                    rolesDictionary[role] = true;
                }
            }

            return rolesDictionary;
        }

        public async Task UpdateApplicationUserAsync(ApplicationUser applicationUser, Dictionary<string, bool> applicationUserRoles)
        {
            if (applicationUser != null)
            {
                if (await FindApplicationUserAsync(applicationUser.Id) != null)
                {
                    await this.applicationUserManager.UpdateAsync(applicationUser);

                    if (applicationUserRoles != null)
                    {
                        foreach (KeyValuePair<string, bool> applicationUserRole in applicationUserRoles)
                        {
                            bool isApplicationUserInRole =
                                await this.applicationUserManager.IsInRoleAsync(applicationUser, applicationUserRole.Key);

                            if (isApplicationUserInRole && !applicationUserRole.Value)
                            {
                                await this.applicationUserManager.RemoveFromRoleAsync(applicationUser, applicationUserRole.Key);
                            }
                            else if (!isApplicationUserInRole && applicationUserRole.Value)
                            {
                                await this.applicationUserManager.AddToRoleAsync(applicationUser, applicationUserRole.Key);
                            }
                        }
                    }
                }
            }

            db.SaveChanges();
        }

        public async Task DeleteApplicationUserAsync(ApplicationUser applicationUser)
        {
            await this.applicationUserManager.DeleteAsync(applicationUser);
        }
    }
}
