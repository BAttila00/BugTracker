using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTracker.Web.SearchModels {
    public class UserSearchModel {
        public string UserName { get; set; }
        public string EmailConfirmedString { get; set; } = "All";
        public int Role { get; set; } = -1;

    }
}
