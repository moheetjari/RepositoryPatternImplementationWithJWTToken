using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading;

namespace LogicBoot.Api.Entities.Helper
{
    public class PagedList<T> : List<T>
    {
        public int CurrentPage { get; private set; }
        public int TotalPages { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }

        public bool HasPrevious
        {
            get
            {
                return (CurrentPage > 1);
            }
        }

        public bool HasNext
        {
            get
            {
                return (CurrentPage < TotalPages);
            }
        }

        public PagedList(List<T> items, int totalCount, int pageNumber, int pageSize)
        {
            TotalCount = totalCount;
            PageSize = PageSize;
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            AddRange(items);
        }

        public static PagedList<T> Create(IQueryable<T> source, int pageNumber, int pageSize)
        {
            var totalCount = source.Count();
            var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            return new PagedList<T>(items, totalCount, pageNumber, pageSize);
        }
    }
}
