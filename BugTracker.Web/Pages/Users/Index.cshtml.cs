﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BugTracker.Dal.Entities;
using BugTracker.Dal.UserRoles;
using BugTracker.Web.SearchModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BugTracker.Web.Pages.Users
{
    public class IndexModel : PageModel
    {
        private readonly BugTracker.Dal.BugTrackerDbContext _context;
        private readonly UserManager<User> _userManager;

        public IndexModel(BugTracker.Dal.BugTrackerDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<User> User { get;set; }

        [BindProperty(SupportsGet = true)]
        public UserSearchModel UserSearch { get; set; }

        public async Task OnGetAsync()
        {
            User = await _context.Users.ToListAsync();

            if (UserSearch.UserName != null)
                User = User.Where(a => a.UserName.ToLower().Equals(UserSearch.UserName.ToLower())).ToList();
            if (UserSearch.EmailConfirmedString.Equals("Yes"))
                User = User.Where(a => a.EmailConfirmed).ToList();
            else if (UserSearch.EmailConfirmedString.Equals("No"))
                User = User.Where(a => !a.EmailConfirmed).ToList();
            if (UserSearch.Role != -1) {
                List<User> usersWithRole = new List<User>();
                foreach (User user in User) {
                    if (await HasRoleAsync(user, UserSearch.Role))
                        usersWithRole.Add(user);
                }
                User = usersWithRole;
            }
        }

        public async Task<string> GetUserRoles(User user) {
            var roles =  await _userManager.GetRolesAsync(user);
            return string.Join(", ", roles);
        }

        public async Task<bool> HasRoleAsync(User user, int role) {
            var roles = await _userManager.GetRolesAsync(user);
            Roles roleEnum = (Roles)role;
            if (roles.Contains(roleEnum.ToString()))
                return true;
            return false;
        }
    }
}
