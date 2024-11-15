using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TourAPI.Data;
using TourAPI.Interfaces.Repository;
using TourAPI.Models;

namespace TourAPI.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDBContext _context;

        public CustomerRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public Task<Customer?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}