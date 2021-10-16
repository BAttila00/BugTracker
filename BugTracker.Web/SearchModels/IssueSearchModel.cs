using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTracker.Web.SearchModels {
    public class IssueSearchModel {
        public string Descreption { get; set; }
        public int AssignedToId { get; set; } = -1;
        public int ProjectId { get; set; } = -1;
        public int CreatorId { get; set; } = -1;
        public int IssueStatus { get; set; } = -1;
        public int IssuePriority { get; set; } = -1;
        public int IssueSeverity { get; set; } = -1;
    }
}
