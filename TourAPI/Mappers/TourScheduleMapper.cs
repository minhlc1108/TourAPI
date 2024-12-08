using TourAPI.Dtos.Tour;
using TourAPI.Dtos.TourSchedule;
using TourAPI.Models;

namespace TourAPI.Mappers
{
    public static class TourScheduleMapper
    {
        public static TourScheduleDto ToTourScheduleDTO(this TourSchedule tourScheduleModel)
        {
            return new TourScheduleDto
            {
                Id = tourScheduleModel.Id,
                Name = tourScheduleModel.Name ?? string.Empty,
                DepartureDate = tourScheduleModel.DepartureDate,
                ReturnDate = tourScheduleModel.ReturnDate,
                Remain = tourScheduleModel.Remain,
                PriceAdult = tourScheduleModel.PriceAdult,
                PriceChild = tourScheduleModel.PriceChild,
                Status = tourScheduleModel.Status,
                TourId = tourScheduleModel.TourId,
                TourPromotions = tourScheduleModel.TourPromotions ?? new List<TourPromotion>(), 
                TransportDetails = tourScheduleModel.TransportDetails ?? new List<TransportDetail>() 
            };
        }
    }
}
