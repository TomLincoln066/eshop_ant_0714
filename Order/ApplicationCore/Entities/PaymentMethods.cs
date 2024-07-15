using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    public class PaymentMethods
    {
        public int Id { get; set; }
        public int PaymentTypeId { get; set; }
        public Guid CustomerId { get; set; }
        public string Provider { get; set; }
        public string AccountNumber { get; set; }
        public DateTime Expiry { get; set; }
        public bool IsDefault { get; set; }
        public PaymentType paymentType { get; set; }
    }
}
