using AutoMapper;
using Discount.API.Data;
using Discount.API.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Discount.API.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly DiscountContext context;
        private readonly IMapper mapper;

        public DiscountRepository(DiscountContext context, IMapper mapper)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Coupon> CreateDiscount(CouponDTO couponDTO)
        {
            Coupon coupon = mapper.Map<Coupon>(couponDTO);
            await context.Coupon.AddAsync(coupon);
            await context.SaveChangesAsync();
            return coupon;
        }

        public async Task<bool> DeleteDiscount(string productName)
        {
            Coupon coupon = await GetDiscount(productName);
            if (coupon is null)
            {
                return false;
            }
            context.Coupon.Remove(coupon);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<Coupon> GetDiscount(string productName)
        {
            Coupon coupon = await context.Coupon.Where(x => x.ProductName == productName).FirstOrDefaultAsync();
            if (coupon is null)
            {
                return new() { ProductName = "No Discount", Amount = 0, Description = "No Discount Desc" };
            }
            return coupon;
        }

        public async Task UpdateDiscount(Coupon coupon)
        {
            context.Coupon.Update(coupon);
            await context.SaveChangesAsync();
        }
    }
}
