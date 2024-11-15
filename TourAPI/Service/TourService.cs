using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TourAPI.Interfaces.Repository;
using TourAPI.Interfaces.Service;
using TourAPI.Models;

namespace TourAPI.Service
{
    public class TourService : ITourService
{
    private readonly ITourRepository _tourRepo;

    public TourService(ITourRepository tourRepo)
    {
        _tourRepo = tourRepo;
    }

    public async Task<List<Tour>> GetAllAsync()
    {
        return await _tourRepository.GetAllAsync(); // Gọi đến repository
    }

}

}