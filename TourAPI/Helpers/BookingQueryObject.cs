namespace TourAPI.Helpers
{
    public class BookingQueryObject
    {
        public string? Id { get; set; } = null;

        public string? CustomerId { get; set; } = null;

        public int? Status { get; set; } = null;

        public string? TourScheduleId { get; set; } = null;

        public DateTime? StartDate { get; set; } = null;
        public DateTime? EndDate { get; set; } = null;

        public string? SortBy { get; set; } = null;

        public bool IsDescending { get; set; } = false;

        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
