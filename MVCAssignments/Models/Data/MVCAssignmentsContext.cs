using Microsoft.EntityFrameworkCore;

namespace MVCAssignments.Models.Data
{
    public class MVCAssignmentsContext : DbContext
    {
        public DbSet<Person> People { get; set; }
    }
}
