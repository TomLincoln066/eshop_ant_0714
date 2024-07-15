using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    public class OrderEntity
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public Guid CustomerId { get; set; }
        public string CustomerName { get; set; }
        public int PaymentMethod { get;set; }
        public string PaymentName { get; set; }
        public string ShippingAddress { get; set; }
        public int ShippingMethod { get; set; }
        public decimal BillAmount { get; set; }
        public int OrderStatusId { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public ICollection<OrderDetailsEntity> orderDetails { get; set; }
    }
}
