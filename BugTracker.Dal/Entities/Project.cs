using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BugTracker.Dal.Entities {
    public class Project {
        public Project() {
            ProjectUsers = new HashSet<ProjectUser>();
        }
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime PlannedFinishDate { get; set; }
        public int CreatorId { get; set; }

        [ForeignKey("CreatorId")]
        public virtual User Creator { get; set; }
        public string ProjectName { get; set; }
        public string ProjectDescreption { get; set; }
        public ProjectStatus ProjectStatus { get; set; }
        public DateTime ModifiedOn { get; set; }
        public int ModifiedById { get; set; }

        [ForeignKey("ModifiedById")]
        public virtual User ModifiedBy { get; set; }




        public virtual ICollection<ProjectUser> ProjectUsers { get; set; }
        public virtual ICollection<Issue> Issues { get; set; }
    }
}
