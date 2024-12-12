using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TourAPI.Dtos.Promotion;
using TourAPI.Helpers;
using TourAPI.Models;

namespace TourAPI.Interfaces.Service
{
    public interface IPromotionService
    {
        Task<PromotionResultDto> GetAllAsync(PromotionQueryObject query); // Lấy tất cả khuyến mãi với bộ lọc
        Task<PromotionDto?> GetByIdAsync(int id); // Lấy khuyến mãi theo ID
        Task<PromotionDto> CreateAsync(CreatePromotionReqDto promotionDto); // Tạo mới khuyến mãi
        Task<PromotionDto?> UpdateAsync(int id, UpdatePromotionReqDto promotionDto); // Cập nhật khuyến mãi
        Task<PromotionDto?> DeleteById(int id); // Xóa khuyến mãi theo ID
        Task<PromotionDto?> GetByCodeAsync(string code); // Lấy khuyến mãi theo mã code
        Task<bool> IsCodeExistsAsync(string code);

    }
}
