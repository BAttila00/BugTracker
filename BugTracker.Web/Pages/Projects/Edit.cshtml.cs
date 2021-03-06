using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BugTracker.Dal;
using BugTracker.Dal.Entities;
using Microsoft.AspNetCore.Identity;

namespace BugTracker.Web.Pages.Projects
{
    public class EditModel : PageModel
    {
        private readonly BugTracker.Dal.BugTrackerDbContext _context;
        private readonly UserManager<User> _userManager;

        public EditModel(BugTracker.Dal.BugTrackerDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
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
           ViewData["CreatorId"] = new SelectList(_context.Users, "Id", "UserName");
           ViewData["ModifiedById"] = new SelectList(_context.Users, "Id", "UserName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            User applicationUser = await _userManager.GetUserAsync(User);
            Project.ModifiedBy = applicationUser;
            Project.ModifiedOn = DateTime.Now;
            _context.Attach(Project).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectExists(Project.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ProjectExists(int id)
        {
            return _context.Projects.Any(e => e.Id == id);
        }

        public string GetProjectUserUserNames() {
            return string.Join(", ", ProjectUser.Select(p => p.User.UserName).ToList());
        }
    }
}
