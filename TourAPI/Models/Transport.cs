using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TourAPI.Models
{
    [Table("Transports")]
    public class Transport
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        [StringLength(255)]
        public string Detail { get; set; }
        [StringLength(255)]
        public string DepartureLocation { get; set; }
        [StringLength(255)]
        public string DestinationLocation { get; set; }
        public int Status {get; set;}  = 1; 
        public List<TransportDetail> TransportDetails = new List<TransportDetail> ();
    }
}