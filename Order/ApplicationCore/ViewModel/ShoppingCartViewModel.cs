using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.ViewModel
{
    public class ShoppingCartViewModel
    {
        public int Id { get; set; }
        public Guid CustomerId { get; set; }
        public string CustomerName { get; set; }
        public List<ShoppingCartItemViewModel> ShoppingItems { get; set; }
    }
}
