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
                City = tourModel.City,
                Status = tourModel.Status,
                CategoryId = tourModel.CategoryId
            };
        }

        public static Tour ToTourFromCreateDTO(this CreateTourDto tourDto) {
            return new Tour {
                Name = tourDto.Name,
                Detail = tourDto.Detail,
                City = tourDto.City,
                Status = 1,
                CategoryId = tourDto.CategoryId
            };
        }   
    }
}