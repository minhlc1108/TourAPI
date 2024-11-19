using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TourAPI.Data;
using TourAPI.Helpers;
using TourAPI.Interfaces.Repository;
using TourAPI.Models;

namespace TourAPI.Repository
{
    public class TourRepository : ITourRepository
    {
        private readonly ApplicationDBContext _context;

        public TourRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<Tour> CreateAsync(Tour tourModel)
        {
            await _context.Tours.AddAsync(tourModel);
            await _context.SaveChangesAsync();
            return tourModel;
        }

        public async Task<(List<Tour>, int totalCount)> GetAllAsync(TourQueryObject query)
        {
            var tours = _context.Tours.Include(t => t.Category).Include(t => t.TourImages).Include(t => t.TourSchedules).AsQueryable();


            if (!string.IsNullOrWhiteSpace(query.Name))
            {
                tours = tours.Where(t => t.Name.Contains(query.Name));
            }

            if (!string.IsNullOrWhiteSpace(query.Departure))
            {
                tours = tours.Where(t => t.Name.Contains(query.Departure));
            }

            if (!string.IsNullOrWhiteSpace(query.Destination))
            {
                tours = tours.Where(t => t.Name.Contains(query.Destination));
            }

            if (query.Quantity.HasValue)
            {
                tours = tours.Where(t => t.Quantity == query.Quantity.Value);
            }

            if (query.Duration.HasValue)
            {
                tours = tours.Where(t => t.Duration == query.Duration.Value);
            }

            if (!string.IsNullOrWhiteSpace(query.CategoryName))
            {
                tours = tours.Where(t => t.Category != null ? t.Category.Name.Contains(query.CategoryName) : true);
            }

            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy.Equals("Id", StringComparison.OrdinalIgnoreCase))
                {
                    tours = query.IsDecsending ? tours.OrderByDescending(c => c.Id) : tours.OrderBy(c => c.Id);
                }

                if (query.SortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    tours = query.IsDecsending ? tours.OrderByDescending(c => c.Name) : tours.OrderBy(c => c.Name);
                }

                if (query.SortBy.Equals("Destination", StringComparison.OrdinalIgnoreCase))
                {
                    tours = query.IsDecsending ? tours.OrderByDescending(c => c.Destination) : tours.OrderBy(c => c.Destination);
                }

                if (query.SortBy.Equals("Departure", StringComparison.OrdinalIgnoreCase))
                {
                    tours = query.IsDecsending ? tours.OrderByDescending(c => c.Departure) : tours.OrderBy(c => c.Departure);
                }

                if (query.SortBy.Equals("Quantity", StringComparison.OrdinalIgnoreCase))
                {
                    tours = query.IsDecsending ? tours.OrderByDescending(c => c.Quantity) : tours.OrderBy(c => c.Quantity);
                }

                if (query.SortBy.Equals("Duration", StringComparison.OrdinalIgnoreCase))
                {
                    tours = query.IsDecsending ? tours.OrderByDescending(c => c.Duration) : tours.OrderBy(c => c.Duration);
                }

                if (query.SortBy.Equals("CategoryName", StringComparison.OrdinalIgnoreCase))
                {
                    tours = query.IsDecsending ? tours.OrderByDescending(t => t.Category != null ? t.Category.Name : string.Empty) : tours.OrderBy(t => t.Category != null ? t.Category.Name : string.Empty);
                }
            }

            tours = tours.Where(t => t.Status == query.Status);
            int totalCount = await tours.CountAsync();
            var skipNumber = (query.PageNumber - 1) * query.PageSize;
            var pagedTours = await tours.Skip(skipNumber).Take(query.PageSize).ToListAsync();
            return (
                pagedTours,
                totalCount
            );
        }

        public async Task<Tour?> GetByIdAsync(int id)
        {
            return await _context.Tours.Include(t => t.Category).Include(t => t.TourImages).FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<Tour> UpdateAsync(Tour tourModel)
        {
            _context.Tours.Update(tourModel);
            await _context.SaveChangesAsync();
            return tourModel;
        }
    }
}