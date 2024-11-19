using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TourAPI.Models;
using TourAPI.Dtos.Promotion;
using TourAPI.Helpers;

namespace TourAPI.Interfaces.Repository
{
    public interface IPromotionRepository
    {
        // Phương thức lấy tất cả các khuyến mãi với phân trang
        Task<(List<Promotion>, int totalCount)> GetAllAsync(PromotionQueryObject query);

        // Phương thức lấy khuyến mãi theo ID
        Task<Promotion?> GetByIdAsync(int id);

        // Phương thức tạo mới khuyến mãi
        Task<Promotion> CreateAsync(Promotion promotionModel);

        // Phương thức cập nhật khuyến mãi
        Task<Promotion?> UpdateAsync(int id, UpdatePromotionReqDto promotionDto);

        // Phương thức xóa khuyến mãi theo ID
        Task<Promotion?> DeleteById(int id);
    }
}
