using Discount.GRPC.Entities;
using System.Threading.Tasks;

namespace Discount.GRPC.Repositories
{
    public interface IDiscountRepository
    {
        Task<Coupon> GetDiscount(string productName);
        Task<Coupon> CreateDiscount(CouponDTO couponDTO);
        Task UpdateDiscount(Coupon coupon);
        Task<bool> DeleteDiscount(string productName);
    }
}
