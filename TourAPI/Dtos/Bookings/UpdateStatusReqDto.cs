using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TourAPI.Dtos.Bookings
{
    public class UpdateStatusReqDto
    {
        public int Id { get; set; }
        public int Status { get; set; }
    }
}