using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TourAPI.Models
{
    [Table("Tours")]
    public class Tour
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; } = String.Empty;

        public string? Detail { get; set; } = String.Empty;

        [StringLength(255)]
        public string Departure { get; set; } = String.Empty;

        [StringLength(255)]
        public string Destination { get; set; } = String.Empty;
        public int Quantity { get; set; }

        public int Duration { get; set; }
        public int Status { get; set; }

        public int CategoryId { get; set; }
        public Category? Category { get; set; } 
        public List<TourImage> TourImages { get; set; } = new List<TourImage>();
        public List<TourSchedule> TourSchedules { get; set; } = new List<TourSchedule>();
    }
}