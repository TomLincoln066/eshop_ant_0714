using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models
{
    public class GetAllProductswithTotalCount
    {
        public int TotalProduct { get; set; }
        public IEnumerable<ProductViewModel> products { get; set; }
    }
}
