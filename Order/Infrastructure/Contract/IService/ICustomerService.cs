using ApplicationCore.Entities;
using ApplicationCore.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Contract.IService
{
    public interface ICustomerService
    {
        Task<int> SaveCustomer(CustomerViewModel entity);
        Task<int> UpdateCustomer(CustomerViewModel entity);
        Task<int> DeleteCustomer(Guid customerId);
        Task<CustomerViewModel> GetCustomerById(Guid customerId);
    }
}
