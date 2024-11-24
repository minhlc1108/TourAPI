using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TourAPI.Dtos.Tour;
using TourAPI.Models;

namespace TourAPI.Mappers
{
    public static class TourMapper
    {
        public static TourDto ToTourDTO(this Tour tourModel)
        {
            return new TourDto
            {
                Id = tourModel.Id,
                Name = tourModel.Name,
                Detail = tourModel.Detail,
                Departure = tourModel.Departure,
                Destination = tourModel.Destination,
                Duration = tourModel.Duration,
                Quantity = tourModel.Quantity,
                Status = tourModel.Status,
                CategoryId = tourModel.CategoryId,
                CategoryName = tourModel.Category != null ? tourModel.Category.Name : string.Empty,
                Images = tourModel.TourImages.Select(image => image.ToTourImageDTO()).ToList(),
                TourSchedules = tourModel.TourSchedules.Select(ts => ts.ToTourScheduleDto()).ToList(),
            };
        }

        public static Tour ToTourFromCreateDTO(this CreateTourReqDto tourDto) {
            return new Tour {
                Name = tourDto.Name,
                Detail = tourDto.Detail,
                Departure = tourDto.Departure,
                Destination = tourDto.Destination,
                Quantity = tourDto.Quantity,
                Duration = tourDto.Duration,
                CategoryId = tourDto.CategoryId,
                Status = 1
            };
        }   
    }
}