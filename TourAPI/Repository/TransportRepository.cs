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
    public class TransportRepository : ITransportRepository
    {
        private readonly ApplicationDBContext _dbContext;

        public TransportRepository(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Transport>> GetAllAsync()
        {
            return await _dbContext.Transports.ToListAsync();
        }

        public async Task<Transport?> GetByIdAsync(int id)
        {
            return await _dbContext.Transports.FindAsync(id);
        }

        public async Task<Transport> CreateAsync(Transport transport)
        {
            _dbContext.Transports.Add(transport);
            await _dbContext.SaveChangesAsync();
            return transport;
        }

        public async Task<Transport> UpdateAsync(Transport transport)
        {
            _dbContext.Transports.Update(transport);
            await _dbContext.SaveChangesAsync();
            return transport;
        }

        public async Task DeleteAsync(int id)
        {
            var transport = await _dbContext.Transports.FindAsync(id);
            if (transport != null)
            {
                _dbContext.Transports.Remove(transport);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}