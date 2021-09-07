using AutoMapper;
using Discount.API.Entities;

namespace Discount.API.Configurations
{
    public class MapperInitilizer : Profile
    {
        public MapperInitilizer()
        {
            CreateMap<CouponDTO, Coupon>().ReverseMap();
        }
    }
}
