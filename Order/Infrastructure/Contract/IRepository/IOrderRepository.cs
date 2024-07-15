using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Contract.IRepository
{
    public interface IOrderRepository
    {
        Task<int> SaveOrder(OrderEntity order);
        Task<int> TotalOrders();
        Task<int> TotalCustomerOrders(Guid customerId);
        Task<int> DeleteOrder(int orderId);
        Task<OrderEntity> GetOrderById(int orderId);
        Task<int> UpdateOrder(OrderEntity order);
        Task<IEnumerable<OrderEntity>> GetOrderByCustomerId(Guid customerId, int pageNumber, int pageSize);
        Task<OrderEntity> GetOrderCustomerById(int orderId, Guid customerId);
        Task<IEnumerable<OrderEntity>> GetAll(int pageNumber, int pageSize);
        Task<OrderEntity> GetOrderBillByCustomerId(decimal billAmout, Guid customerId);
    }
}
