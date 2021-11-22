using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BugTracker.Dal;
using BugTracker.Dal.Entities;
using Microsoft.Extensions.Logging;
using BugTracker.Web.Services;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace BugTracker.Web.Pages.Issues
{
    public class DeleteModel : PageModel
    {
        private readonly BugTracker.Dal.BugTrackerDbContext _context;
        private readonly ILogger<DeleteModel> _logger;
        private readonly IDbLogger _dbLogger;
        private readonly UserManager<User> _userManager;

        public DeleteModel(BugTracker.Dal.BugTrackerDbContext context, ILogger<DeleteModel> logger, IDbLogger dbLogger, UserManager<User> userManager)
        {
            _context = context;
            _logger = logger;
            _dbLogger = dbLogger;
            _userManager = userManager;
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
                int issueId = Issue.Id;
                _context.Issues.Remove(Issue);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Issue with Id = {issueId} deleted.", issueId);

                User applicationUser = await _userManager.GetUserAsync(User);
                var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                await _dbLogger.LogEvent(LogTypes.Deletion, userId, $"Issue with Id = {issueId} deleted.");
            }

            return RedirectToPage("./Index");
        }
    }
}
