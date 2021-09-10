using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BugTracker.Dal {
    class BugTrackerDbContext : DbContext {
        // ezen options-el az adatbázis-kapcsolat konfigurációs beállításait (timeout, connection string) állíthatjuk be.
        public BugTrackerDbContext(DbContextOptions options) : base(options) {

        }
    }
}
