using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BugTracker.Dal.Entities {
    public class User : IdentityUser<int> {

        public User() {
            Comments = new HashSet<Comment>();
            ProjectUsers = new HashSet<ProjectUser>();
        }

        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<ProjectUser> ProjectUsers { get; set; }
    }
}
