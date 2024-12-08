using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TourAPI.Dtos.Booking;
using TourAPI.Dtos.TourSchedule;
using TourAPI.Helpers;

namespace TourAPI.Interfaces.Service
{
    public interface ITourScheduleService
    {
        Task<TourScheduleResultDto> GetAllAsync();
    }
}