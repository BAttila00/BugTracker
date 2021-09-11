using System;
using System.Collections.Generic;
using System.Text;

//Project és User között több-több kapcsolat áll fenn, ezért ennek külön táblát hozok létre

namespace BugTracker.Dal.Entities {
    public class ProjectUser {
        public int ProjectId { get; set; }
        public int UserId { get; set; }

        public virtual Project Project { get; set; }
        public virtual User User { get; set; }
    }
}
