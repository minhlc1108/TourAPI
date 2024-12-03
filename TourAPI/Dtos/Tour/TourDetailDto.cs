namespace TourAPI.Dtos.Tour
{
    public class TourDetailDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Duration { get; set; }
        public string Destination { get; set; }
        public string Departure { get; set; } = String.Empty;
        public decimal TotalCostForAdult { get; set; }
        public List<TourScheduleDto> Schedules { get; set; }
    }
}
