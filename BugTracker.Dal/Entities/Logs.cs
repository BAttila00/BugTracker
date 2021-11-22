using System;
using System.Collections.Generic;
using System.Text;

namespace BugTracker.Dal.Entities {
    public class Log {
        public int Id { get; set; }
        public string LogType { get; set; }
        public DateTime LogDate { get; set; }
        public int UserId { get; set; }
        public string EventMessage { get; set; }
    }
}
