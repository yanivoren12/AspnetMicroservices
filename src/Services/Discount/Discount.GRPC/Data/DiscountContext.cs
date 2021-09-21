using Discount.GRPC.Entities;
using Microsoft.EntityFrameworkCore;

namespace Discount.GRPC.Data
{
    public class DiscountContext : DbContext
    {
        public DbSet<Coupon> Coupon { get; set; }

        public DiscountContext(DbContextOptions<DiscountContext> options) : base(options)
        {

        }
    }
}

