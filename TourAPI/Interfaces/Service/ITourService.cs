using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TourAPI.Dtos.Category;
using TourAPI.Dtos.Tour;
using TourAPI.Helpers;
using TourAPI.Models;

namespace TourAPI.Interfaces.Service
{
    public interface ITourService
    {
        Task<TourResultDto> GetAllAsync(TourQueryObject query);

        // Task<List<Tour>> GetAllAsync();
    }
}