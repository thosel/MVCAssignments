using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Cryptography;

namespace MVCAssignments.Models.Data
{
    public class MVCAssignmentsContext : IdentityDbContext<ApplicationUser>
    {
        public MVCAssignmentsContext(DbContextOptions<MVCAssignmentsContext> options) : base(options) { }
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<PersonLanguage> PersonLanguages { get; set; }


        public DbSet<ApplicationUser> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            string adminRoleId = Guid.NewGuid().ToString();
            string adminUserId = Guid.NewGuid().ToString();

            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = adminRoleId,
                Name = "Admin",
                NormalizedName = "ADMIN"
            });
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = Guid.NewGuid().ToString(),
                Name = "User",
                NormalizedName = "USER"
            });

            PasswordHasher<ApplicationUser> passwordHasher = new PasswordHasher<ApplicationUser>();


            modelBuilder.Entity<ApplicationUser>().HasData(new ApplicationUser
            {
                Id = adminUserId,
                UserName = "admin.A1@admin.com",
                NormalizedUserName = "ADMIN.A1@ADMIN.COM",
                Email = "admin.A1@admin.com",
                NormalizedEmail = "ADMIN.A1@ADMIN.COM",
                PasswordHash = passwordHasher.HashPassword(null, "admin.A1@admin.com"),
                FirstName = "Admin",
                LastName = "Adamsson",
                BirthDate = DateTime.UtcNow.Date
            });
            
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = adminRoleId,
                UserId = adminUserId
            });

            modelBuilder.Entity<PersonLanguage>().HasKey(personLanguage => new { personLanguage.PersonId, personLanguage.LanguageId });

            modelBuilder.Entity<PersonLanguage>()
                .HasOne(personLanguage => personLanguage.Person)
                .WithMany(person => person.PersonLanguages)
                .HasForeignKey(personLanguage => personLanguage.PersonId);

            modelBuilder.Entity<PersonLanguage>()
                .HasOne(personLanguage => personLanguage.Language)
                .WithMany(language => language.PersonLanguages)
                .HasForeignKey(personLanguage => personLanguage.LanguageId);

            modelBuilder.Entity<Country>().HasData(new { Id = 1000, Name = "Sverige" });
            modelBuilder.Entity<Country>().HasData(new { Id = 1001, Name = "Finland" });
            modelBuilder.Entity<Country>().HasData(new { Id = 1002, Name = "Island" });
            modelBuilder.Entity<Country>().HasData(new { Id = 1003, Name = "Danmark" });
            modelBuilder.Entity<Country>().HasData(new { Id = 1004, Name = "Norge" });

            modelBuilder.Entity<City>().HasData(new { Id = 1000, Name = "Stockholm", CountryId = 1000 });
            modelBuilder.Entity<City>().HasData(new { Id = 1001, Name = "Helsingfors", CountryId = 1001 });
            modelBuilder.Entity<City>().HasData(new { Id = 1002, Name = "Reykjavík", CountryId = 1002 });
            modelBuilder.Entity<City>().HasData(new { Id = 1003, Name = "Köpenhamn", CountryId = 1003 });
            modelBuilder.Entity<City>().HasData(new { Id = 1004, Name = "Oslo", CountryId = 1004 });

            modelBuilder.Entity<Person>().HasData(new { Id = 1000, Name = "Sven Svensk", Phone = "1111111111", CityId = 1000 });
            modelBuilder.Entity<Person>().HasData(new { Id = 1001, Name = "Pecka Finsk", Phone = "2222222222", CityId = 1001 });
            modelBuilder.Entity<Person>().HasData(new { Id = 1002, Name = "Sigurður Isländsk", Phone = "3333333333", CityId = 1002 });
            modelBuilder.Entity<Person>().HasData(new { Id = 1003, Name = "Preben Dansk", Phone = "4444444444", CityId = 1003 });
            modelBuilder.Entity<Person>().HasData(new { Id = 1004, Name = "Ola Norman", Phone = "5555555555", CityId = 1004 });

            modelBuilder.Entity<Language>().HasData(new { Id = 1000, Name = "Svenska" });
            modelBuilder.Entity<Language>().HasData(new { Id = 1001, Name = "Finska" });
            modelBuilder.Entity<Language>().HasData(new { Id = 1002, Name = "Isländska" });
            modelBuilder.Entity<Language>().HasData(new { Id = 1003, Name = "Danska" });
            modelBuilder.Entity<Language>().HasData(new { Id = 1004, Name = "Norska" });

            modelBuilder.Entity<PersonLanguage>().HasData(new { PersonId = 1000, LanguageId = 1000 });
            modelBuilder.Entity<PersonLanguage>().HasData(new { PersonId = 1001, LanguageId = 1001 });
            modelBuilder.Entity<PersonLanguage>().HasData(new { PersonId = 1002, LanguageId = 1002 });
            modelBuilder.Entity<PersonLanguage>().HasData(new { PersonId = 1003, LanguageId = 1003 });
            modelBuilder.Entity<PersonLanguage>().HasData(new { PersonId = 1004, LanguageId = 1004 });
        }
    }
}
