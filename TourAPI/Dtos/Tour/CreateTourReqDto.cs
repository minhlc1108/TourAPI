using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TourAPI.Dtos.Tour
{
    public class CreateTourReqDto
    {
        [Required(ErrorMessage = "Tên tour không được để trống")]
        [MaxLength(255, ErrorMessage = "Tên tour không được vượt quá 255 ký tự")]
        public string Name { get; set; }
        public string? Detail { get; set; }
        [Required(ErrorMessage = "Điểm đến không được để trống")]
        [MaxLength(255, ErrorMessage = "Điểm khởi hành không được vượt quá 255 ký tự")]
        public string Departure { get; set; }


        [Required(ErrorMessage = "Điểm đến không được để trống")]
        [MaxLength(255, ErrorMessage = "Điểm đến không được vượt quá 255 ký tự")]
        public string Destination { get; set; }

        [Required(ErrorMessage = "Số lượng người không được để trống")]
        public int Quantity { get; set; }

         [Required(ErrorMessage = "Thời lượng không được để trống")]
        public int Duration { get; set; }
        public int CategoryId { get; set; }
    }
}