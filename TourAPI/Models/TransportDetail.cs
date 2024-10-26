using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TourAPI.Models
{
    [Table("TransportDetails")]
    public class TransportDetail
    {
        public int TransportId { get; set; }
        public int TourScheduleId { get; set; }
        public Transport Transport { get; set; }
        public TourSchedule TourSchedule { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public int Status { get; set; }
    }
}