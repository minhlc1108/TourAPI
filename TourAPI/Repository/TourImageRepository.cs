using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TourAPI.Data;
using TourAPI.Interfaces.Repository;
using TourAPI.Models;

namespace TourAPI.Repository
{
    public class TourImageRepository : ITourImageRepository
    {   
        private readonly ApplicationDBContext _context;

        public TourImageRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task AddTourImagesAsync(List<TourImage> tourImages)
        {
            await _context.TourImages.AddRangeAsync(tourImages);
            await _context.SaveChangesAsync();
        }
    }
}