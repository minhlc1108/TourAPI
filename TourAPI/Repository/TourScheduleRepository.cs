using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TourAPI.Data;
using TourAPI.Exceptions;
using TourAPI.Helpers;
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

        public async Task<TourSchedule> CreateAsync(TourSchedule tourSchedule)
        {
            await _context.TourSchedules.AddAsync(tourSchedule);
            await _context.SaveChangesAsync();
            return tourSchedule;
        }

        public async Task<TourSchedule?> DeleteAsync(int id)
        {
            var tourSchedule = await _context.TourSchedules.FirstOrDefaultAsync(ts => ts.Id == id);
            if (tourSchedule == null)
            {
                return null;
            }
            tourSchedule.Status = 0;
            await _context.SaveChangesAsync();
            return tourSchedule;
        }

        public async Task<TourSchedule?> GetByIdAsync(int id)
        {
            return await _context.TourSchedules.Include(ts => ts.Tour).ThenInclude(t => t.TourImages).FirstOrDefaultAsync(ts => ts.Id == id && ts.Status == 1);
        }

        public async Task<(List<TourSchedule>, int total)> GetTourSchedules(TourScheduleQueryObject queryObject)
        {
            var tourSchedules = _context.TourSchedules.AsQueryable();


            if (!string.IsNullOrWhiteSpace(queryObject.Name))
            {
                tourSchedules = tourSchedules.Where(ts => ts.Name.Contains(queryObject.Name));
            }


            if (!string.IsNullOrWhiteSpace(queryObject.SortBy))
            {
                if (queryObject.SortBy.Equals("Id", StringComparison.OrdinalIgnoreCase))
                {
                    tourSchedules = queryObject.IsDecsending ? tourSchedules.OrderByDescending(ts => ts.Id) : tourSchedules.OrderBy(ts => ts.Id);
                }

                if (queryObject.SortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    tourSchedules = queryObject.IsDecsending ? tourSchedules.OrderByDescending(ts => ts.Name) : tourSchedules.OrderBy(ts => ts.Name);
                }

                if (queryObject.SortBy.Equals("DepartureDate", StringComparison.OrdinalIgnoreCase))
                {
                    tourSchedules = queryObject.IsDecsending ? tourSchedules.OrderByDescending(ts => ts.DepartureDate) : tourSchedules.OrderBy(ts => ts.DepartureDate);
                }

                if (queryObject.SortBy.Equals("ReturnDate", StringComparison.OrdinalIgnoreCase))
                {
                    tourSchedules = queryObject.IsDecsending ? tourSchedules.OrderByDescending(ts => ts.ReturnDate) : tourSchedules.OrderBy(ts => ts.ReturnDate);
                }

                if (queryObject.SortBy.Equals("Remain", StringComparison.OrdinalIgnoreCase))
                {
                    tourSchedules = queryObject.IsDecsending ? tourSchedules.OrderByDescending(ts => ts.Remain) : tourSchedules.OrderBy(ts => ts.Remain);
                }

                if (queryObject.SortBy.Equals("PriceAdult", StringComparison.OrdinalIgnoreCase))
                {
                    tourSchedules = queryObject.IsDecsending ? tourSchedules.OrderByDescending(ts => ts.PriceAdult) : tourSchedules.OrderBy(ts => ts.PriceAdult);
                }

                if (queryObject.SortBy.Equals("PriceChild", StringComparison.OrdinalIgnoreCase))
                {
                    tourSchedules = queryObject.IsDecsending ? tourSchedules.OrderByDescending(ts => ts.PriceChild) : tourSchedules.OrderBy(ts => ts.PriceChild);
                }
            }
            
            if(queryObject.TourId != null){
             tourSchedules = tourSchedules.Where(ts => ts.TourId == queryObject.TourId);
            }
            tourSchedules = tourSchedules.Where(ts => ts.Status == queryObject.Status);

            int totalCount = await tourSchedules.CountAsync();
            var skipNumber = (queryObject.PageNumber - 1) * queryObject.PageSize;
            var pagedTourSchedules = await tourSchedules.Skip(skipNumber).Take(queryObject.PageSize).ToListAsync();

            return (pagedTourSchedules, totalCount);
        }

        public async Task<TourSchedule> UpdateAsync(TourSchedule tourSchedule)
        {
            _context.TourSchedules.Update(tourSchedule);
            await _context.SaveChangesAsync();
            return tourSchedule;
        }
    }
}