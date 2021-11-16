using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BugTracker.Dal.Entities {
    public class User : IdentityUser<int> {

        public User() {
            Comments = new HashSet<Comment>();
            ProjectUsers = new HashSet<ProjectUser>();
        }

        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<ProjectUser> ProjectUsers { get; set; }

        [InverseProperty("Creator")]
        public virtual ICollection<Issue> CreatedIssues { get; set; }

        [InverseProperty("AssignedTo")]
        public virtual ICollection<Issue> Issues { get; set; }

        [InverseProperty("Creator")]
        public virtual ICollection<Project> CreatedProjects { get; set; }

        [InverseProperty("ModifiedBy")]
        public virtual ICollection<Project> ModifiedProjects { get; set; }
        /*
        private string userName;
        public override string UserName { get { return userName + " asd"; } set { userName = value; } }
        */
    }
}
