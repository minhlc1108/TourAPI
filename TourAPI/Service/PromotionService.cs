using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TourAPI.Dtos.Promotion;
using TourAPI.Exceptions;
using TourAPI.Interfaces.Repository;
using TourAPI.Interfaces.Service;
using TourAPI.Mappers;
using TourAPI.Helpers;

namespace TourAPI.Service
{
    public class PromotionService : IPromotionService
    {
        private readonly IPromotionRepository _promotionRepo;

        public PromotionService(IPromotionRepository promotionRepo)
        {
            _promotionRepo = promotionRepo;
        }

        public async Task<PromotionDto> CreateAsync(CreatePromotionReqDto promotionDto)
        {
            var promotionModel = promotionDto.ToPromotionFromCreateDTO(); // Chuyển đổi DTO sang model
            await _promotionRepo.CreateAsync(promotionModel); // Lưu vào DB
            return promotionModel.ToPromotionDTO(); // Chuyển đổi model sang DTO và trả về
        }

        public async Task<PromotionDto?> DeleteById(int id)
        {
            var promotionModel = await _promotionRepo.DeleteById(id); // Xóa từ DB
            if (promotionModel == null)
            {
                throw new NotFoundException("Không tìm thấy khuyến mãi");
            }
            return promotionModel.ToPromotionDTO(); // Chuyển đổi model sang DTO và trả về
        }

        public async Task<PromotionResultDto> GetAllAsync(PromotionQueryObject query)
        {
            var (promotions, totalCount) = await _promotionRepo.GetAllAsync(query); // Lấy danh sách khuyến mãi từ DB
            var promotionsDto = promotions.Select(p => p.ToPromotionDTO()).ToList(); // Chuyển đổi sang DTO
            return new PromotionResultDto
            {
                Promotions = promotionsDto, // Danh sách DTO khuyến mãi
                Total = totalCount // Tổng số khuyến mãi
            };
        }

        public async Task<PromotionDto?> GetByIdAsync(int id)
        {
            var promotion = await _promotionRepo.GetByIdAsync(id); // Lấy khuyến mãi theo ID
            if (promotion == null)
            {
                throw new NotFoundException("Không tìm thấy khuyến mãi");
            }
            return promotion.ToPromotionDTO(); // Chuyển đổi và trả về DTO
        }

        public async Task<PromotionDto?> UpdateAsync(int id, UpdatePromotionReqDto promotionDto)
        {
            var promotionModel = await _promotionRepo.UpdateAsync(id, promotionDto); // Cập nhật khuyến mãi từ DB
            if (promotionModel == null)
            {
                throw new NotFoundException("Không tìm thấy khuyến mãi");
            }
            return promotionModel.ToPromotionDTO(); // Chuyển đổi model sang DTO và trả về
        }
    }
}
