using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class ShippingDbContext:DbContext
{
    public ShippingDbContext(DbContextOptions<ShippingDbContext> option) : base(option)
    {
            
    }
    
    public DbSet<Region>Regions{ get; set; }
    public DbSet<Shipper>Shippers{ get; set; }
    public DbSet<ShipperRegion>ShipperRegions{ get; set; }
}