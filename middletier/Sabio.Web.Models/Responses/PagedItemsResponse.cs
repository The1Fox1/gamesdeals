using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sabio.Web.Models.Responses
{
    public class PagedItemsResponse<T> : ItemsResponse<T>
    {
        public PagedItemsResponse(List<T> items, int currentPage, int itemsPerPage, int totalCount)
        {
            Items = items;
            CurrentPage = currentPage;
            ItemsPerPage = itemsPerPage;
            TotalCount = totalCount;
        }

        public int CurrentPage { get; private set; }
        public int TotalCount { get; private set; }
        public int ItemsPerPage { get; private set; }
        public int TotalPages
        {
            get
            {
                return (int)Math.Ceiling((double)TotalCount / ItemsPerPage);
            }
        }
    }
}