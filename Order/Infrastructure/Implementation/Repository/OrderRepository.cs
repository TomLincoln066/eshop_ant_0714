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
    public class OrderRepository : IOrderRepository
    {
        protected readonly OrderDbContext _dbContext;

        public OrderRepository(OrderDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<int> DeleteOrder(int orderId)
        {
            var entity =  await _dbContext.Order.Where(x => x.Id == orderId).FirstOrDefaultAsync();
            if (entity == null)
            {
                return 0;
            }

            _dbContext.Order.Remove(entity);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<OrderEntity>> GetAll(int pageNumber, int pageSize)
        {
            var entity = await _dbContext.Order.OrderByDescending(x=>x.OrderDate).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            if(entity.Count() == 0)
            {
                return null;
            }
            return entity;
        }

        public async Task<IEnumerable<OrderEntity>> GetOrderByCustomerId(Guid customerId, int pageNumber, int pageSize)
        {
            var entity = await _dbContext.Order.Where(x=>x.CustomerId == customerId).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            if (entity.Count() == 0)
            {
                return null;
            }
            return entity;
        }

        public async Task<OrderEntity> GetOrderById(int orderId)
        {
            var entity = await _dbContext.Order.Where(x => x.Id == orderId).FirstOrDefaultAsync();
            if (entity == null)
            {
                return null;
            }
            return entity;
        }

        public async Task<OrderEntity> GetOrderCustomerById(int orderId, Guid customerId)
        {
            var entity = await _dbContext.Order.Where(x=>x.Id == orderId && x.CustomerId == customerId).FirstOrDefaultAsync();
            if( entity == null) 
            {
                return null;
            }
            return entity;
        }

        public async Task<OrderEntity> GetOrderBillByCustomerId(decimal billAmout, Guid customerId)
        {
            var entity = await _dbContext.Order.Where(x => x.BillAmount == billAmout && x.CustomerId == customerId).FirstOrDefaultAsync();
            if (entity == null)
            {
                return null;
            }
            return entity;
        }

        public async Task<int> SaveOrder(OrderEntity order)
        {
            await _dbContext.Order.AddAsync(order);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> TotalCustomerOrders(Guid customerId)
        {
            var totalCustomerOrder = await _dbContext.Order.Where(x=>x.CustomerId == customerId).ToListAsync();
            return totalCustomerOrder.Count();
        }

        public async Task<int> TotalOrders()
        {
            var total = await _dbContext.Order.CountAsync();
            return total;
        }

        public async Task<int> UpdateOrder(OrderEntity order)
        {
            _dbContext.Order.Update(order);
            return await _dbContext.SaveChangesAsync();
        }
    }
}
