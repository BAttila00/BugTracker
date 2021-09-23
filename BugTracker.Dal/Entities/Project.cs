using System;
using System.Collections.Generic;
using System.Text;

namespace BugTracker.Dal.Entities {
    public class Project {
        public Project() {
            ProjectUsers = new HashSet<ProjectUser>();
        }
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime PlannedFinishDate { get; set; }
        public int UserId { get; set; }     //creator id
        public User User { get; set; }      //creator
        public string ProjectName { get; set; }
        public string ProjectDescreption { get; set; }
        public ProjectStatus ProjectStatus { get; set; }
        public DateTime ModifiedOn { get; set; }
        public User ModifiedBy { get; set; }




        public virtual ICollection<ProjectUser> ProjectUsers { get; set; }
    }
}
