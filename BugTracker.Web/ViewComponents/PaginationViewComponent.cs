using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTracker.Web.ViewComponents {
    public class PaginationViewComponent : ViewComponent {

        public IViewComponentResult Invoke(int pageSize, int pageNumber, int numberOfElements, int pagesToShow) {
            return View(new PaginationOptions {
                PageSize = pageSize,
                PageNumber = pageNumber,
                NumberOfElements = numberOfElements,
                TotalPages = (int)Math.Ceiling((double)numberOfElements / (double)pageSize),
                NumberOfPagesToShow = pagesToShow
            });
        }

        public class PaginationOptions {
            public int NumberOfElements { get; set; }
            public int PageNumber { get; set; }
            public int PageSize { get; set; }
            public int TotalPages { get; set; }
            public int NumberOfPagesToShow { get; set; }
        }
    }
}
