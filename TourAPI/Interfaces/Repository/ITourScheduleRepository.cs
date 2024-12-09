using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TourAPI.Helpers;
using TourAPI.Models;

namespace TourAPI.Interfaces.Repository
{
    public interface ITourScheduleRepository
    {
        Task<(List<TourSchedule>,int total)> GetByTourId(int tourId, TourScheduleQueryObject queryObject);
        Task<TourSchedule?> GetByIdAsync(int id);
        Task<TourSchedule> CreateAsync(TourSchedule tourSchedule);
        Task<TourSchedule> UpdateAsync(TourSchedule tourSchedule);
         Task<TourSchedule?> DeleteAsync(int id);
        Task<(List<TourSchedule>, int total)> GetTourSchedules(TourScheduleQueryObject queryObject);
        Task<bool> CheckAvailable(int tourScheduleId, int customerCount);
        Task<TourSchedule?> GetTourScheduleAsync(int id);
        Task IncreaseAvailableSlot(int tourScheduleId, int customerCount);
    }
}