using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TourAPI.Dtos.TourImage;
using TourAPI.Models;

namespace TourAPI.Mappers
{
    public static class TourImageMapper
    {
           public static TourImageDto ToTourImageDTO(this TourImage tourImage)
        {
            return new TourImageDto
            {
                Id = tourImage.Id,
                Url = tourImage.Url,
                TourId = tourImage.TourId
            };
        }
        
            public static TourImage ToTourImageFromImageDTO(this ImageDto imageDto, int tourId) {
                return new TourImage {
                    TourId = tourId,
                    Url = imageDto.Url
                };
            }
    }
}