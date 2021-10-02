using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BugTracker.Dal;
using BugTracker.Dal.Entities;

namespace BugTracker.Web.Pages.Issues
{
    public class CreateModel : PageModel
    {
        private readonly BugTracker.Dal.BugTrackerDbContext _context;

        public CreateModel(BugTracker.Dal.BugTrackerDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["AssignedToId"] = new SelectList(_context.Users, "Id", "Id");
        ViewData["CreatorId"] = new SelectList(_context.Users, "Id", "Id");
        ViewData["ModifiedById"] = new SelectList(_context.Users, "Id", "Id");
        ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "ProjectName");
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

            _context.Issues.Add(Issue);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
