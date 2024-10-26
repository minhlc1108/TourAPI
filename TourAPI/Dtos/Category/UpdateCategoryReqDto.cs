using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TourAPI.Dtos.Category
{
    public class UpdateCategoryReqDto
    {
        [Required(ErrorMessage = "Tên danh mục không được để trống")]
        [MaxLength(255, ErrorMessage = "Tên danh mục không được vượt quá 255 ký tự")]
        public string Name { get; set; }
        public string? Detail { get; set; }
        public string? Status { get; set; }
    }
}