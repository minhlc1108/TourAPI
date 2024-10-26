using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TourAPI.Dtos.Tour
{
    public class TourDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Detail { get; set; }
        public string City { get; set; }
        public int Status { get; set; }
        public int CategoryId { get; set; }
    }
}