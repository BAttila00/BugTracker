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
using BugTracker.Web.SearchModels;
using BugTracker.Dal.Dto;

namespace BugTracker.Web.Pages.Projects {
    public class IndexModel : PageModel {
        private readonly BugTracker.Dal.BugTrackerDbContext _context;
        private readonly UserManager<User> _userManager;

        public IndexModel(BugTracker.Dal.BugTrackerDbContext context, UserManager<User> userManager) {
            _context = context;
            _userManager = userManager;
        }

        public IList<Project> Project { get; set; }
        public PaginationContainer<Project> PaginationContainer { get; set; }

        public IList<ProjectUser> ProjectUser { get; set; }

        [BindProperty(SupportsGet = true)]
        public ProjectSearchModel ProjectSearch { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool myProjects { get; set; }

        public async Task<IActionResult> OnGetAsync(int? pageNumber, int? pageSize) {
            ViewData["myProjects"] = myProjects;
            Project = await _context.Projects
                .Include(p => p.Creator)
                .Include(p => p.ModifiedBy).ToListAsync();

            ProjectUser = await _context.ProjectUsers
                .Include(p => p.User)
                .Include(p => p.Project).ToListAsync();

            User applicationUser = await _userManager.GetUserAsync(User);

            //Ha nem admin van bejelentkezve ne látszódjon az összes projekt még akkor se ha http://localhost:...portNumber.../Projects/ oldalra navigálunk manuálisan (átirányít)
            var roles = await _userManager.GetRolesAsync(applicationUser);
            //if (!roles.Contains("Administrators") && !myProjects) {
            //    return RedirectToAction("/Index", new { myProjects = true});
            //    //vagy
            //    //return RedirectToAction("Index", new { myProjects = true });
            //    //vagy
            //    //return RedirectToPage("/Projects/Index", new { myProjects = true });
            //}

            if ((!roles.Contains("Administrators") && !myProjects) || myProjects) {
                var myProjectIds = ProjectUser.Where(p => p.UserId == applicationUser.Id).Select(p => p.ProjectId).Distinct().ToList();
                Project = Project.Where(p => myProjectIds.Contains(p.Id)).ToList();
            }

            //A saját projektek megjelenítése
            //http://localhost:...portNumber.../Projects/?myProjects=true
            //if (myProjects) {

            //    var myProjectIds = ProjectUser.Where(p => p.UserId == applicationUser.Id).Select(p => p.ProjectId).Distinct().ToList();

            //    Project = Project.Where(p => myProjectIds.Contains(p.Id)).ToList();
            //}

            //Searching
            if (ProjectSearch.ProjectName != null)
                Project = Project.Where(a => a.ProjectName.ToLower().Contains(ProjectSearch.ProjectName.ToLower())).ToList();

            if (ProjectSearch.ProjectDescreption != null)
                Project = Project.Where(a => a.ProjectDescreption.ToLower().Equals(ProjectSearch.ProjectDescreption.ToLower())).ToList();

            if (ProjectSearch.ProjectStatus != -1)
                Project = Project.Where(a => (int)a.ProjectStatus == ProjectSearch.ProjectStatus).ToList();


            //Lapozás
            pageNumber ??= 1;
            int pageNumberNotNull = pageNumber.Value;

            pageSize ??= 5;
            int pageSizeNotNull = Math.Min(pageSize.Value, 50);
            int numberOfElements = Project.Count();
            Project = Project.Skip((pageNumberNotNull - 1) * pageSizeNotNull).Take(pageSizeNotNull).ToList();
            //if (numberOfElements <= pageSizeNotNull) pageSizeNotNull = numberOfElements;
            PaginationContainer = new PaginationContainer<Project> {
                NumberOfElements = numberOfElements,
                PageNumber = pageNumberNotNull,
                PageSize = pageSizeNotNull,
                Pages = Project
            };


            return Page();
        }
    }
}
