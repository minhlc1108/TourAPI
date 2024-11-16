using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TourAPI.Dtos.Tour
{
    public class TourResultDto
    {
        public List<TourDto> Tours { get; set; }
        public int Total { get; set; }
    }
}