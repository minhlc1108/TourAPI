using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TourAPI.Helpers
{
    public class TourQueryObject
    {
        public string? Id { get; set; } = null;
        public string? Name { get; set; } = null;
        public string? Departure {get; set;} = null;
        public string? Destination {get; set;} = null;
        public int? Quantity {get; set;} = null;
        public int? Duration {get; set;} = null;
        public string? CategoryName {get; set;} = null;
        public string? SortBy { get; set; } = null;
        public bool IsDecsending { get; set; } = false;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int Status {get; set;} = 1;
    }
}