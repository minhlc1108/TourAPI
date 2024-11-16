using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TourAPI.Dtos.Tour;
using TourAPI.Helpers;

namespace TourAPI.Interfaces.Service
{
    public interface ITourService
    {
        Task<TourResultDto> GetAllAsync(TourQueryObject query);
    }
}