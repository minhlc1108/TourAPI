using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TourAPI.Models;

namespace TourAPI.Interfaces.Repository
{
    public interface ITourImageRepository
    {
        Task AddTourImagesAsync(List<TourImage> tourImages);
    }
}