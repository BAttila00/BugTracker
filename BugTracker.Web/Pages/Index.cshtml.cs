using BugTracker.Dal;
using BugTracker.Dal.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BugTracker.Web.Pages {
    public class IndexModel : PageModel {
        private readonly ILogger<IndexModel> _logger;

        private readonly BugTrackerDbContext _context;

        public IList<Issue> Issues { get; set; }
        public IList<Issue> IssuesUnresolvedAssignedToMe { get; set; }
        public IList<Issue> IssuesUnassigned { get; set; }
        public IList<Issue> IssuesResolvedCreatedByMe { get; set; }
        public IList<Issue> IssuesRecentlyModified { get; set; }

        public IndexModel(ILogger<IndexModel> logger, BugTrackerDbContext context) {
            _logger = logger;
            _context = context;
        }

        public void OnGet() {
            if (User.Identity.IsAuthenticated) {
                //A bejelentkezett user id-ja
                int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

                Issues = _context.Issues
                    .Include(i => i.AssignedTo)
                    .Include(i => i.Creator)
                    .Include(i => i.ModifiedBy)
                    .Include(i => i.Project).ToList();

                IssuesUnresolvedAssignedToMe = Issues.Where(i => i.AssignedToId == userId && i.IssueStatus != IssueStatus.Resolved && i.IssueStatus != IssueStatus.Closed).ToList();
                IssuesUnassigned = Issues.Where(i => i.IssueStatus == IssueStatus.Unassigned).ToList();
                IssuesResolvedCreatedByMe = Issues.Where(i => i.IssueStatus == IssueStatus.Resolved && i.CreatorId == userId).ToList();
                IssuesRecentlyModified = Issues.Where(i => ((DateTime.Now - i.ModifiedOn).TotalDays < 5)).ToList();
            }
        }
    }
}
