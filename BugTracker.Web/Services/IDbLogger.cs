using BugTracker.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTracker.Web.Services {
    public interface IDbLogger {
        Task LogEvent(LogTypes logType, int userId, string eventMessage);
    }
}
