using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TourAPI.Dtos.Tour;

namespace TourAPI.Dtos.Category
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public string? Detail { get; set; } = String.Empty;
        public int Status { get; set; }
        public List<TourDto>? Tours { get; set; }
    }
}