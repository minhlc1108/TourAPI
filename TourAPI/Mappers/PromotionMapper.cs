using System;
using System.Collections.Generic;
using System.Linq;
using TourAPI.Dtos.Promotion;
using TourAPI.Models;

namespace TourAPI.Mappers
{
    public static class PromotionMapper
    {
        // Chuyển đổi từ model Promotion sang PromotionDto
        public static PromotionDto ToPromotionDTO(this Promotion promotionModel)
        {
            return new PromotionDto
            {
                Id = promotionModel.Id,
                Name = promotionModel.Name,
                Percentage = promotionModel.Percentage,
                StartDate = promotionModel.StartDate,
                EndDate = promotionModel.EndDate,
                Status = promotionModel.Status,
                Code = promotionModel.Code,

            };
        }

        // Chuyển đổi từ CreatePromotionReqDto sang model Promotion
        public static Promotion ToPromotionFromCreateDTO(this CreatePromotionReqDto promotionDto)
        {
            return new Promotion
            {
                Name = promotionDto.Name,
                Percentage = promotionDto.Percentage,
                StartDate = promotionDto.StartDate,
                EndDate = promotionDto.EndDate,
                Status = promotionDto.Status,
                Code = promotionDto.Code
                
            };
        }

        // Chuyển đổi từ UpdatePromotionReqDto sang model Promotion
        public static Promotion ToPromotionFromUpdateDTO(this UpdatePromotionReqDto promotionDto)
        {
            return new Promotion
            {
                Name = promotionDto.Name,
                Percentage = promotionDto.Percentage,
                StartDate = promotionDto.StartDate,
                EndDate = promotionDto.EndDate,
                Status = promotionDto.Status,
                Code = promotionDto.Code,

            };
        }
    }
}
