using ApplicationCore.Entities;
using ApplicationCore.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Contract.IService
{
    public interface IPaymentMethodService
    {
        Task<int> SavePaymentMethod(PaymentMethodViewModel paymentMethods);
        Task<int> UpdatePaymentMethod(PaymentMethodViewModel paymentMethods);
        Task<int> DeletePaymentMethod(int paymentMethodId);
        Task<IEnumerable<PaymentMethodViewModel>> GetPaymentMethods(Guid customerId);
    }
}
