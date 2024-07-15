using ApplicationCore.Entities;
using ApplicationCore.ViewModel;
using Infrastructure.Common;
using Infrastructure.Contract.IRepository;
using Infrastructure.Contract.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Implementation.Service
{
    public class OrderdetailService : IOrderDetailService
    {
        private readonly IOrderDetailRepository _orderDetailRepository;

        public OrderdetailService(IOrderDetailRepository orderDetailRepository)
        {
            _orderDetailRepository = orderDetailRepository;
        }

        public Task<int> DeleteOD(int orderdetailId, int orderId)
        {
            var result = _orderDetailRepository.DeleteOrderDetail(orderdetailId, orderId);
            return result;
        }

        public async Task<IEnumerable<OrderDetailViewModel>> GetODByOrderId(int OrderId, int pageNumber, int pageSize)
        {
            var result = await _orderDetailRepository.GetOrderDetailsByOrderId(OrderId, pageNumber, pageSize);
            List<OrderDetailViewModel> detailViewModels = new List<OrderDetailViewModel>();
            var mapper = MapperConfig.InitializeAutomapper();
           
            foreach (var item in result)
            {
                var viewOrderdetails = mapper.Map<OrderDetailViewModel>(item);
                detailViewModels.Add(viewOrderdetails);
            }
           
            return detailViewModels;
        }

        public async Task<IEnumerable<OrderDetailViewModel>> GetODByOrderId(int orderId)
        {
            var result = await _orderDetailRepository.GetOrderDetailByOrderId(orderId);
            var mapper = MapperConfig.InitializeAutomapper();
            List<OrderDetailViewModel> orderDetailViews = new List<OrderDetailViewModel>();
            foreach (var item in result)
            {
                var orderViewModel = mapper.Map<OrderDetailViewModel>(item);
                orderDetailViews.Add(orderViewModel);
            }

            return orderDetailViews;
        }

        public async Task<int> SaveOD(OrderDetailViewModel orderDetailViewModel)
        {
            var mapper = MapperConfig.InitializeAutomapper();
            var entity = mapper.Map<OrderDetailsEntity>(orderDetailViewModel);

            var result = await _orderDetailRepository.SaveOrderDetail(entity);
            return result;
        }

        public async Task<int> UpdateOD(OrderDetailViewModel orderDetailViewModel)
        {
            var mapper = MapperConfig.InitializeAutomapper();
            var entity = mapper.Map<OrderDetailsEntity>(orderDetailViewModel);

            var result = await _orderDetailRepository.UpdateOrderDetail(entity);
            return result;
        }
    }
}
