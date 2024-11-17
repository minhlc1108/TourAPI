using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TourAPI.Data;
using TourAPI.Helpers;
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

        public async Task<(List<Customer>, int totalCount)> GetAllAsync(CustomerQueryObject query)
        {
            // var customers = _context.Customers.AsQueryable();
            var customers = _context.Customers.Include(c => c.BookingDetails).AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Name))
            {
                customers = customers.Where(c => c.Name.Contains(query.Name));
            }

            if (!string.IsNullOrWhiteSpace(query.Address))
            {
                customers = customers.Where(c => c.Address.Contains(query.Address));
            }
            if (query.Birthday.HasValue)
            {
                customers = customers.Where(c => c.Birthday.Date == query.Birthday.Value.Date);
            }


            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    customers = query.IsDecsending ? customers.OrderByDescending(c => c.Name) : customers.OrderBy(c => c.Name);
                }
                // Thêm các trường sắp xếp khác nếu cần
            }
            customers = customers.Where(c => c.Status == query.Status);
            int totalCount = await customers.CountAsync();
            var skipNumber = (query.PageNumber - 1) * query.PageSize;
            var pagedCustomers = await customers.Skip(skipNumber).Take(query.PageSize).ToListAsync();
            return (
                pagedCustomers,
                totalCount
            );
        }

        public Task<Customer?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}