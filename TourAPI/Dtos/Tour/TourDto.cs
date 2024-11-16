using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TourAPI.Dtos.Tour
{
    public class TourDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public string? Detail { get; set; } = string.Empty;

        public string Departure { get; set; } = String.Empty;
        public string Destination { get; set; } = String.Empty;

        public int Quantity { get; set; }
        public int Duration { get; set; }
        public int Status { get; set; }
        public int? CategoryId { get; set; }
        public string? CategoryName { get; set; } = string.Empty;
    }
}