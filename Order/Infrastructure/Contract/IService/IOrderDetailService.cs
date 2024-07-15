using ApplicationCore.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Contract.IService
{
    public interface IOrderDetailService
    {
        Task<int> SaveOD(OrderDetailViewModel orderDetailViewModel);
        Task<int> DeleteOD(int orderdetailId, int orderId);
        Task<IEnumerable<OrderDetailViewModel>> GetODByOrderId(int OrderId, int pageNumber, int pageSize);
        Task<int> UpdateOD(OrderDetailViewModel orderDetailViewModel);
        Task<IEnumerable<OrderDetailViewModel>> GetODByOrderId(int orderId);
    }
}
