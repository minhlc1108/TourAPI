using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TourAPI.Dtos.Account;
using TourAPI.Dtos.Category;
using TourAPI.Dtos.Tour;
using TourAPI.Dtos.TourSchedule;
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

        public static TourScheduleDto ToTourScheduleFromTourDto(this TourSchedule TourSchedule)
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
                // Tour = TourSchedule.Tour?.ToTourDTO(),

                
                // Tours = booking.Tours.Select(t => t.ToTourDTO()).ToList()
            };
        }

        public static TourScheduleResponseDto ToTourScheduleResponseDto(this TourSchedule TourSchedule)
        {
            return new TourScheduleResponseDto
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
            };
        }

        public static TourSchedule ToTourScheduleFromCreateDTO(this CreateTourScheduleRequestDto createTourScheduleRequestDto, int Quantity, int Duration)
        {
            return new TourSchedule
            {
                Name = createTourScheduleRequestDto.Name,
                DepartureDate = createTourScheduleRequestDto.DepartureDate,
                ReturnDate = createTourScheduleRequestDto.DepartureDate.AddDays(Duration),
                Remain = Quantity,
                PriceAdult = createTourScheduleRequestDto.PriceAdult,
                PriceChild = createTourScheduleRequestDto.PriceChild,
                TourId = createTourScheduleRequestDto.TourId,
                Status = 1
            };
        }
    }
}