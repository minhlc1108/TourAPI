using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using TourAPI.Data;
using TourAPI.Helpers;
using TourAPI.Dtos.Customer;
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

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            return await _context.Customers.ToListAsync();
        }
        public async Task<Customer?> GetByIdAsync(int id)
        {
            return await _context.Customers
                .Include(c => c.BookingDetails)
                .Include(c => c.Bookings)
                    .ThenInclude(b => b.TourSchedule)
                        .ThenInclude(ts => ts.Tour)
                            .ThenInclude(t => t.TourImages)

                .Include(c => c.Account)
                // .Include(c => c.Bookings.Select(v => v.TourSchedule)) 
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        //  public async Task<Customer?> GetByEmailAsync(string Email)
        // {
        //     return await _context.Customers
        //         .Include(c => c.BookingDetails)
        //         .Include(c => c.Bookings)
        //             .ThenInclude(b => b.TourSchedule)
        //                 .ThenInclude(ts => ts.Tour)
        //                     .ThenInclude(t => t.TourImages)

        //         .Include(c => c.Account)
        //         // .Include(c => c.Bookings.Select(v => v.TourSchedule)) 
        //         .FirstOrDefaultAsync(t => t.Email == Email);
        // }


        public async Task<Customer?> UpdateAsync(Customer customerModel)
        {
            _context.Customers.Update(customerModel);
            await _context.SaveChangesAsync();
            return customerModel;
        }

        public async Task<Customer?> GetCustomerByIdAsync(int id)
        {
            return await _context.Customers.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Customer?> GetCustomerByAccountIdAsync(string accountId)
        {
            return await _context.Customers
                .Include(c => c.BookingDetails)
                .Include(c => c.Bookings)
                    .ThenInclude(b => b.TourSchedule)
                        .ThenInclude(ts => ts.Tour)
                            .ThenInclude(t => t.TourImages)

                .Include(c => c.Account)
                .FirstOrDefaultAsync(c => c.AccountId == accountId);
        }
        //  public async Task<Customer?> GetByEmailAsync(string Email)
        // {
        //     return await _context.Customers
        //         .FirstOrDefaultAsync(c => c.Email == Email);
        // }
        public async Task<Customer?> GetCustomerByEmailAsync(string email)
        {
            return await _context.Customers
            .Include( c=> c.Account)
            .Include(c => c.Bookings)
                .FirstOrDefaultAsync(c => c.Account.Email == email);
            // .Include(c => c.BookingDetails)
            //     .Include(c => c.Bookings)
            //         .ThenInclude(b => b.TourSchedule)
            //             .ThenInclude(ts => ts.Tour)
            //                 .ThenInclude(t => t.TourImages)

        }

        public async Task AddCustomerAsync(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateCustomerAsync(Customer customer)
        {
            _context.Customers.Update(customer);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteCustomerAsync(string id)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(u => u.AccountId == id);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }

        public async Task<(List<Customer>, int totalCount)> GetAllAsync(CustomerQueryObject query)
        {
            var customers = _context.Customers
                 .Include(c => c.BookingDetails)
                 .Include(c => c.Bookings)
                 .Include(c => c.Account)
                 .AsQueryable();
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
    }
}
