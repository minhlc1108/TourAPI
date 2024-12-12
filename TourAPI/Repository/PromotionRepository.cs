using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TourAPI.Interfaces.Repository;
using TourAPI.Models;
using TourAPI.Data;
using Microsoft.EntityFrameworkCore;
using TourAPI.Helpers;
using TourAPI.Dtos.Promotion;
namespace TourAPI.Repository
{
     public class PromotionRepository : IPromotionRepository
    {
        private readonly ApplicationDBContext _context;

        public PromotionRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Promotion> CreateAsync(Promotion promotionModel)
        {
            await _context.AddAsync(promotionModel);
            await _context.SaveChangesAsync();
            return promotionModel;
        }
      


        public async Task<Promotion?> DeleteById(int id)
        {
            var promotion = await _context.Promotions.FirstOrDefaultAsync(p => p.Id == id);
            if (promotion == null)
            {
                return null;
            }
            promotion.Status = 0; // Giả định rằng 0 là trạng thái xóa
            await _context.SaveChangesAsync();
            return promotion;
        }

        public async Task<(List<Promotion>, int totalCount)> GetAllAsync(PromotionQueryObject query)
        {
            var promotions = _context.Promotions.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Name))
            {
                promotions = promotions.Where(p => p.Name.Contains(query.Name));
            }

            if (query.Percentage.HasValue)
            {
                promotions = promotions.Where(p => p.Percentage == query.Percentage.Value);
            }

            if (query.StartDate.HasValue)
            {
                promotions = promotions.Where(p => p.StartDate >= query.StartDate.Value);
            }

            if (query.EndDate.HasValue)
            {
                promotions = promotions.Where(p => p.EndDate <= query.EndDate.Value);
            }

            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy.Equals("Id", StringComparison.OrdinalIgnoreCase))
                {
                    promotions = query.IsDecsending ? promotions.OrderByDescending(p => p.Id) : promotions.OrderBy(p => p.Id);
                }

                if (query.SortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    promotions = query.IsDecsending ? promotions.OrderByDescending(p => p.Name) : promotions.OrderBy(p => p.Name);
                }

                if (query.SortBy.Equals("Percentage", StringComparison.OrdinalIgnoreCase))
                {
                    promotions = query.IsDecsending ? promotions.OrderByDescending(p => p.Percentage) : promotions.OrderBy(p => p.Percentage);
                }
            }

            promotions = promotions.Where(p => p.Status == query.Status);
            int totalCount = await promotions.CountAsync();
            var skipNumber = (query.PageNumber - 1) * query.PageSize;
            var pagedPromotions = await promotions.Skip(skipNumber).Take(query.PageSize).ToListAsync();

            return (pagedPromotions, totalCount);
        }

        public async Task<Promotion?> GetByCodeAsync(string code)
        {
            return await _context.Promotions
                .FirstOrDefaultAsync(p => p.Code == code && p.Status == 1);
        }

        public async Task<Promotion?> GetByIdAsync(int id)
        {
            return await _context.Promotions
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Promotion?> UpdateAsync(int id, UpdatePromotionReqDto promotionDto)
        {
            var promotion = await _context.Promotions.FirstOrDefaultAsync(p => p.Id == id);
            if (promotion == null)
            {
                return null;
            }

            promotion.Name = promotionDto.Name;
            promotion.Percentage = promotionDto.Percentage;
            promotion.StartDate = promotionDto.StartDate;
            promotion.EndDate = promotionDto.EndDate;

            await _context.SaveChangesAsync();

            return promotion;
        }
        public async Task<bool> ExistsByCodeAsync(string code)
    {
        return await _context.Promotions.AnyAsync(p => p.Code == code);
    }
    }
}