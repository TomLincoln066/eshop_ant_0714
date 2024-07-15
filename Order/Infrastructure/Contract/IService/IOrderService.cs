using ApplicationCore.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Contract.IService
{
    public interface IOrderService
    {
        Task<OrderViewModel> OrderStatus(int orderId, Guid customerId);
        Task<int> CancelOrder(int orderId, Guid customerId);
        Task<int> SaveOrder(OrderViewModel orderViewModel);
        Task<GetAllOrderswithTotalCount> GetAllOrdersByCustomerId(Guid customerId, int pageNumber, int pageSize);
        Task<GetAllOrderswithTotalCount> GetAllOrders(int pageNumber, int pageSize);
        Task<OrderViewModel> OrderCompleted(decimal bill, Guid customerId);
        Task<int> UpdateOrder(OrderViewModel orderViewModel);
    }
}
