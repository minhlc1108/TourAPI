using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TourAPI.Helpers
{
    public class TourScheduleQueryObject
    {
        public string? Name {get; set;}
        public string? SortBy { get; set; } = null;
        public bool IsDecsending { get; set; } = false;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int Status { get; set; } = 1;
    }
}