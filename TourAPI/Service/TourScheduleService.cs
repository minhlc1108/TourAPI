using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TourAPI.Dtos.Booking;
using TourAPI.Dtos.TourSchedule;
using TourAPI.Interfaces.Repository;
using TourAPI.Interfaces.Service;
using TourAPI.Mappers;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace TourAPI.Service
{
    public class TourScheduleService : ITourScheduleService
    {
        private readonly ITourScheduleRepository _tourScheduleRepo;


        public TourScheduleService(ITourScheduleRepository tourScheduleRepo)
        {
            _tourScheduleRepo = tourScheduleRepo;
        }

        public async Task<TourScheduleResultDto> GetAllAsync()
        {
            var (tourShedules, totalCount) = await _tourScheduleRepo.GetAllAsync();
            var tourSchedules = tourShedules.Select(c => c.ToTourScheduleDTO()).ToList();
            return new TourScheduleResultDto
            {
                TourSchedules = tourSchedules,
                Total = tourShedules.Count()
            };

        }
    }
}