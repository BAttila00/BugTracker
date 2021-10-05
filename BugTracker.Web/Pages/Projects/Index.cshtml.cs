using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BugTracker.Dal;
using BugTracker.Dal.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics;

namespace BugTracker.Web.Pages.Projects {
    public class IndexModel : PageModel {
        private readonly BugTracker.Dal.BugTrackerDbContext _context;
        private readonly UserManager<User> _userManager;

        public IndexModel(BugTracker.Dal.BugTrackerDbContext context, UserManager<User> userManager) {
            _context = context;
            _userManager = userManager;
        }

        public IList<Project> Project { get; set; }

        public IList<ProjectUser> ProjectUser { get; set; }

        public async Task<IActionResult> OnGetAsync(bool myProjects = false) {
            Project = await _context.Projects
                .Include(p => p.Creator)
                .Include(p => p.ModifiedBy).ToListAsync();

            ProjectUser = await _context.ProjectUsers
                .Include(p => p.User)
                .Include(p => p.Project).ToListAsync();

            User applicationUser = await _userManager.GetUserAsync(User);

            //Ha nem admin van bejelentkezve ne látszódjon az összes projekt még akkor se ha http://localhost:...portNumber.../Projects/ oldalra navigálunk manuálisan (átirányít)
            var roles = await _userManager.GetRolesAsync(applicationUser);
            if (!roles.Contains("Administrators") && !myProjects) {
                return RedirectToPage("/Index", new { myProjects = true });
                //vagy
                //return RedirectToAction("Index", new { myProjects = true });
                //vagy
                //return RedirectToPage("/Projects/Index", new { myProjects = true });
            }

            //A saját projektek megjelenítése
            //http://localhost:...portNumber.../Projects/?myProjects=true
            if (myProjects) {

                var myProjectIds = ProjectUser.Where(p => p.UserId == applicationUser.Id).Select(p => p.ProjectId).Distinct().ToList();

                Project = Project.Where(p => myProjectIds.Contains(p.Id)).ToList();
            }

            return Page();
        }
    }
}
