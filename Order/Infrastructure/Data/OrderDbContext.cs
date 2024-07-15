using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class OrderDbContext : DbContext
    {
        public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options) { } 
        public DbSet<OrderEntity> Order { get; set; }
        public DbSet<OrderStatus> OrderStatus { get; set; }
        public DbSet<OrderDetailsEntity> OrderDetails { get; set; }
        public DbSet<CustomerEntity> Customer { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<ShoppingCart> ShoppingCart { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItem { get; set;}
        public DbSet<PaymentType> PaymentType { get; set; }
        public DbSet<PaymentMethods> PaymentMethod { get; set; }
    }
}
