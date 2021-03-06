using BugTracker.Dal;
using BugTracker.Dal.Entities;
using BugTracker.Dal.UserRoles;
using BugTracker.Web.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BugTracker.Web {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.AddDbContext<BugTrackerDbContext>(                                                         
                option => option.UseSqlServer(Configuration.GetConnectionString("BugTrackerDbContext"))
            );

            //https://docs.microsoft.com/en-us/aspnet/core/security/authentication/identity-configuration?view=aspnetcore-3.1
            services.AddIdentity<User, IdentityRole<int>>(opt => {
                opt.SignIn.RequireConfirmedEmail = true;
            })
                .AddEntityFrameworkStores<BugTrackerDbContext>()
                .AddDefaultTokenProviders();

            //MailSettings mailSettings = new MailSettings();                             //?gy is el lehetne ?rni a mailSettings be?ll?t?sokat
            //Configuration.GetSection("MailSettings").Bind(mailSettings);
            services.Configure<MailSettings>(Configuration.GetSection("MailSettings"));  //A MailSettings.cs f?jlba felolvassa a appsettings.Development.json-ben a MailSettings section alatt be?ll?tott ?rt?keket
            services.AddTransient<IEmailSender, Services.EmailSender>();
            services.AddTransient<Services.IDbLogger, Services.DbLogger>();

            //https://docs.microsoft.com/en-us/aspnet/core/security/authorization/roles?view=aspnetcore-3.1#policy-based-role-checks-1
            services.AddAuthorization(options => {
                options.AddPolicy("RequireAdministratorOrLeadDeveloperRole", policy => policy.RequireRole(Roles.Administrators.ToString(), Roles.LeadDevelopers.ToString()));
                options.AddPolicy("RequireAdministratorRole", policy => policy.RequireRole(Roles.Administrators.ToString()));
            });

            //https://docs.microsoft.com/en-us/aspnet/core/security/authorization/razor-pages-authorization?view=aspnetcore-3.1
            services.AddRazorPages(options =>
            {
                options.Conventions.AuthorizeFolder("/Projects");       //Ne lehessen el?rni /Projects mapp?t csak ha be vagyunk jelentkezve.
                options.Conventions.AuthorizeFolder("/Issues");
                options.Conventions.AuthorizeFolder("/Users", "RequireAdministratorRole");
                options.Conventions.AuthorizePage("/Projects/Edit", "RequireAdministratorOrLeadDeveloperRole");
                options.Conventions.AuthorizePage("/Projects/Delete", "RequireAdministratorOrLeadDeveloperRole");
                options.Conventions.AuthorizePage("/Projects/Create", "RequireAdministratorOrLeadDeveloperRole");
                options.Conventions.AuthorizePage("/Projects/ManageProjectUsers", "RequireAdministratorOrLeadDeveloperRole");
                options.Conventions.AuthorizePage("/Issues/Delete", "RequireAdministratorOrLeadDeveloperRole");
            });

            //https://docs.microsoft.com/en-us/aspnet/core/security/authentication/identity-configuration?view=aspnetcore-3.1#cookie-settings
            services.ConfigureApplicationCookie(options => {
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                options.LoginPath = "/Identity/Account/Login";
                options.LogoutPath = "/Identity/Account/Logout";
            });

            services.AddControllersWithViews().AddRazorRuntimeCompilation();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory) {

            var path = Directory.GetCurrentDirectory();
            loggerFactory.AddFile($"{path}\\Logs\\Log.txt");

            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }
            else {
                app.UseExceptionHandler("/Error");
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapRazorPages();
            });
        }
    }
}
