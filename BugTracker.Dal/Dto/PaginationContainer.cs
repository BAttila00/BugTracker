using System;
using System.Collections.Generic;
using System.Text;

namespace BugTracker.Dal.Dto {
    public class PaginationContainer<T> {

        public IEnumerable<T> Pages { get; set; }
        public int NumberOfElements { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

    }
}
