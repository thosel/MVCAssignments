using Microsoft.AspNetCore.Identity;
using MVCAssignments.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCAssignments.Services
{
    public interface IApplicationUsersService
    {
        public IQueryable<ApplicationUser> Read();

        public Task<ApplicationUser> FindApplicationUserAsync(string applicationUserId);

        public List<IdentityRole> ReadApplicationRoles();

        public Task<Dictionary<string, bool>> FindApplicationUserRolesAsync(ApplicationUser applicationUser);

        public Task UpdateApplicationUserAsync(ApplicationUser applicationUser, Dictionary<string, bool> applicationUserRole);

        public Task DeleteApplicationUserAsync(ApplicationUser applicationUser);
    }
}
