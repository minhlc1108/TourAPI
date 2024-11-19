using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TourAPI.Interfaces.Repository;
using TourAPI.Interfaces.Service;
using TourAPI.Models;

namespace TourAPI.Service
{
    public class TransportService : ITransportService
    {
        private readonly ITransportRepository _transportRepository;

        public TransportService(ITransportRepository transportRepository)
        {
            _transportRepository = transportRepository;
        }

        public async Task<IEnumerable<Transport>> GetAllAsync()
        {
            return await _transportRepository.GetAllAsync();
        }

        public async Task<Transport?> GetByIdAsync(int id)
        {
            return await _transportRepository.GetByIdAsync(id);
        }

        public async Task<Transport> CreateAsync(Transport transport)
        {
            return await _transportRepository.CreateAsync(transport);
        }

        public async Task<Transport> UpdateAsync(Transport transport)
        {
            return await _transportRepository.UpdateAsync(transport);
        }

        public async Task DeleteAsync(int id)
        {
            await _transportRepository.DeleteAsync(id);
        }
    }
}