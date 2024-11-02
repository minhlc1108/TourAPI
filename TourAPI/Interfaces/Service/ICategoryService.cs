using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TourAPI.Dtos.Category;
using TourAPI.Helpers;

namespace TourAPI.Interfaces.Service
{
    public interface ICategoryService 
    {
      
        Task<CategoryResultDto> GetAllAsync(CategoryQueryObject query);
        Task<CategoryDto?> GetByIdAsync(int id);
        Task<CategoryDto> CreateAsync(CreateCategoryReqDto categoryDto);
        Task<CategoryDto?> UpdateAsync(int id, UpdateCategoryReqDto categoryDto);  
        Task<CategoryDto?> DeleteById(int id);
    }
}