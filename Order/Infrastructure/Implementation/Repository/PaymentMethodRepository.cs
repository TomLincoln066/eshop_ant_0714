using ApplicationCore.Entities;
using Infrastructure.Contract.IRepository;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Implementation.Repository
{
    public class PaymentMethodRepository : IPaymentMethodRepository
    {
        private readonly OrderDbContext _dbContext;

        public PaymentMethodRepository(OrderDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<int> DeletePaymentMethod(int paymentMethodId)
        {
            var entity = await _dbContext.PaymentMethod.Where(x => x.Id == paymentMethodId).FirstOrDefaultAsync();
            if (entity == null)
            {
                return 0;
            }

            _dbContext.PaymentMethod.Remove(entity);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<PaymentMethods>> GetPaymentMethods(Guid customerId)
        {
            var entity = await _dbContext.PaymentMethod.Where(x => x.CustomerId == customerId).ToListAsync();
            return entity;
        }

        public async Task<int> SavePaymentMethod(PaymentMethods paymentMethods)
        {
            await _dbContext.PaymentMethod.AddAsync(paymentMethods);
            var result = await _dbContext.SaveChangesAsync();
            return result;
        }

        public async Task<int> UpdatePaymentMethod(PaymentMethods paymentMethods)
        {
            _dbContext.PaymentMethod.Update(paymentMethods);
            return await _dbContext.SaveChangesAsync();
        }
    }
}
