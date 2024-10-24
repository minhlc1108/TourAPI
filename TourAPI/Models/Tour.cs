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
        public string Name { get; set; }
        
        public string Detail {get;set;}

        [StringLength(255)]
        public string City {get; set;}

        public int Status {get; set;}

        public int CategoryId {get; set;}
        public Category Category {get; set;}
    }
}