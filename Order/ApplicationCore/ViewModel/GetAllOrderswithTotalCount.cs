using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.ViewModel
{
    public class GetAllOrderswithTotalCount
    {
        public int TotalOrders { get; set; }
        public IEnumerable<OrderViewModel> orders { get; set; }
    }
}
