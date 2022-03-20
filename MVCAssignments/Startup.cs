using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MVCAssignments.Models;
using MVCAssignments.Models.Data;
using MVCAssignments.Services;
using System.Threading.Tasks;

namespace MVCAssignments
{
    public class Startup
    {
        public IConfiguration Configuration { get; private set; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddMvc();

            services.AddHttpContextAccessor();
            services.AddSession();
            services.AddDbContext<MVCAssignmentsContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
                    .AddDefaultUI()
                    .AddDefaultTokenProviders()
                    .AddEntityFrameworkStores<MVCAssignmentsContext>();

            services.AddScoped<IPeopleService, PeopleService>();
            services.AddScoped<ICitiesService, CitiesService>();
            services.AddScoped<ICountriesService, CountriesService>();
            services.AddScoped<ILanguagesService, LanguagesService>();
            services.AddScoped<IApplicationUsersService, ApplicationUsersService>();

            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseCors(
                options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
                );
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet(
                    "/Identity/Account/Manage",
                    context => Task.Factory.StartNew(() => context.Response.Redirect("/Home/Index", true, true)));

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"
                    );

                endpoints.MapControllerRoute(
                    name: "feverCheck",
                    pattern: "/FeverCheck",
                    defaults: new { controller = "Doctor", action = "FeverCheck" }
                    );

                endpoints.MapControllerRoute(
                    name: "guessingGame",
                    pattern: "/GuessingGame",
                    defaults: new { controller = "Games", action = "GuessingGame" }
                    );

                endpoints.MapRazorPages();
            });
        }
    }
}
