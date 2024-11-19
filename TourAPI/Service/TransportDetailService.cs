using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TourAPI.Interfaces.Repository;
using TourAPI.Interfaces.Service;
using TourAPI.Models;

namespace TourAPI.Service
{
    public class TransportDetailService : ITransportDetailService
    {
        private readonly ITransportDetailRepository _transportDetailRepository;

        public TransportDetailService(ITransportDetailRepository transportDetailRepository)
        {
            _transportDetailRepository = transportDetailRepository;
        }

        public async Task<IEnumerable<TransportDetail>> GetAllAsync()
        {
            return await _transportDetailRepository.GetAllAsync();
        }

        public async Task<TransportDetail?> GetByIdAsync(int id)
        {
            return await _transportDetailRepository.GetByIdAsync(id);
        }

        public async Task<TransportDetail> CreateAsync(TransportDetail transportDetail)
        {
            return await _transportDetailRepository.CreateAsync(transportDetail);
        }

        public async Task<TransportDetail> UpdateAsync(TransportDetail transportDetail)
        {
            return await _transportDetailRepository.UpdateAsync(transportDetail);
        }

        public async Task DeleteAsync(int id)
        {
            await _transportDetailRepository.DeleteAsync(id);
        }
    }
}