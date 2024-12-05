using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TourAPI.Dtos.TourSchedule
{
    public class TourScheduleResultDto
    {
        public List<TourScheduleResponseDto> TourSchedules { get; set; } = new List<TourScheduleResponseDto>();
        public int Total { get; set; }
    }
}