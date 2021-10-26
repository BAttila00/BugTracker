using BugTracker.Dal.UserRoles;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTracker.Web.Hosting {
    public static class HostDataExtensions {

        //https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/extension-methods
        //egy extension method, segítségével új funkcióval láthatunk el típusokat
        //jelen esetben egy IHost típust láttunk el új funkcióval (this IHost host rész miatt).
        //Ezek után, ha be usingoljuk a BookShop.Web.Hosting névteret vhova ott tudni fogjuk használni a MigrateDatabase metódust egy IHost példányon.
        //pl: host.MigrateDatabase()
        public static IHost MigrateDatabase<TContext>(this IHost host) where TContext : DbContext {
            using (var scope = host.Services.CreateScope()) {
                var serviceProvider = scope.ServiceProvider;
                var context = serviceProvider.GetRequiredService<TContext>();
                context.Database.Migrate();

                //User Role-ok létrehozása
                foreach (Roles role in (Roles[])Enum.GetValues(typeof(Roles))) {
                    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();
                    if (!roleManager.RoleExistsAsync(role.ToString()).Result) {
                        roleManager.CreateAsync(new IdentityRole<int>(role.ToString())).Wait();
                    }
                }
            }

            return host;
        }
    }
}

