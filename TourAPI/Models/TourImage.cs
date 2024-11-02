using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TourAPI.Models
{
    [Table("TourImages")]
    public class TourImage
    {
        public int Id { get; set; }
        public string? Url { get; set; }
        public bool IsAvatar { get; set; }
        public int? TourId { get; set; }
        public Tour? Tour { get; set; }
    }
}