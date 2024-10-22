using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TourAPI.Models
{   
    [Table("Categories")]
    public class Category
    {   
        public int Id {get; set;}

        [Required]
        [StringLength(255)] 
        public string Name {get; set;}

        [StringLength(255)] 
        public string Detail {get; set;}

        public List<Tour> Tours {get; set;} = new List<Tour>();
    }
}