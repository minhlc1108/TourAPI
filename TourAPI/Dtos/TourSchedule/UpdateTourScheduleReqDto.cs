using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TourAPI.Dtos.TourSchedule
{
    public class UpdateTourScheduleReqDto
    {
        [Required(ErrorMessage = "Tên tour không được để trống")]
        [MaxLength(255, ErrorMessage = "Tên tour không được vượt quá 255 ký tự")]
        public string Name { get; set; }
        public int PriceAdult { get; set; }
        public int PriceChild { get; set; }
    }
}