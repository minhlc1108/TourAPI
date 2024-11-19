using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TourAPI.Dtos.Category;
using TourAPI.Helpers;
using TourAPI.Models;

namespace TourAPI.Interfaces.Repository
{
    public interface ICategoryRepository
    {
        Task<(List<Category>, int totalCount)> GetAllAsync(CategoryQueryObject query);
        Task<Category?> GetByIdAsync(int id);
        Task<Category> CreateAsync(Category categoryModel);
        Task<Category?> UpdateAsync(Category categoryModel);  
        Task<Category?> DeleteById(int id);
    }
}