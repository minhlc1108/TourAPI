using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TourAPI.Dtos.Category;
using TourAPI.Helpers;
using TourAPI.Models;

namespace TourAPI.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllAsync(QueryObject query);
        Task<Category?> GetByIdAsync(int id);
        Task<Category> CreateAsync(Category categoryModel);
        Task<Category?> UpdateAsync(int id, UpdateCategoryReqDto categoryDto);  
        Task<Category?> DeleteById(int id);
    }
}