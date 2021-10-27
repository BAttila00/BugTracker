using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BugTracker.Dal;
using BugTracker.Dal.Entities;

namespace BugTracker.Web.Pages.Projects
{
    public class DetailsModel : PageModel
    {
        private readonly BugTracker.Dal.BugTrackerDbContext _context;

        public DetailsModel(BugTracker.Dal.BugTrackerDbContext context)
        {
            _context = context;
        }

        public Project Project { get; set; }
        public IList<ProjectUser> ProjectUser { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Project = await _context.Projects
                .Include(p => p.Creator)
                .Include(p => p.ModifiedBy).FirstOrDefaultAsync(m => m.Id == id);

            ProjectUser = await _context.ProjectUsers
                .Include(p => p.User)
                .Include(p => p.Project).Where(p => p.ProjectId == id).ToListAsync();

            if (Project == null)
            {
                return NotFound();
            }
            return Page();
        }

        public string GetProjectUserUserNames() {
            return string.Join(", ", ProjectUser.Select(p => p.User.UserName).ToList());
        }
    }
}
