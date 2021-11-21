using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BugTracker.Dal;
using BugTracker.Dal.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BugTracker.Web.Pages.Issues
{
    public class CreateModel : PageModel
    {
        private readonly BugTracker.Dal.BugTrackerDbContext _context;
        private readonly UserManager<User> _userManager;

        public CreateModel(BugTracker.Dal.BugTrackerDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> OnGet()
        {
            ViewData["AssignedToId"] = new SelectList(_context.Users, "Id", "UserName");
            ViewData["CreatorId"] = new SelectList(_context.Users, "Id", "UserName");
            ViewData["ModifiedById"] = new SelectList(_context.Users, "Id", "UserName");
            SelectList projectNames = new SelectList(_context.Projects, "Id", "ProjectName");

            User applicationUser = await _userManager.GetUserAsync(User);
            var roles = await _userManager.GetRolesAsync(applicationUser);
            if (!(roles.Contains("Administrators") || roles.Contains("LeadDevelopers"))) {
                List<Project> myProjects = await GetMyProjects();
                projectNames = new SelectList(myProjects, "Id", "ProjectName");
            }
            ViewData["ProjectId"] = projectNames;
            return Page();
        }

        [BindProperty]
        public Issue Issue { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            User applicationUser = await _userManager.GetUserAsync(User);
            Issue.Creator = applicationUser;
            Issue.ModifiedBy = applicationUser;
            Issue.CreationDate = DateTime.Now;
            Issue.ModifiedOn = DateTime.Now;
            Issue.SolvedOn = null;

            _context.Issues.Add(Issue);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        protected async Task<List<Project>> GetMyProjects() {
            var projects = await _context.Projects
                .Include(p => p.Creator)
                .Include(p => p.ModifiedBy).ToListAsync();

            var projectUser = await _context.ProjectUsers
                .Include(p => p.User)
                .Include(p => p.Project).ToListAsync();

            User applicationUser = await _userManager.GetUserAsync(User);

            var myProjectIds = projectUser.Where(p => p.UserId == applicationUser.Id).Select(p => p.ProjectId).Distinct().ToList();
            projects = projects.Where(p => myProjectIds.Contains(p.Id)).ToList();

            return projects;
        }
    }
}
