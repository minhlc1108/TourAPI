using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TourAPI.Models;

namespace TourAPI.Interfaces.Service
{
    public interface ITokenService 
    {
        Task<string> CreateToken(Account account);
    }
}