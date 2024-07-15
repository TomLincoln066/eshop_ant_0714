using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class PromotionDbContext:DbContext
{
    public PromotionDbContext(DbContextOptions<PromotionDbContext> option) : base(option)
    {
            
    }
    
    public DbSet<Promotion>Promotions{ get; set; }
    public DbSet<PromotionDetails>PromotionDetails{ get; set; }
}