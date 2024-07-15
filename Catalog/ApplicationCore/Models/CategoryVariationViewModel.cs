using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models
{
    public class CategoryVariationViewModel
    {
        public string Id { get; set; }
        public string CategoryId { get; set; }
        public string VariationName { get; set; }

        public List<VariationValueViewModel> Variations { get; set; }
    }
}
