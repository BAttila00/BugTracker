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
    public class IndexModel : PageModel
    {
        private readonly BugTracker.Dal.BugTrackerDbContext _context;

        public IndexModel(BugTracker.Dal.BugTrackerDbContext context)
        {
            _context = context;
        }

        public IList<Issue> Issue { get;set; }

        public async Task OnGetAsync()
        {
            Issue = await _context.Issues
                .Include(i => i.AssignedTo)
                .Include(i => i.Creator)
                .Include(i => i.ModifiedBy)
                .Include(i => i.Project).ToListAsync();
        }
    }
}
