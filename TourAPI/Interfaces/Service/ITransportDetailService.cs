using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TourAPI.Models;

namespace TourAPI.Interfaces.Service
{
    public interface ITransportDetailService
    {
        Task<IEnumerable<TransportDetail>> GetAllAsync();
        Task<TransportDetail?> GetByIdAsync(int id);
        Task<TransportDetail> CreateAsync(TransportDetail transportDetail);
        Task<TransportDetail> UpdateAsync(TransportDetail transportDetail);
        Task DeleteAsync(int id);
    }
}