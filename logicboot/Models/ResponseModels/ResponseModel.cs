using LogicBoot.Api.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogicBoot.Api.Web.Models
{
    public class ResponseModel<T>
    {
        public List<T> Items { get; set; }
        public bool HasPreviousPage { get; set; }
        public string PreviousPageLink { get; set; }
        public bool HasNextPage { get; set; }
        public string NextPageLink { get; set; }
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPage { get; set; }
    }
}
