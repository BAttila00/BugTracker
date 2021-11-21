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
using BugTracker.Web.SearchModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using BugTracker.Dal.Dto;
using Microsoft.Extensions.Logging;

namespace BugTracker.Web.Pages.Issues {
    public class IndexModel : PageModel {
        private readonly BugTracker.Dal.BugTrackerDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(BugTracker.Dal.BugTrackerDbContext context, UserManager<User> userManager, ILogger<IndexModel> logger) {
            _context = context;
            _userManager = userManager;
            _logger = logger;

        }

        public IList<Issue> Issue { get; set; }
        public PaginationContainer<Issue> PaginationContainer { get; set;}

        [BindProperty(SupportsGet = true)]
        public IssueSearchModel IssueSearch { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool myIssues { get; set; }

        public async Task<IActionResult> OnGetAsync(int? pageNumber, int? pageSize, int? projectId) {
            _logger.LogInformation("Issues page opened");
            ViewData["myIssues"] = myIssues;
            Issue = await _context.Issues
                .Include(i => i.AssignedTo)
                .Include(i => i.Creator)
                .Include(i => i.ModifiedBy)
                .Include(i => i.Project).ToListAsync();

            User applicationUser = await _userManager.GetUserAsync(User);

            //Ha nem admin van bejelentkezve ne látszódjon az összes issue még akkor se ha http://localhost:...portNumber.../Issues/ oldalra navigálunk manuálisan (átirányít)
            var roles = await _userManager.GetRolesAsync(applicationUser);
            if ((!(roles.Contains("Administrators") || roles.Contains("LeadDevelopers")) && !myIssues) || myIssues) {
                Issue = Issue.Where(i => i.AssignedToId == applicationUser.Id).ToList();
            }

            if(projectId != null)
                Issue = Issue.Where(a => a.ProjectId == projectId).ToList();

            ViewData["UsersWithUserName"] = new SelectList(_context.Users, "Id", "UserName");
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "ProjectName");
            //Searching
            if (IssueSearch.Descreption != null)
                Issue = Issue.Where(a => a.Descreption.ToLower().Contains(IssueSearch.Descreption.ToLower())).ToList();
            if (IssueSearch.AssignedToId != -1)
                Issue = Issue.Where(a => a.AssignedToId == IssueSearch.AssignedToId).ToList();
            if (IssueSearch.ProjectId != -1)
                Issue = Issue.Where(a => a.ProjectId == IssueSearch.ProjectId).ToList();
            if (IssueSearch.CreatorId != -1)
                Issue = Issue.Where(a => a.AssignedToId == IssueSearch.CreatorId).ToList();
            if (IssueSearch.IssueStatus != -1)
                Issue = Issue.Where(a => (int)a.IssueStatus == IssueSearch.IssueStatus).ToList();
            if (IssueSearch.IssueSeverity != -1)
                Issue = Issue.Where(a => (int)a.IssueStatus == IssueSearch.IssueSeverity).ToList();
            if (IssueSearch.IssuePriority != -1)
                Issue = Issue.Where(a => (int)a.IssueStatus == IssueSearch.IssuePriority).ToList();

            //Lapozás
            pageNumber ??= 1;
            int pageNumberNotNull = pageNumber.Value;

            pageSize??= 5;
            int pageSizeNotNull = Math.Min(pageSize.Value, 50);
            int numberOfElements = Issue.Count();
            Issue = Issue.Skip((pageNumberNotNull - 1) * pageSizeNotNull).Take(pageSizeNotNull).ToList();
            //if (numberOfElements <= pageSizeNotNull) pageSizeNotNull = numberOfElements;
            PaginationContainer = new PaginationContainer<Issue> {
                NumberOfElements = numberOfElements,
                PageNumber = pageNumberNotNull,
                PageSize = pageSizeNotNull,
                Pages = Issue
            };

            return Page();
        }
    }
}
