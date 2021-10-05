﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BugTracker.Dal;
using BugTracker.Dal.Entities;

namespace BugTracker.Web.Pages.Issues
{
    public class EditModel : PageModel
    {
        private readonly BugTracker.Dal.BugTrackerDbContext _context;

        public EditModel(BugTracker.Dal.BugTrackerDbContext context)
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
           ViewData["AssignedToId"] = new SelectList(_context.Users, "Id", "Id");
           ViewData["CreatorId"] = new SelectList(_context.Users, "Id", "Id");
           ViewData["ModifiedById"] = new SelectList(_context.Users, "Id", "Id");
           ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "ProjectName");
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
    }
}