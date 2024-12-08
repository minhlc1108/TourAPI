using TourAPI.Dtos.Booking;

namespace TourAPI.Dtos.TourSchedule
{
    public class TourScheduleResultDto
    {
        public List<TourScheduleDto> TourSchedules { get; set; }
        public int Total { get; set; }
    }
}
