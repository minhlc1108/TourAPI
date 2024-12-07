using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TourAPI.Dtos.Tour;
using TourAPI.Helpers;
using TourAPI.Models;

namespace TourAPI.Interfaces.Repository
{
    public interface ITourRepository
    {
        Task<(List<Tour>, int totalCount)> GetAllAsync(TourQueryObject query);

        Task<Tour?> GetByIdAsync(int id);
        Task<Tour> CreateAsync(Tour tourModel);
        Task<Tour> UpdateAsync(Tour tourModel);
        Task<TourDetailDto> GetTourDetail(int id);
        Task<List<TourDetailDto>> GetAllToursAndToursSchedule();
        Task<Tour?> DeleteAsync(int id);
    }
}