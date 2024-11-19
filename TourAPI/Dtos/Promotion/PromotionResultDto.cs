using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TourAPI.Dtos.Promotion
{
    public class PromotionResultDto
    {
         public List<PromotionDto> Promotions { get; set; }
         public int Total {get; set;}
    }
}