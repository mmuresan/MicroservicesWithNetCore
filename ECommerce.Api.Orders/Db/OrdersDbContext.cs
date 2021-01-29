using Microsoft.EntityFrameworkCore;

namespace ECommerce.Api.Orders.Db
{
    public class OrdersDbContext : DbContext
    {
        public OrdersDbContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<Db.Order> Orders { get; set; }
        public DbSet<Db.OrderItem> OrderItems { get; set; }

    }
}
