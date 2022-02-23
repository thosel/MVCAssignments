using Microsoft.AspNetCore.Identity;
using MVCAssignments.Models;
using System.Linq;
using System.Threading.Tasks;

namespace MVCAssignments.Services
{
    public class ApplicationUsersService : IApplicationUsersService
    {
        private readonly UserManager<ApplicationUser> applicationUserManager;

        public ApplicationUsersService(UserManager<ApplicationUser> applicationUserManager)
        {
            this.applicationUserManager = applicationUserManager;
        }

        public IQueryable<ApplicationUser> Read()
        {
            return this.applicationUserManager.Users;
        }

        public async Task<ApplicationUser> FindApplicationUserAsync(string applicationUserId)
        {
            return await this.applicationUserManager.FindByIdAsync(applicationUserId);
        }

        public async Task DeleteApplicationUserAsync(ApplicationUser applicationUser)
        {
            await this.applicationUserManager.DeleteAsync(applicationUser);
        }
    }
}
