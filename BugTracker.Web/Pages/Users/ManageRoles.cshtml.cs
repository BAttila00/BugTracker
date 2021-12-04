using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BugTracker.Dal.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BugTracker.Web.Pages.Users
{
    public class ManageRolesModel : PageModel
    {
        public class ManageUserRolesModel {
            public string RoleName { get; set; }
            public bool Selected { get; set; }
        }

        [BindProperty]
        public List<ManageUserRolesModel> UserRolesModels { get; set; }
        public User User { get; set; }          //Nem szerencsés elnevezés, mert elrejti a RazorPages-böl örökölt User property-t
                                                //ami pl ehhez kellene: User applicationUser = await _userManager.GetUserAsync(User);

        private readonly BugTracker.Dal.BugTrackerDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;


        public ManageRolesModel(BugTracker.Dal.BugTrackerDbContext context, UserManager<User> userManager, RoleManager<IdentityRole<int>> roleManager) {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<IActionResult> OnGetAsync(int? id) {
            if (id == null) {
                return NotFound();
            }

            User = await _context.Users.FirstOrDefaultAsync(m => m.Id == id);

            if (User == null) {
                return NotFound();
            }

            //TODO: deletsection
            //hibát dob:
            //InvalidOperationException: There is already an open DataReader associated with this Connection which must be closed first.
            //megoldás lent
            //UserRolesModels = new List<ManageUserRolesModel>();
            //foreach (var role in _roleManager.Roles) {
            //    var userRolesViewModel = new ManageUserRolesModel {
            //        RoleName = role.Name
            //    };
            //    if (await _userManager.IsInRoleAsync(User, role.Name)) {
            //        userRolesViewModel.Selected = true;
            //    }
            //    else {
            //        userRolesViewModel.Selected = false;
            //    }
            //    UserRolesModels.Add(userRolesViewModel);
            //}

            //Megoldás:
            //https://entityframeworkcore.com/knowledge-base/60727080/helping-solving--there-is-already-an-open-datareader-associated-with-this-command-which-must-be-closed-first--in-asp-net-core-3-1-application

            var rolesList = await _roleManager.Roles.ToListAsync();

            var userRolesModels = new List<ManageUserRolesModel>();
            foreach (var role in rolesList) {
                var userRolesViewModel = new ManageUserRolesModel {
                    RoleName = role.Name
                };
                if (await _userManager.IsInRoleAsync(User, role.Name)) {
                    userRolesViewModel.Selected = true;
                }
                else {
                    userRolesViewModel.Selected = false;
                }
                userRolesModels.Add(userRolesViewModel);
            }
            UserRolesModels = userRolesModels;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id) {

            if (id == null) {
                return NotFound();
            }

            var user = await _context.Users.FirstOrDefaultAsync(m => m.Id == id);
            if (user == null) {
                return NotFound();
            }
            try {
                var selectedRoles = UserRolesModels.Where(x => x.Selected).Select(y => y.RoleName);
                var roles = await _userManager.GetRolesAsync(user);
                var result = await _userManager.RemoveFromRolesAsync(user, roles);
                if (!result.Succeeded) {
                    return Page();
                }

                result = await _userManager.AddToRolesAsync(user, selectedRoles);

                if (!result.Succeeded) {
                    return Page();
                }
            }
            catch {

            }

            return RedirectToPage("./Index");
        }
    }
}
