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

namespace BugTracker.Web.Pages.Issues
{
    public class IndexModel : PageModel
    {
        private readonly BugTracker.Dal.BugTrackerDbContext _context;
        private readonly UserManager<User> _userManager;

        public IndexModel(BugTracker.Dal.BugTrackerDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<Issue> Issue { get;set; }

        public async Task<IActionResult> OnGetAsync(bool myIssues = false)
        {
            Issue = await _context.Issues
                .Include(i => i.AssignedTo)
                .Include(i => i.Creator)
                .Include(i => i.ModifiedBy)
                .Include(i => i.Project).ToListAsync();

            User applicationUser = await _userManager.GetUserAsync(User);

            //Ha nem admin van bejelentkezve ne látszódjon az összes issue még akkor se ha http://localhost:...portNumber.../Issues/ oldalra navigálunk manuálisan (átirányít)
            var roles = await _userManager.GetRolesAsync(applicationUser);
            if ((!roles.Contains("Administrators") && !myIssues) || myIssues) {
                Issue = Issue.Where(i => i.AssignedToId == applicationUser.Id).ToList();
            }

            return Page();
        }
    }
}
