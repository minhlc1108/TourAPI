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

        public TourService(ITourRepository tourRepo, ITourImageRepository tourImageRepo)
        {
            _tourRepo = tourRepo;
            _tourImageRepo = tourImageRepo;
        }

        public async Task<TourDto> CreateAsync(CreateTourReqDto createTourReqDto)
        {
            var tourModel = createTourReqDto.ToTourFromCreateDTO();
            tourModel = await _tourRepo.CreateAsync(tourModel);
            if (createTourReqDto.Images != null && createTourReqDto.Images.Any())
            {
                var tourImages = createTourReqDto.Images.Select(image => image.ToTourImageFromImageDTO(tourModel.Id)).ToList();

                await _tourImageRepo.AddTourImagesAsync(tourImages);
            }
            return tourModel.ToTourDTO();
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
    }
}