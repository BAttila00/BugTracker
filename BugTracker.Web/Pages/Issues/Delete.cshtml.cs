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
    public class DeleteModel : PageModel
    {
        private readonly BugTracker.Dal.BugTrackerDbContext _context;

        public DeleteModel(BugTracker.Dal.BugTrackerDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Issue Issue { get; set; }

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

            if (Issue == null)
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

            Issue = await _context.Issues.FindAsync(id);

            if (Issue != null)
            {
                _context.Issues.Remove(Issue);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
