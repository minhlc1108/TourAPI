using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TourAPI.Data;
using TourAPI.Exceptions;
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

        public async Task AddAsync(TourImage tourImage)
        {
            await _context.AddAsync(tourImage);
            await _context.SaveChangesAsync();
        }

        public async Task AddTourImagesAsync(List<TourImage> tourImages)
        {
            await _context.TourImages.AddRangeAsync(tourImages);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var image = await _context.TourImages.FindAsync(id);
            if(image == null) {
                throw new NotFoundException("Không tìm thấy ảnh!");
            }
            _context.TourImages.Remove(image);
            await _context.SaveChangesAsync();
        }

        public async Task<List<TourImage>> GetByTourIdAsync(int tourId)
        {
            return await _context.TourImages.Where(tourImage => tourImage.TourId == tourId).ToListAsync();
        }

        public async Task UpdateAsync(TourImage tourImage)
        {
            _context.TourImages.Update(tourImage);
            await _context.SaveChangesAsync();
        }
    }
}