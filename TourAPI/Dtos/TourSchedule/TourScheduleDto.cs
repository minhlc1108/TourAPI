using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TourAPI.Dtos.TourImage;

namespace TourAPI.Dtos.Tour
{
     public class TourScheduleDto
    {
        public int Id { get; set; }
        public string? Name { get; set; } = String.Empty;
        public DateTime DepartureDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public int Remain { get; set; }
        public int PriceAdult { get; set; }
        public int PriceChild { get; set; }
        public int Status { get; set; } = 1;
        public int TourId { get; set; }
        public TourDto? Tour { get; set; }
      
    }
    
    }