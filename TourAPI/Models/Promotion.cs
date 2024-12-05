using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TourAPI.Models
{
    [Table("Promotions")]
    public class Promotion
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string? Name { get; set; }
        [Range(0, 100)]
        public int Percentage { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Status { get; set; } = 1;
    }
}