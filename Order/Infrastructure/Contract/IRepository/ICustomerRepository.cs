using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Contract.IRepository
{
    public interface ICustomerRepository
    {
        Task<int> SaveCustomer(CustomerEntity entity);
        Task<int> UpdateCustomer(CustomerEntity entity);
        Task<int> DeleteCustomer(Guid customerId);
        Task<CustomerEntity> GetCustomerById(Guid customerId);
    }
}
