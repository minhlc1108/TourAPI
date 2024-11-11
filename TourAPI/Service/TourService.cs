using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TourAPI.Dtos.Tour;
using TourAPI.Helpers;
using TourAPI.Interfaces.Repository;
using TourAPI.Interfaces.Service;
using TourAPI.Mappers;

namespace TourAPI.Service
{
    public class TourService : ITourService
    {
        private readonly ITourRepository _tourRepo;

        public TourService(ITourRepository tourRepo)
        {
            _tourRepo = tourRepo;
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
    }
}