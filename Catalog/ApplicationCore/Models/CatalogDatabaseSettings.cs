using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models
{
    public class CatalogDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string CategoryVariationCollectionName { get; set; } = null!;
        public string ProductsCollectionName { get; set; } = null!;
        public string ProductCategoryCollectionName { get; set; } = null!;
        public string VariationValueCollectionName { get; set; } = null!;
        public string ProductVaraiationValuesCollectionName { get; set; } = null!;
    }
}
