using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TourAPI.Dtos.Tour
{
    public class CreateTourDto
    {
        [Required(ErrorMessage ="Tên tour không được để trống")]
        [MaxLength(255, ErrorMessage = "Tên tour không được vượt quá 255 ký tự")]
        public string Name { get; set; }
        public string? Detail { get; set; }
        public string City { get; set; }
        public int CategoryId { get; set; }
    }
}