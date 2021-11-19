using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BugTracker.Dal.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BugTracker.Web.Pages.Users
{
    public class DeleteModel : PageModel
    {
        private readonly BugTracker.Dal.BugTrackerDbContext _context;
        private readonly ILogger<DeleteModel> _logger;

        public DeleteModel(BugTracker.Dal.BugTrackerDbContext context, ILogger<DeleteModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        [BindProperty]
        public User User { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            User = await _context.Users.FirstOrDefaultAsync(m => m.Id == id);

            if (User == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            User = await _context.Users.FindAsync(id);
            await DeleteUserFromConnectedIssues(id.Value);
            await DeleteUserFromConnectedProjects(id.Value);

            if (User != null)
            {
                int userId = User.Id;
                _context.Users.Remove(User);
                await _context.SaveChangesAsync();
                _logger.LogInformation("User with Id = {userId} deleted.", userId);
            }

            return RedirectToPage("./Index");
        }

        protected async Task DeleteUserFromConnectedIssues(int userId) {
            IList<Issue> IssuesAssignedtoUser = await _context.Issues.Where(i => i.AssignedToId == userId)
                .Include(i => i.AssignedTo).ToListAsync();
            IList<Issue> IssuesCreatedByUser = await _context.Issues.Where(i => i.CreatorId == userId)
                .Include(i => i.Creator).ToListAsync();
            IList<Issue> IssuesModifiedByUser = await _context.Issues.Where(i => i.ModifiedById == userId)
                .Include(i => i.ModifiedBy).ToListAsync();

            foreach (var issue in IssuesAssignedtoUser) {
                issue.AssignedToId = null;
                issue.IssueStatus = IssueStatus.Unassigned;
                _context.Attach(issue).State = EntityState.Modified;
            }
            foreach (var issue in IssuesCreatedByUser) {
                issue.CreatorId = null;
                _context.Attach(issue).State = EntityState.Modified;
            }
            foreach (var issue in IssuesModifiedByUser) {
                issue.ModifiedById = null;
                _context.Attach(issue).State = EntityState.Modified;
            }
            await _context.SaveChangesAsync();
        }

        protected async Task DeleteUserFromConnectedProjects(int userId) {
            IList<Project> ProjectsCreatedByUser = await _context.Projects.Where(p => p.CreatorId == userId)
                .Include(p => p.Creator).ToListAsync();
            IList<Project> ProjectsModifiedByUser = await _context.Projects.Where(p => p.ModifiedById == userId)
                .Include(p => p.ModifiedBy).ToListAsync();

            foreach (var project in ProjectsCreatedByUser) {
                project.CreatorId = null;
                _context.Attach(project).State = EntityState.Modified;
            }
            foreach (var project in ProjectsModifiedByUser) {
                project.ModifiedById = null;
                _context.Attach(project).State = EntityState.Modified;
            }
            await _context.SaveChangesAsync();
        }
    }
}
