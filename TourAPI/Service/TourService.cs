using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TourAPI.Dtos.Tour;
using TourAPI.Exceptions;
using TourAPI.Helpers;
using TourAPI.Interfaces.Repository;
using TourAPI.Interfaces.Service;
using TourAPI.Mappers;

namespace TourAPI.Service
{
    public class TourService : ITourService
    {
        private readonly ITourRepository _tourRepo;
        private readonly ITourImageRepository _tourImageRepo;
        private readonly ICategoryRepository _categoryRepo;

        public TourService(ITourRepository tourRepo, ITourImageRepository tourImageRepo, ICategoryRepository categoryRepo)
        {
            _tourRepo = tourRepo;
            _tourImageRepo = tourImageRepo;
            _categoryRepo = categoryRepo;
        }

        public async Task<TourDto> CreateAsync(CreateTourReqDto createTourReqDto)
        {
            var category = await _categoryRepo.GetByIdAsync(createTourReqDto.CategoryId);
            if (category == null)
            {
                throw new NotFoundException("Không tìm thấy danh mục");
            }
            var tourModel = createTourReqDto.ToTourFromCreateDTO();
            tourModel = await _tourRepo.CreateAsync(tourModel);
            if (createTourReqDto.Images != null && createTourReqDto.Images.Any())
            {
                var tourImages = createTourReqDto.Images.Select(image => image.ToTourImageFromImageDTO(tourModel.Id)).ToList();

                await _tourImageRepo.AddTourImagesAsync(tourImages);
            }
            return tourModel.ToTourDTO();
        }

        public async Task<TourDto?> DeleteAsync(int id)
        {
            var tour = await _tourRepo.DeleteAsync(id);
            if(tour == null)
            {
                throw new NotFoundException("Không tìm thấy tour");
            }
            return tour.ToTourDTO();
        }

        public async Task<TourResultDto> GetAllAsync(TourQueryObject query)
        {
            var (tours, totalCount) = await _tourRepo.GetAllAsync(query);
            var toursDto = tours.Select(t => t.ToTourDTO()).ToList();
            return new TourResultDto
            {
                Tours = toursDto,
                Total = totalCount
            };
        }

        public async Task<TourDto?> GetByIdAsync(int id)
        {
            var tour = await _tourRepo.GetByIdAsync(id);

            if (tour == null)
            {
                throw new NotFoundException("Không tìm thấy tour");
            }
            return tour.ToTourDTO();
        }

        public async Task<TourDto> UpdateAsync(int id, UpdateTourReqDto updateTourReqDto)
        {
            var tour = await _tourRepo.GetByIdAsync(id);
            if (tour == null)
            {
                throw new NotFoundException("Không tìm thấy tour");
            }

            tour.Name = updateTourReqDto.Name;
            tour.Detail = updateTourReqDto.Detail;
            tour.Departure = updateTourReqDto.Departure;
            tour.Destination = updateTourReqDto.Destination;
            tour.Quantity = updateTourReqDto.Quantity;
            tour.Duration = updateTourReqDto.Duration;

            var tourImages = await _tourImageRepo.GetByTourIdAsync(id);

            var deleteImages = tourImages.Where(i => !updateTourReqDto.Images.Any(img => img.Id == i.Id)).ToList();
            foreach (var img in deleteImages)
            {
                await _tourImageRepo.DeleteAsync(img.Id);
            }

            var addImages = updateTourReqDto.Images.Where(i => i.Id == null).Select(image => image.ToTourImageFromImageDTO(id)).ToList();
            await _tourImageRepo.AddTourImagesAsync(addImages);

            var updateImages = updateTourReqDto.Images.Where(i => tourImages.Any(img => img.Id == i.Id)).ToList();

            foreach (var img in updateImages)
            {
                var imageModel = tourImages.FirstOrDefault(i => i.Id == img.Id);
                if (imageModel != null)
                {
                    imageModel.Url = img.Url;
                    await _tourImageRepo.UpdateAsync(imageModel);
                }
            }

            var updatedTour = await _tourRepo.UpdateAsync(tour);
            return updatedTour.ToTourDTO();
        }
    }

}
