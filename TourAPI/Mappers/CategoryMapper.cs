using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TourAPI.Dtos.Category;
using TourAPI.Models;

namespace TourAPI.Mappers
{
    public static class CategoryMapper
    {
        public static CategoryDto ToCategoryDTO(this Category categoryModel)
        {
            return new CategoryDto
            {
                Id = categoryModel.Id,
                Name = categoryModel.Name,
                Detail = categoryModel.Detail,
                Status = categoryModel.Status,
                Tours = categoryModel.Tours.Select(t => t.ToTourDTO()).ToList()
            };
        }

        public static Category ToCategoryFromCreateDTO(this CreateCategoryReqDto categoryDto)
        {
            return new Category
            {
              Name = categoryDto.Name,
              Detail = categoryDto.Detail,
              Status = 1
            };
        }
    }
}