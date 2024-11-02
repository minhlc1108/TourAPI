using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TourAPI.Dtos.Category
{
    public class CategoryResultDto
    {
         public List<CategoryDto> Categories { get; set; }
         public int Total {get; set;}
    }
}