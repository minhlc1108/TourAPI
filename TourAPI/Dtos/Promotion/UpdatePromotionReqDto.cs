using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TourAPI.Dtos.Promotion
{
    public class UpdatePromotionReqDto
    {
        [Required(ErrorMessage = "Tên khuyến mãi không được để trống")]
        [MaxLength(255, ErrorMessage = "Tên khuyến mãi không được vượt quá 255 ký tự")]
        public string Name { get; set; }
        
        [Range(0, 100, ErrorMessage = "Phần trăm giảm giá phải từ 0 đến 100")]
        public int Percentage { get; set; }

        public string Code { get; set; }
        
        [Required(ErrorMessage = "Ngày bắt đầu không được để trống")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Ngày kết thúc không được để trống")]
        public DateTime EndDate { get; set; }

        public int Status { get; set; } = 1; // Mặc định là 1 (hoạt động)
    }
}