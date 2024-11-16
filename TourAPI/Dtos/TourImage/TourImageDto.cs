using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TourAPI.Dtos.TourImage
{
    public class TourImageDto
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public int TourId { get; set; }
    }
}