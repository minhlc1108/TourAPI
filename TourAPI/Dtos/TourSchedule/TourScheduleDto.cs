using TourAPI.Models;

namespace TourAPI.Dtos.TourSchedule
{
    public class TourScheduleDto
    {
        public int Id { get; set; }
        public string? Name { get; set; } = String.Empty;
        public DateTime DepartureDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public int Remain { get; set; }
        public int PriceAdult { get; set; }
        public int PriceChild { get; set; }
        public int Status { get; set; } = 1;
        public int TourId { get; set; }
        public List<TourPromotion> TourPromotions { get; set; } = new List<TourPromotion>();
        public List<TransportDetail> TransportDetails { get; set; } = new List<TransportDetail>();
    }
}
