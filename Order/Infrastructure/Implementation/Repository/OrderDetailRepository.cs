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
    public class OrderDetailRepository : IOrderDetailRepository
    {
        public readonly OrderDbContext _context;
        public OrderDetailRepository(OrderDbContext context)
        {
            _context = context;
        }
        public async Task<int> DeleteOrderDetail(int id, int OrderId)
        {
            var entity = await _context.OrderDetails.Where(x=>x.Id == id && x.OrderId == OrderId).FirstOrDefaultAsync();
            if(entity == null)
            {
                return -1;
            }
            
            _context.OrderDetails.Remove(entity);
            return await _context.SaveChangesAsync();
        }

        public async Task<OrderDetailsEntity> GetOrderDetailById(int orderdetailId)
        {
            var entity = await _context.OrderDetails.Where(x => x.Id == orderdetailId).FirstOrDefaultAsync();
            if(entity == null)
            {
                return null;
            }
            return entity;
        }

        public async Task<IEnumerable<OrderDetailsEntity>> GetOrderDetailsByOrderId(int OrderId, int pageNumber, int pageSize)
        {
            var entity = await _context.OrderDetails.Where(x => x.OrderId == OrderId).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            if(entity == null)
            {
                return null;
            }
            return entity;
        }

        public async Task<IEnumerable<OrderDetailsEntity>> GetOrderDetailByOrderId(int orderId)
        {
            var entity = await _context.OrderDetails.Where(x => x.OrderId == orderId).ToListAsync();
            if(entity.Count() == 0)
            {
                return null;
            }
            return entity;
        }

        public async Task<int> SaveOrderDetail(OrderDetailsEntity orderDetails)
        {
            await _context.OrderDetails.AddAsync(orderDetails);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateOrderDetail(OrderDetailsEntity orderDetails)
        {
                _context.OrderDetails.Update(orderDetails);
                return await _context.SaveChangesAsync();
        }
    }
}
