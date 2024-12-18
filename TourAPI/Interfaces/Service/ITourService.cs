using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TourAPI.Dtos.Tour;
using TourAPI.Helpers;
using TourAPI.Models;

namespace TourAPI.Interfaces.Service
{
    public interface ITourService
    {
        Task<TourResultDto> GetAllAsync(TourQueryObject query);
        Task<TourDto?> GetByIdAsync(int id);

        Task<TourDto> CreateAsync(CreateTourReqDto createTourReqDto);
        Task<TourDto> UpdateAsync(int id, UpdateTourReqDto updateTourReqDto);
        Task<TourDto?> DeleteAsync(int id);
        Task<TourDetailDto?> GetTourDetail(int id);
    }
}