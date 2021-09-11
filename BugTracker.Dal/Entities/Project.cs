using System;
using System.Collections.Generic;
using System.Text;

namespace BugTracker.Dal.Entities {
    public class Project {
        public Project() {
            ProjectUsers = new HashSet<ProjectUser>();
        }
        public int Id { get; set; }

        public virtual ICollection<ProjectUser> ProjectUsers { get; set; }
    }
}
