using System;
using System.Collections.Generic;
using System.Text;

namespace BugTracker.Dal.Entities {
    public enum IssueStatus {
        Unassigned,
        Assigned,
        Acknowledged,
        Resolved,
        Closed
    }
}
