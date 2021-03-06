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
using BugTracker.Dal.Dto;

namespace BugTracker.Web.Pages.Issues
{
    public class EditModel : PageModel
    {
        private readonly BugTracker.Dal.BugTrackerDbContext _context;
        private readonly UserManager<User> _userManager;

        public EditModel(BugTracker.Dal.BugTrackerDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
            Comments = new List<Comment>();
        }

        public Issue Issue { get; set; }

        [BindProperty]
        public IssueDto IssueDto { get; set; }

        [BindProperty]
        public Comment NewComment { get; set; }
        public List<Comment> Comments { get; set; }

        public string UserName { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Comments = _context.Comments.Where(c => c.IssueId == id)
                .Include(c => c.User)
                .ToList();
            NewComment = new Comment{ IssueId = id.Value};
            UserName = _userManager.GetUserName(User);

            Issue = await _context.Issues
                .Include(i => i.AssignedTo)
                .Include(i => i.Creator)
                .Include(i => i.ModifiedBy)
                .Include(i => i.Project).FirstOrDefaultAsync(m => m.Id == id);

            if (Issue == null)
            {
                return NotFound();
            }
            ViewData["AssignedToId"] = new SelectList(_context.Users, "Id", "UserName");
            ViewData["CreatorId"] = new SelectList(_context.Users, "Id", "UserName");
            ViewData["ModifiedById"] = new SelectList(_context.Users, "Id", "UserName");
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "ProjectName");

            IssueDto = new IssueDto {
                Descreption = Issue.Descreption,
                IssuePriority = Issue.IssuePriority,
                IssueSeverity = Issue.IssueSeverity,
                IssueStatus = Issue.IssueStatus,
                AssignedToId = Issue.AssignedToId,
                ProjectId = Issue.ProjectId
            };

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        // ----> Done

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Issue = await _context.Issues
                .Include(i => i.AssignedTo)
                .Include(i => i.Creator)
                .Include(i => i.ModifiedBy)
                .Include(i => i.Project).FirstOrDefaultAsync(m => m.Id == id);

            if (Issue == null) {
                return NotFound();
            }

            Issue.Descreption = IssueDto.Descreption;
            Issue.IssueStatus = IssueDto.IssueStatus;
            Issue.IssuePriority = IssueDto.IssuePriority;
            Issue.IssueSeverity = IssueDto.IssueSeverity;
            Issue.ProjectId = IssueDto.ProjectId;
            Issue.AssignedToId = IssueDto.AssignedToId;

            User applicationUser = await _userManager.GetUserAsync(User);
            Issue.ModifiedBy = applicationUser;
            Issue.ModifiedOn = DateTime.Now;
            if (Issue.IssueStatus == IssueStatus.Closed)
                Issue.SolvedOn = DateTime.Now;
            if (Issue.IssueStatus == IssueStatus.Unassigned)
                Issue.AssignedToId = null;

            _context.Attach(Issue).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IssueExists(Issue.Id))
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

        private bool IssueExists(int id)
        {
            return _context.Issues.Any(e => e.Id == id);
        }

        public IActionResult OnPostPostComment() {

            if (!ModelState.IsValid) {
                return RedirectToPage("/Issues/Edit", new { id = NewComment.IssueId });
            }

            string userId = _userManager.GetUserId(User);
            try {
                NewComment.UserId = int.Parse(userId);

                _context.Comments.Add(new Comment {
                    IssueId = NewComment.IssueId,
                    UserId = NewComment.UserId,
                    Text = NewComment.Text,
                    CreationDate = DateTime.Now
                });
                _context.SaveChanges();
                return RedirectToPage("/Issues/Edit", new { id = NewComment.IssueId });
            }
            catch (Exception ex) {

            }
            return RedirectToPage("./Index");
        }
    }
}
