using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTracker.Web.ViewComponents {
    public class PaginationViewComponent : ViewComponent {

        public IViewComponentResult Invoke(int pageSize, int pageNumber, int numberOfElements, int numberOfPagesToShow) {
            return View(new PaginationOptions {
                PageNumber = pageNumber,
                NumberOfElements = numberOfElements,
                TotalPages = (int)Math.Ceiling((double)numberOfElements / (double)pageSize),
                NumberOfPagesToShow = numberOfPagesToShow
            });
        }

        public class PaginationOptions {
            public int NumberOfElements { get; set; }
            public int PageNumber { get; set; }
            public int TotalPages { get; set; }
            /// <summary>
            /// How many page icons are shown at pagination.
            /// </summary>
            public int NumberOfPagesToShow { get; set; }
        }
    }
}
