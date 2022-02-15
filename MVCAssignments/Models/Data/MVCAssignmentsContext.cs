using Microsoft.EntityFrameworkCore;

namespace MVCAssignments.Models.Data
{
    public class MVCAssignmentsContext : DbContext
    {
        public MVCAssignmentsContext(DbContextOptions<MVCAssignmentsContext> options) : base(options) { }
        public DbSet<Person> People { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>().HasData(new { Id = 1000, Name = "Kålle Glagogubbe", Phone = "031654321", City = "Göteborg" });
            modelBuilder.Entity<Person>().HasData(new { Id = 1001, Name = "Sune Söderkis", Phone = "08654321", City = "Stockholm" });
            modelBuilder.Entity<Person>().HasData(new { Id = 1002, Name = "Enok Evertsson", Phone = "0704654321", City = "Luleå" });
            modelBuilder.Entity<Person>().HasData(new { Id = 1003, Name = "Alva Alm", Phone = "0703216541", City = "Avesta" });
            modelBuilder.Entity<Person>().HasData(new { Id = 1004, Name = "Ted Rajtantajtansson", Phone = "031321654", City = "Göteborg" });
        }
    }
}
