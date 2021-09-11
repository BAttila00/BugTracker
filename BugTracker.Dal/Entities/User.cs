using System;
using System.Collections.Generic;
using System.Text;

namespace BugTracker.Dal.Entities {
    public class User {

        public User() {
            Comments = new HashSet<Comment>();
            ProjectUsers = new HashSet<ProjectUser>();
        }

        public int Id { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<ProjectUser> ProjectUsers { get; set; }
    }
}
