using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TourAPI.Models;

namespace TourAPI.Interfaces
{
    public interface ITourRepository
    {
        Task<List<Tour>> GetAllAsync();

        Task<Tour?> GetByIdAsync(int id);
        Task<Tour> CreateAsync(Tour tourModel);
    }
}