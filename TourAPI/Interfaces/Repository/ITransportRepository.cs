using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TourAPI.Models;

namespace TourAPI.Interfaces.Repository
{
    public interface ITransportRepository
    {
        Task<IEnumerable<Transport>> GetAllAsync();
        Task<Transport?> GetByIdAsync(int id);
        Task<Transport> CreateAsync(Transport transport);
        Task<Transport> UpdateAsync(Transport transport);
        Task DeleteAsync(int id);
    }
}