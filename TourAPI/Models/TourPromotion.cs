using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TourAPI.Models
{
    [Table("TourPromotions")]
    public class TourPromotion
    {
        public int PromotionId { get; set; }
        public int TourScheduleId { get; set; }
        public Promotion? Promotion { get; set; }
        public TourSchedule? TourSchedule { get; set; }
    }
}