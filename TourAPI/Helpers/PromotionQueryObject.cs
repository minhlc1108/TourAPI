using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace TourAPI.Helpers
{
    public class PromotionQueryObject
    {
        public string? Id { get; set; } = null;

        public string? Name { get; set; } = null;

        public int? Percentage {get; set; } =null;
        // Có thể thêm các thuộc tính khác nếu cần (Ví dụ: lọc theo StartDate hoặc EndDate)
        public DateTime? StartDate { get; set; } = null;
        public DateTime? EndDate { get; set; } = null;

        public string? SortBy { get; set; } = null;

         public bool IsDecsending { get; set; } = false;

        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 10;

        public int Status { get; set; } = 1;
    }
}
