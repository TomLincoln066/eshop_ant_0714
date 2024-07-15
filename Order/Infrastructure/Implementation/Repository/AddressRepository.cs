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
    public class AddressRepository : IAddressRepository
    {
        private readonly OrderDbContext _dbContext;
        public AddressRepository(OrderDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<int> DeleteAddress(int Id)
        {
            var entity = await _dbContext.Address.Where(x => x.Id == Id).FirstOrDefaultAsync();
            if (entity == null)
            {
                return 0;
            }

            _dbContext.Address.Remove(entity);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<Address> GetAddress(int Id)
        {
            var entity = await _dbContext.Address.Where(x => x.Id == Id).FirstOrDefaultAsync();
            return entity;
        }

        public async Task<IEnumerable<Address>> GetAddressByCustomerId(int customerId)
        {
            var entity = await _dbContext.Address.Where(x => x.CustomerId == customerId).ToListAsync();
            return entity;
        }

        public async Task<int> SaveAddress(Address address)
        {
            await _dbContext.Address.AddAsync(address);
            var result = await _dbContext.SaveChangesAsync();
            return result;
        }

        public async Task<int> UpdateAddress(Address address)
        {
             _dbContext.Address.Update(address);
            var result = await _dbContext.SaveChangesAsync();
            return result;
        }
    }
}
