using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TourAPI.Dtos.Category;
using TourAPI.Exceptions;
using TourAPI.Helpers;
using TourAPI.Interfaces.Repository;
using TourAPI.Interfaces.Service;
using TourAPI.Mappers;

namespace TourAPI.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepo;

        public CategoryService(ICategoryRepository categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }
        public async Task<CategoryDto> CreateAsync(CreateCategoryReqDto categoryDto)
        {
            var categoryModel = categoryDto.ToCategoryFromCreateDTO();
            await _categoryRepo.CreateAsync(categoryModel);
            return categoryModel.ToCategoryDTO();
        }

        public async Task<CategoryDto?> DeleteById(int id)
        {
            var categoryModel = await _categoryRepo.DeleteById(id);
            if (categoryModel == null)
            {
                throw new NotFoundException("Không tìm thấy danh mục");
            }
            return categoryModel.ToCategoryDTO();
        }

        public async Task<CategoryResultDto> GetAllAsync(CategoryQueryObject query)
        {
            var (categories, totalCount) = await _categoryRepo.GetAllAsync(query);
            var categoriesDto = categories.Select(c => c.ToCategoryDTO()).ToList();
            return new CategoryResultDto
            {
                Categories = categoriesDto,
                Total = totalCount
            };
        }

        public async Task<CategoryDto?> GetByIdAsync(int id)
        {
            var category = await _categoryRepo.GetByIdAsync(id);

            if (category == null)
            {
                throw new NotFoundException("Không tìm thấy danh mục");
            }
            return category.ToCategoryDTO();
        }

        public async Task<CategoryDto?> UpdateAsync(int id, UpdateCategoryReqDto categoryDto)
        {
            var categoryModel = await _categoryRepo.GetByIdAsync(id);
            if (categoryModel == null)
            {
                throw new NotFoundException("Không tìm thấy danh mục");
            }

            categoryModel.Name = categoryDto.Name;
            categoryModel.Detail = categoryDto.Detail;
            return categoryModel.ToCategoryDTO();
        }
    }
}