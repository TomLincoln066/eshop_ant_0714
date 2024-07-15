using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Contract.IRepository
{
    public interface IPaymentMethodRepository
    {
        Task<int> SavePaymentMethod(PaymentMethods paymentMethods);
        Task<int> UpdatePaymentMethod(PaymentMethods paymentMethods);
        Task<int> DeletePaymentMethod(int paymentMethodId);
        Task<IEnumerable<PaymentMethods>> GetPaymentMethods(Guid customerId);
    }
}
