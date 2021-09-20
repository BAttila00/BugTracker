using BugTracker.Dal;
using BugTracker.Dal.Entities;
using BugTracker.Web.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
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
                option => option.UseSqlServer(Configuration.GetConnectionString("BookShopDbContext"))
            );

            services.AddIdentity<User, IdentityRole<int>>(opt => {
                opt.SignIn.RequireConfirmedEmail = true;
            })
                .AddEntityFrameworkStores<BugTrackerDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<MailSettings>(Configuration.GetSection("MailSettings"));  //A MailSettings.cs f�jlba felolvassa a appsettings.Development.json-ben a MailSettings section alatt be�ll�tott �rt�keket
            services.AddTransient<IEmailSender, Services.EmailSender>();

            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }
            else {
                app.UseExceptionHandler("/Error");
            }

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
