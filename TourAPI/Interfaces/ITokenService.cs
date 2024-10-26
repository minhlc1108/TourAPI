using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TourAPI.Models;

namespace TourAPI.Interfaces
{
    public interface ITokenService 
    {
        string CreateToken(Account account);
    }
}