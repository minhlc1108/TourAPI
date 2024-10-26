using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TourAPI.Data;
using TourAPI.Dtos.Category;
using TourAPI.Helpers;
using TourAPI.Interfaces;
using TourAPI.Models;

namespace TourAPI.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDBContext _context;

        public CategoryRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Category> CreateAsync(Category categoryModel)
        {
            await _context.AddAsync(categoryModel);
            await _context.SaveChangesAsync();
            return categoryModel;
        }

        public async Task<Category?> DeleteById(int id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (category == null)
            {
                return null;
            }
            category.Status = 0;
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<List<Category>> GetAllAsync(QueryObject query)
        {
            var categories = _context.Categories.Include(c => c.Tours).AsQueryable();

             if (!string.IsNullOrWhiteSpace(query.Key)) {
                categories = categories.Where(s => s.Name.Contains(query.Key) || s.Detail.Contains(query.Key));
             }

             if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                 if (query.SortBy.Equals("Name", StringComparison.OrdinalIgnoreCase)){
                    categories = query.IsDecsending ? categories.OrderByDescending(c => c.Name) : categories.OrderBy(c => c.Name);
                 }

                  if (query.SortBy.Equals("Detail", StringComparison.OrdinalIgnoreCase)){
                    categories = query.IsDecsending ? categories.OrderByDescending(c => c.Detail) : categories.OrderBy(c => c.Detail);
                 }
            }

            var skipNumber = (query.PageNumber - 1) * query.PageSize;

            return await categories.Skip(skipNumber).Take(query.PageSize).ToListAsync();
        }

        public async Task<Category?> GetByIdAsync(int id)
        {
            return await _context.Categories.Include(c => c.Tours).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Category?> UpdateAsync(int id, UpdateCategoryReqDto categoryDto)
        {
            var category = await _context.Categories.FirstOrDefaultAsync((c => c.Id == id));
            if (category == null)
            {
                return null;
            }
            category.Name = categoryDto.Name;
            category.Detail = category.Detail;

            await _context.SaveChangesAsync();

            return category;
        }
    }
}