using BugTracker.Dal;
using BugTracker.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTracker.Web.Services {
    public class DbLogger : IDbLogger {
        private readonly BugTrackerDbContext _context;

        public DbLogger(BugTrackerDbContext context) {
            _context = context;
        }
        public async Task LogEvent(LogTypes logType, int userId, string eventMessage) {
            Log log = new Log { 
                LogDate = DateTime.Now,
                LogType = logType.ToString(),
                UserId = userId,
                EventMessage = eventMessage
            };

            _context.Logs.Add(log);
            await _context.SaveChangesAsync();
        }
    }
}
