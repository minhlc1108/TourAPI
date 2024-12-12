using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TourAPI.Data;
using TourAPI.Helpers;
using TourAPI.Interfaces.Repository;
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

        public async Task<(List<Category>,int totalCount)> GetAllAsync(CategoryQueryObject query)
        {
            var categories = _context.Categories.Include(c => c.Tours).AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Name))
            {
                categories = categories.Where(s => s.Name.Contains(query.Name));
            }

            if (!string.IsNullOrWhiteSpace(query.Detail))
            {
                categories = categories.Where(s => s.Detail.Contains(query.Detail));
            }

            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy.Equals("Id", StringComparison.OrdinalIgnoreCase))
                {
                    categories = query.IsDecsending ? categories.OrderByDescending(c => c.Id) : categories.OrderBy(c => c.Id);
                }

                if (query.SortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    categories = query.IsDecsending ? categories.OrderByDescending(c => c.Name) : categories.OrderBy(c => c.Name);
                }

                if (query.SortBy.Equals("Detail", StringComparison.OrdinalIgnoreCase))
                {
                    categories = query.IsDecsending ? categories.OrderByDescending(c => c.Detail) : categories.OrderBy(c => c.Detail);
                }
            }
            categories = categories.Where(c => c.Status == query.Status);
            int totalCount = await categories.CountAsync();
            var skipNumber = (query.PageNumber - 1) * query.PageSize;
            var pagedCategories =await categories.Skip(skipNumber).Take(query.PageSize).ToListAsync();
            return (
                pagedCategories,
                totalCount
            );
        }

        public async Task<Category?> GetByIdAsync(int id)
        {
            return await _context.Categories.Include(c => c.Tours).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Category?> UpdateAsync(Category categoryModel)
        {
            _context.Categories.Update(categoryModel);
            await _context.SaveChangesAsync();
            return categoryModel;
        }
    }
}