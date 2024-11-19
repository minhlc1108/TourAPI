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
    public class TransportDetailRepository : ITransportDetailRepository
    {
        private readonly ApplicationDBContext _dbContext;

        public TransportDetailRepository(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<TransportDetail>> GetAllAsync()
        {
            return await _dbContext.TransportDetails.ToListAsync();
        }

        public async Task<TransportDetail?> GetByIdAsync(int id)
        {
            return await _dbContext.TransportDetails.FindAsync(id);
        }

        public async Task<TransportDetail> CreateAsync(TransportDetail transportDetail)
        {
            _dbContext.TransportDetails.Add(transportDetail);
            await _dbContext.SaveChangesAsync();
            return transportDetail;
        }

        public async Task<TransportDetail> UpdateAsync(TransportDetail transportDetail)
        {
            _dbContext.TransportDetails.Update(transportDetail);
            await _dbContext.SaveChangesAsync();
            return transportDetail;
        }

        public async Task DeleteAsync(int id)
        {
            var transportDetail = await _dbContext.TransportDetails.FindAsync(id);
            if (transportDetail != null)
            {
                _dbContext.TransportDetails.Remove(transportDetail);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}