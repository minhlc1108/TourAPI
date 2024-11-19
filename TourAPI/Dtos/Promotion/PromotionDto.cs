using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TourAPI.Dtos.Tour;

namespace TourAPI.Dtos.Promotion
{
    public class PromotionDto
    {
        public int Id { get; set; }
        public string? Name { get; set; } // Thêm dấu hỏi (?) để đồng bộ với kiểu nullable trong Promotion.cs
        public int Percentage { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Status { get; set; } = 1;

        // Optional: Nếu muốn bao gồm thông tin về các TourPromotion
        //public List<TourPromotionDto> TourPromotions { get; set; } = new List<TourPromotionDto>();
    }
}
