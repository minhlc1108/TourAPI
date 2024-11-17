using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TourAPI.Helpers
{
    public class CustomerQueryObject
    {
        public string? Id { get; set; } = null;

        public string? Name { get; set; } = null;
        public string? Sex {get; set;} = null;
        public string? Address { get; set; } = null;

        public DateTime? Birthday { get; set; }
        

        public int Status {get; set;} = 1;

        public bool IsDecsending { get; set; } = false;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? SortBy { get; internal set; }
    }
}