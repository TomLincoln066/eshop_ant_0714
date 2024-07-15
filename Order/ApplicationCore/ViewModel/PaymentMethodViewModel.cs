using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.ViewModel
{
    public class PaymentMethodViewModel
    {
        public int Id { get; set; }
        public int PaymentTypeId { get; set; }
        public string Provider { get; set; }
        public string AccountNumber { get; set; }
        public DateTime Expiry { get; set; }
        public bool IsDefault { get; set; }
        public string PaymentTypeName { get; set; }
        public Guid CustomerId { get; set; }
    }
}
