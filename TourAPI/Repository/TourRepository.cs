using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TourAPI.Data;
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

        public async Task<List<Tour>> GetAllAsync()
        {
            return await _context.Tours.ToListAsync();
        }

        public async Task<Tour?> GetByIdAsync(int id)
        {
            return await _context.Tours.FirstOrDefaultAsync(t => t.Id == id);
        }
    }
}