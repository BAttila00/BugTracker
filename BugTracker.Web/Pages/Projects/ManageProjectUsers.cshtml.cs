using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BugTracker.Dal.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BugTracker.Web.Pages.Projects
{
    public class ManageProjectUsersModel : PageModel
    {

        public class ProjectUsersModel {
            public int UserId { get; set; }
            public string UserName { get; set; }
            public bool Selected { get; set; }
        }

        [BindProperty]
        public List<ProjectUsersModel> ProjectUsersModels { get; set; }

        private readonly BugTracker.Dal.BugTrackerDbContext _context;
        /// <summary>
        /// The selected project
        /// </summary>
        public Project Project { get; set; }
        /// <summary>
        /// The ProjectUser entities connected to this project.
        /// </summary>
        public IList<ProjectUser> ProjectUsersOnThisProject { get; set; }
        /// <summary>
        /// The IDs of users who are on this project
        /// </summary>
        public IList<int> ProjectUsersOnThisProjectOnlyUserIds { get; set; }
        /// <summary>
        /// All users
        /// </summary>
        public IList<User> User { get; set; }       //Nem szerencsés elnevezés, mert elrejti a RazorPages-böl örökölt User property-t
                                                    //ami pl ehhez kellene: User applicationUser = await _userManager.GetUserAsync(User);

        public ManageProjectUsersModel(BugTracker.Dal.BugTrackerDbContext context) {
            _context = context;
        }


        public async Task<IActionResult> OnGetAsync(int? projectId)
        {
            if (projectId == null) {
                return NotFound();
            }

            Project = await _context.Projects
                .Include(p => p.Creator)
                .Include(p => p.ModifiedBy).FirstOrDefaultAsync(m => m.Id == projectId);

            if (Project == null) {
                return NotFound();
            }

            ProjectUsersOnThisProject = await _context.ProjectUsers
                .Include(p => p.User)
                .Include(p => p.Project).Where(p => p.ProjectId == projectId).ToListAsync();

            ProjectUsersOnThisProjectOnlyUserIds = ProjectUsersOnThisProject.Select(p => p.UserId).Distinct().ToList();

            User = await _context.Users.ToListAsync();

            var projectUsersModels = new List<ProjectUsersModel>();
            foreach (var user in User) {
                var projectUsersModel = new ProjectUsersModel {
                    UserId = user.Id,
                    UserName = user.UserName
                };
                if (ProjectUsersOnThisProjectOnlyUserIds.Contains(user.Id)) {
                    projectUsersModel.Selected = true;
                }
                else {
                    projectUsersModel.Selected = false;
                }
                projectUsersModels.Add(projectUsersModel);
            }
            ProjectUsersModels = projectUsersModels;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? projectId) {

            if (!ModelState.IsValid) {
                return Page();
            }
            if (projectId == null) {
                return NotFound();
            }

            //töröljük az adott projekt usereit
            _context.ProjectUsers.RemoveRange(_context.ProjectUsers.Where(x => x.ProjectId == projectId.Value));
            await _context.SaveChangesAsync();

            //hozzáadjuk a projekthez a bejelölt usereket
            ProjectUser projectUser;
            foreach (var item in ProjectUsersModels) {
                if (item.Selected) {
                projectUser = new ProjectUser {
                    ProjectId = projectId.Value,
                    UserId = item.UserId
                };
                _context.ProjectUsers.Add(projectUser);
                }
            }
            await _context.SaveChangesAsync();


            return RedirectToPage("./Index");
        }
    }
}
