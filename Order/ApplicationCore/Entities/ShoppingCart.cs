using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    public class ShoppingCart
    {
        public int Id { get; set; }
        public Guid CustomerId { get; set; }
        public string CustomerName { get; set; }
        public List<ShoppingCartItem> ShoppingItems { get; set; }
    }
}
