using BugTracker.Dal.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BugTracker.Dal.Dto {
    public class IssueDto {
        [Required(ErrorMessage = "Kérjük adja meg a hiba leírását")]
        public string Descreption { get; set; }
        public IssueStatus IssueStatus { get; set; }
        public int? AssignedToId { get; set; }
        public int ProjectId { get; set; }
        public IssuePriority IssuePriority { get; set; }
        public IssueSeverity IssueSeverity { get; set; }
    }
}
