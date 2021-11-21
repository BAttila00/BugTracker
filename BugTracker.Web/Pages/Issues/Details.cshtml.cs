using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BugTracker.Dal;
using BugTracker.Dal.Entities;

namespace BugTracker.Web.Pages.Issues
{
    public class DetailsModel : PageModel
    {
        private readonly BugTracker.Dal.BugTrackerDbContext _context;

        public DetailsModel(BugTracker.Dal.BugTrackerDbContext context)
        {
            _context = context;
        }

        public Issue Issue { get; set; }
        public List<Comment> Comments { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Issue = await _context.Issues
                .Include(i => i.AssignedTo)
                .Include(i => i.Creator)
                .Include(i => i.ModifiedBy)
                .Include(i => i.Project).FirstOrDefaultAsync(m => m.Id == id);

            Comments = _context.Comments.Where(c => c.IssueId == id)
                .Include(c => c.User)
                .ToList();

            if (Issue == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
