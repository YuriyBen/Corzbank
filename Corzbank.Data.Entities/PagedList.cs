using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Corzbank.Data.Models
{
    public class PagedList<T>
    {
        public PagedList(int currentPage, int itemsPerPage, int totalPages, IQueryable<T> data)
        {
            CurrentPage = currentPage;
            ItemsPerPage = itemsPerPage;
            TotalItems = totalPages;
            Data = data;
        }

        public int CurrentPage { get; set; }
        public int ItemsPerPage { get; set; }
        public int TotalItems { get; set; }
        public IQueryable<T> Data { get; set; }
    }
}
