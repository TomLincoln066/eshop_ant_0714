using ApplicationCore.Entities;
using Infrastructure.Contract.IRepository;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Implementation.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly OrderDbContext _dbContext;
        public CustomerRepository(OrderDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<int> DeleteCustomer(Guid customerId)
        {
            var entity = await _dbContext.Customer.Where(x => x.UserId == customerId).FirstOrDefaultAsync();
            if (entity == null)
            {
                return 0;
            }

            _dbContext.Customer.Remove(entity);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<CustomerEntity> GetCustomerById(Guid customerId)
        {
            var entity = await _dbContext.Customer.Where(x => x.UserId == customerId).FirstOrDefaultAsync();
            return entity;
        }

        public async Task<int> SaveCustomer(CustomerEntity entity)
        {
            await _dbContext.Customer.AddAsync(entity);
            var result = await _dbContext.SaveChangesAsync();
            return result;
        }

        public async Task<int> UpdateCustomer(CustomerEntity entity)
        {
            _dbContext.Customer.Update(entity);
            return await _dbContext.SaveChangesAsync();
        }
    }
}
