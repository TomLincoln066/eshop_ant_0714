using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models
{
    public class ProductCategoryViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Parent_CategoryId { get; set; }
    }
}
