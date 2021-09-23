using System;
using System.Collections.Generic;
using System.Text;

namespace BugTracker.Dal.Entities {
    public class Issue {
        public int Id { get; set; }
        public string Descreption { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }      //creator
        public IssueStatus IssueStatus { get; set; }
        public DateTime CreationDate { get; set; }
        public int AssignedToId { get; set; }
        public User AssignedTo { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public IssuePriority IssuePriority { get; set; }
        public IssueSeverity IssueSeverity { get; set; }
        public DateTime ModifiedOn { get; set; }
        public User ModifiedBy { get; set; }
        public DateTime SolvedOn { get; set; }

    }
}
