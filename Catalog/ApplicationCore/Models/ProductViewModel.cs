using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models
{
    public class ProductViewModel
    {
        public string Id { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string CategoryID { get; set; }
        public int Price { get; set; }
        public string Product_Image { get; set; }
        public int SKU { get; set; }
        public bool InActive { get; set; }
        public List<ProductCategoryViewModel> ProductCategory { get; set; }
        public List<CategoryVariationViewModel> CategoryVariation { get; set; }
        //public List<VariationValueViewModel> VariationValue { get; set; }
    }
}
