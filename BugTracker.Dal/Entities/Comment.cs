using System;
using System.Collections.Generic;
using System.Text;

namespace BugTracker.Dal.Entities {
    public class Comment {
        public int Id { get; set; }
        public int UserId { get; set; }


        public virtual User User { get; set; }

        public string Text { get; set; }
        public int IssueId { get; set; }
        public virtual Issue Issue { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
