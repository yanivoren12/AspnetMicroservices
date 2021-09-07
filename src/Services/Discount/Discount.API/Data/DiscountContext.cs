using Discount.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Discount.API.Data
{
    public class DiscountContext : DbContext
    {
        public DbSet<Coupon> Coupon { get; set; }

        public DiscountContext(DbContextOptions<DiscountContext> options) : base(options)
        {

        }
    }
}

