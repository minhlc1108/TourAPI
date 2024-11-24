using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TourAPI.Dtos.Account;
using TourAPI.Dtos.Category;
using TourAPI.Dtos.Tour;
using TourAPI.Models;

namespace TourAPI.Mappers
{
    public static class TourScheduleMapper
    {
        public static TourScheduleDto ToTourScheduleDto(this TourSchedule TourSchedule)
        {
            return new TourScheduleDto
            {
                Id = TourSchedule.Id,
                Name  = TourSchedule.Name,
                DepartureDate = TourSchedule.DepartureDate,
                ReturnDate = TourSchedule.ReturnDate,
                Remain = TourSchedule.Remain,
                PriceAdult = TourSchedule.PriceAdult,
                PriceChild = TourSchedule.PriceChild,
                Status = TourSchedule.Status,
                TourId = TourSchedule.TourId,
                Tour = TourSchedule.Tour?.ToTourDTO(),

                
                // Tours = booking.Tours.Select(t => t.ToTourDTO()).ToList()
            };
        }
    }
}