using System;
using System.Collections.Generic;
using System.Text;

namespace BugTracker.Dal.Entities {
    public class Comment {
        public int Id { get; set; }
        public int UserId { get; set; }


        public virtual User User { get; set; }
    }
}
