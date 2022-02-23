using MVCAssignments.Models;
using System.Linq;
using System.Threading.Tasks;

namespace MVCAssignments.Services
{
    public interface IApplicationUsersService
    {
        public IQueryable<ApplicationUser> Read();

        public Task<ApplicationUser> FindApplicationUserAsync(string applicationUserId);

        public Task DeleteApplicationUserAsync(ApplicationUser applicationUser);
    }
}
