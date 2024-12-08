using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TourAPI.Data;
using TourAPI.Interfaces.Repository;
using TourAPI.Models;

namespace TourAPI.Repository
{
    public class TourScheduleRepository : ITourScheduleRepository
    {
        private readonly ApplicationDBContext _context;

        public TourScheduleRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<(List<TourSchedule>, int totalCount)> GetAllAsync()
        {
            var tourSchedules = _context.TourSchedules.AsQueryable();
            int totalCount = await tourSchedules.CountAsync();
            var pagedBookings = await tourSchedules.ToListAsync();
            return (pagedBookings, totalCount);
        }
    }
}