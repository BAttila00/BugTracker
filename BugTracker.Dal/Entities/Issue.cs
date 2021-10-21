using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BugTracker.Dal.Entities {
    public class Issue {
        public int Id { get; set; }

        [Required(ErrorMessage = "Kérjük adja meg a hiba leírását")]
        public string Descreption { get; set; }
        public int? CreatorId { get; set; }

        [ForeignKey("CreatorId")]
        public virtual User Creator { get; set; }
        public IssueStatus IssueStatus { get; set; }
        public DateTime CreationDate { get; set; }
        public int? AssignedToId { get; set; }

        [ForeignKey("AssignedToId")]
        public virtual User AssignedTo { get; set; }
        public int ProjectId { get; set; }
        public virtual Project Project { get; set; }
        public IssuePriority IssuePriority { get; set; }
        public IssueSeverity IssueSeverity { get; set; }
        public DateTime ModifiedOn { get; set; }
        public int? ModifiedById { get; set; }

        [ForeignKey("ModifiedById")]
        public virtual User ModifiedBy { get; set; }
        public DateTime? SolvedOn { get; set; }


        public virtual ICollection<Comment> Comments { get; set; }

    }
}
