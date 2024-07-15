using ApplicationCore.Entities;
using ApplicationCore.Enum;
using ApplicationCore.ViewModel;
using DocumentFormat.OpenXml.Drawing.Charts;
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
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderDetailService _orderDetailService;

        public OrderService(IOrderRepository orderRepository, IOrderDetailService orderDetailService)
        {
            _orderRepository = orderRepository;
            _orderDetailService = orderDetailService;
        }

        public async Task<int> CancelOrder(int orderId, Guid customerId)
        {
            var orderData = await _orderRepository.GetOrderCustomerById(orderId, customerId);
            if (orderData == null)
            {
                return -1;
            }
            else
            {
                orderData.OrderStatusId = (int)OrderStat.Cancel;
                var result = await _orderRepository.UpdateOrder(orderData);
                return result;
            }
        }

        public async Task<GetAllOrderswithTotalCount> GetAllOrders(int pageNumber, int pageSize)
        {
            var orders = await _orderRepository.GetAll(pageNumber, pageSize);
            List<OrderViewModel> orderViews = new List<OrderViewModel>();
            var mapper = MapperConfig.InitializeAutomapper();
            GetAllOrderswithTotalCount getAllOrderswithTotalCount = new GetAllOrderswithTotalCount();
            
            foreach (var item in orders)
            {
                var viewOrder = mapper.Map<OrderViewModel>(item);
                if(viewOrder.ShippingMethod != 0 && viewOrder.OrderStatusId != 0)
                {
                    viewOrder.Shipping = ((ApplicationCore.Enum.ShippingMethod)viewOrder.ShippingMethod).ToString();
                    viewOrder.Order_Status = ((ApplicationCore.Enum.OrderStat)viewOrder.OrderStatusId).ToString();
                }
                
                var orderDetails = await _orderDetailService.GetODByOrderId(viewOrder.Id);
                viewOrder.OrderDetail = orderDetails.ToList();
                orderViews.Add(viewOrder);
            }
            
            getAllOrderswithTotalCount.orders = orderViews;
            getAllOrderswithTotalCount.TotalOrders = await _orderRepository.TotalOrders();
            
            return getAllOrderswithTotalCount;
        }

        public async Task<GetAllOrderswithTotalCount> GetAllOrdersByCustomerId(Guid customerId, int pageNumber, int pageSize)
        {
            var orders = await _orderRepository.GetOrderByCustomerId(customerId, pageNumber, pageSize);
            List<OrderViewModel> orderViews = new List<OrderViewModel>();
            var mapper = MapperConfig.InitializeAutomapper();
            GetAllOrderswithTotalCount getAllOrderswithTotalCount = new GetAllOrderswithTotalCount();
            foreach (var item in orders)
            {
                var viewOrder = mapper.Map<OrderViewModel>(item);
                if (viewOrder.ShippingMethod != 0 && viewOrder.OrderStatusId != 0)
                {
                    viewOrder.Shipping = ((ApplicationCore.Enum.ShippingMethod)viewOrder.ShippingMethod).ToString();
                    viewOrder.Order_Status = ((ApplicationCore.Enum.OrderStat)viewOrder.OrderStatusId).ToString();
                }
                var orderDetails = await _orderDetailService.GetODByOrderId(viewOrder.Id);
                viewOrder.OrderDetail = orderDetails.ToList();
                orderViews.Add(viewOrder);
            }

            getAllOrderswithTotalCount.orders = orderViews;
            getAllOrderswithTotalCount.TotalOrders = await _orderRepository.TotalCustomerOrders(customerId);
            
            return getAllOrderswithTotalCount;
        }

        public async Task<int> SaveOrder(OrderViewModel orderViewModel)
        {
            
            var mapper = MapperConfig.InitializeAutomapper();
            var orderEntity = mapper.Map<OrderEntity>(orderViewModel);
            orderEntity.PaymentName = ((ApplicationCore.Enum.PaymentType)orderEntity.PaymentMethod).ToString();

                var orderDetail = orderViewModel.OrderDetail;
            if (Enum.IsDefined(typeof(ApplicationCore.Enum.PaymentType), orderViewModel.PaymentMethod) && Enum.IsDefined(typeof(ShippingMethod), orderViewModel.ShippingMethod))
            {
                orderEntity.OrderStatusId = (int)OrderStat.OrderPlaced;
                var result = await _orderRepository.SaveOrder(orderEntity);

                foreach (var item in orderDetail)
                {
                    var order = await _orderRepository.GetOrderCustomerById(orderEntity.Id, orderEntity.CustomerId);
                    item.OrderId = order.Id;
                    await _orderDetailService.SaveOD(item);
                }

                return result;
            }
            else
            {
                return -1;
            }
            
        }

        public async Task<OrderViewModel?> OrderCompleted(decimal bill, Guid customerId)
        {
            var mapper = MapperConfig.InitializeAutomapper();
            
            var orderEntity = await _orderRepository.GetOrderBillByCustomerId(bill, customerId);
            
            if (orderEntity == null)
            {
                return null;
            }
            else
            {
                orderEntity.OrderStatusId = (int)OrderStat.Complete;
                await _orderRepository.UpdateOrder(orderEntity);
                var viewOrder = mapper.Map<OrderViewModel>(orderEntity);
                
                var orderDetails = await _orderDetailService.GetODByOrderId(viewOrder.Id);
                if (viewOrder.ShippingMethod != 0 && viewOrder.OrderStatusId != 0)
                {
                    viewOrder.Shipping = ((ApplicationCore.Enum.ShippingMethod)viewOrder.ShippingMethod).ToString();
                    viewOrder.Order_Status = ((ApplicationCore.Enum.OrderStat)viewOrder.OrderStatusId).ToString();
                }
                
                viewOrder.OrderDetail = orderDetails.ToList();
                return viewOrder;
            }
        }

        public async Task<OrderViewModel> OrderStatus(int orderId, Guid customerId)
        {
            var order = await _orderRepository.GetOrderCustomerById(orderId, customerId);
            if(order == null)
            {
                return null;
            }
            var mapper = MapperConfig.InitializeAutomapper();
            var viewOrder = mapper.Map<OrderViewModel>(order);
            var orderDetails = await _orderDetailService.GetODByOrderId(viewOrder.Id);
            if (viewOrder.ShippingMethod != 0 && viewOrder.OrderStatusId != 0)
            {
                viewOrder.Shipping = ((ApplicationCore.Enum.ShippingMethod)viewOrder.ShippingMethod).ToString();
                viewOrder.Order_Status = ((ApplicationCore.Enum.OrderStat)viewOrder.OrderStatusId).ToString();
            }
            viewOrder.OrderDetail = orderDetails.ToList();
            return viewOrder;
        }

        public async Task<int> UpdateOrder(OrderViewModel orderViewModel)
        {
            var mapper = MapperConfig.InitializeAutomapper();
            var orderEntity = mapper.Map<OrderEntity>(orderViewModel);
            await _orderRepository.UpdateOrder(orderEntity);
            var orderDetail = orderViewModel.OrderDetail;
            foreach(var item in orderDetail)
            {
                await _orderDetailService.UpdateOD(item);
            }
            
            return 0;
        }
    }
}
