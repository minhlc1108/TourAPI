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
                Code = promotionModel.Code,
                Status = promotionModel.Status,
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
                Code = promotionDto.Code,
                Status = promotionDto.Status,
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
                Code = promotionDto.Code,
                Status = promotionDto.Status
            };
        }
    }
}
