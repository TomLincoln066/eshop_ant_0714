using ApplicationCore.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    public class OrderDetailsEntity
    {
        public int Id { get; set; }
        public int OrderId { get; set; } 
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public int Qty { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public OrderEntity Order { get; set; }
    }
}
